param ($env)

if ($env -eq "dev") {
  docker-compose -f docker-compose.dev.yml --env-file dev.env up --build
} elseif ($env -eq "prod") {
  docker-compose -f docker-compose.prod.yml --env-file prod.env up --build
} else {
  Write-Host "Unknown environment: $env"
  Exit 1
}