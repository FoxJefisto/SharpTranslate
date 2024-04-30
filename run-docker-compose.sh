#!/bin/bash

# Check if the environment variable is set to "dev" or "prod"
if [ "$1" = "dev" ]; then
  docker-compose -f docker-compose.dev.yml down
  docker-compose -f docker-compose.dev.yml --env-file dev.env up --build --detach
elif [ "$1" = "prod" ]; then
  docker-compose -f docker-compose.prod.yml down 
  docker-compose -f docker-compose.prod.yml --env-file prod.env up --build --detach
else
  echo "Unknown environment: $1"
  exit 1
fi

# Wait for the Postgres container to start
COUNT=0
MAX_COUNT=10
sleep 10

while [ "$(docker container inspect -f '{{.State.Running}}' postgres)" != "true" ]; do
  echo "Waiting for container to start..."
  sleep 10
  COUNT=$((COUNT + 1))
  if [ $COUNT -ge $MAX_COUNT ]; then
    echo "Timeout: container did not start within 3 minutes"
    exit 1
  fi
done

# Run EF Core migrations
echo "Running EF Core migrations..."
if [ "$1" = "dev" ]; then
  dotnet ef database update --project SharpTranslate -- --environment Development
elif [ "$1" = "prod" ]; then
  dotnet ef database update --project SharpTranslate
fi
echo "EF Core migrations complete"