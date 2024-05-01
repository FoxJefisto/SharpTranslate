#!/bin/bash

# Check if the environment variable is set to "dev" or "prod"
echo "Reloading docker containers..."
if [ "$1" = "dev" ]; then
  docker-compose -f docker-compose.dev.yml --env-file dev.env down
  docker-compose -f docker-compose.dev.yml --env-file dev.env up --build -d
elif [ "$1" = "prod" ]; then
  docker-compose -f docker-compose.prod.yml --env-file dev.env down 
  docker-compose -f docker-compose.prod.yml --env-file prod.env up -d
else
  echo "Unknown environment: $1"
  exit 1
fi
echo "Containers is running"

# Wait for the Postgres container to start
COUNT=0
MAX_COUNT=10
sleep 12

while [ "$(docker container inspect -f '{{.State.Running}}' postgres)" != "true" ]; do
  echo "Waiting for container to start..."
  sleep 10
  COUNT=$((COUNT + 1))
  if [ $COUNT -ge $MAX_COUNT ]; then
    echo "Timeout: container did not start within 2 minutes"
    exit 1
  fi
done
echo "Postgres container is running"

# Containers ps
docker ps -a

# # Check if dotnet-ef is installed
# dotnet-ef --version

# if dotnet-ef --version
# then
#   echo "dotnet-ef is already installed"
# else
#   # Install dotnet-ef if it's not installed
#   echo "dotnet-ef is not installed. Installing..."
#   dotnet tool install --global dotnet-ef --version 6.0.29
# fi

# Run EF Core migrations
echo "Running EF Core migrations..."
if [ "$1" = "dev" ]; then
  dotnet-ef database update --project SharpTranslate -- --environment Development
elif [ "$1" = "prod" ]; then
  dotnet-ef database update --project SharpTranslate -- --environment Production
fi
echo "EF Core migrations complete"
