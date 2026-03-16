#!/bin/bash

# $CONN_STR is saved in .env
set -a
source .env
set +a

# Connection string format from rpede.github.io/connection_strings/
dotnet tool install -g dotnet-ef
dotnet ef dbcontext scaffold "$CONN_STR" Npgsql.EntityFrameworkCore.PostgreSQL \
    --output-dir ./Entities \
    --context-dir . \
    --context MyDbContext \
    --no-onconfiguring \
    --namespace efscaffold.Entities \
    --context-namespace Infrastructure.Postgres.Scaffolding \
    --schema todosystem \
    --force