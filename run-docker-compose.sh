#!/bin/bash

if [ "$1" = "dev" ]; then
  docker-compose -f docker-compose.dev.yml down
  docker-compose -f docker-compose.dev.yml --env-file dev.env up --build
elif [ "$1" = "prod" ]; then
  docker-compose -f docker-compose.prod.yml down 
  docker-compose -f docker-compose.prod.yml --env-file prod.env up --build
else
  echo "Unknown environment: $1"
  exit 1
fi