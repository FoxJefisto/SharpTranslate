#!/bin/bash

if [ "$1" = "dev" ]; then
  docker-compose -f docker-compose.dev.yml up
elif [ "$1" = "prod" ]; then
  docker-compose -f docker-compose.prod.yml up
else
  echo "Unknown environment: $1"
  exit 1
fi