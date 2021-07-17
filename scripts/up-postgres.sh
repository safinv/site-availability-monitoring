#!/bin/bash

image_name=postgres;

containers=$(docker container ls --filter name=$image_name --quiet)

for container in $containers; do
    docker container kill $container;
done;

docker run --rm --detach \
    --name $image_name \
    --publish 5432:5432 \
    --env "POSTGRES_DB=postgres" \
    --env "POSTGRES_USER=postgres" \
    --env "POSTGRES_PASSWORD=postgres" \
    $image_name