# Check if the environment variable is set to "dev" or "prod"
param ($env)

if ($env -eq "dev") {
  docker-compose -f docker-compose.dev.yml down
  docker-compose -f docker-compose.dev.yml --env-file dev.env up -d
} elseif ($env -eq "prod") {
  docker-compose -f docker-compose.prod.yml down
  docker-compose -f docker-compose.prod.yml --env-file prod.env up -d
} else {
  Write-Host "Unknown environment: $env"
  exit 1
}

# Wait for the Postgres container to start
$COUNT = 0
$MAX_COUNT = 10
Start-Sleep -s 10

while ((docker container inspect -f '{{.State.Running}}' postgres) -ne "true") {
  Write-Host "Waiting for container to start..."
  Start-Sleep -s 10
  $COUNT++
  if ($COUNT -ge $MAX_COUNT) {
    Write-Host "Timeout: container did not start within 3 minutes"
    exit 1
  }
}

# Run EF Core migrations
Write-Host "Running EF Core migrations..."
if ($env -eq "dev") {
  dotnet ef database update --project SharpTranslate -- --environment Development
} elseif ($env -eq "prod") {
  dotnet ef database update --project SharpTranslate
}
Write-Host "EF Core migrations complete"