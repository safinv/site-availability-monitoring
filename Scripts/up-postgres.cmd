@echo off

set NAME=postgres

call docker-container-stop "%NAME%"

call docker run --rm --detach ^
    --name "%NAME%" ^
    --publish 5432:5432 ^
    --env "POSTGRES_DB=postgres" ^
    --env "POSTGRES_USER=postgres" ^
    --env "POSTGRES_PASSWORD=postgres" ^
    postgres

@pause