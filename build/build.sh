#!/bin/bash

source ./.env
# echo -e "\033[33m please input account[$DOCKER_REGISTRY_USERNAME] password,and input e to finish: \033[0m"
# read input
# docker login --username=$DOCKER_REGISTRY_USERNAME --password=$DOCKER_REGISTRY_PASSWORD $DOCKER_REGISTRY


#±àÒë·þÎñ
docker-compose up --build -d --force-recreate $DOCKER_REGISTRY_IMAGE