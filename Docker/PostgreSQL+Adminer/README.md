# Abstract
This docker package creates container with PostgreSQL database and Adminer panel.

# Details
## PostgreSQL
* Default credentials:
    * user: `postgres`;
    * password: `1`;
* Default database name: `postgres`;
* URL: `http://localhost:5432`;

## Adminer
* URL: `http://localhost:8080`;

# Installation
Run `docker compose up -d`.

# External
* [PostgreSQL image](https://hub.docker.com/_/postgres);
* [Adminer image](https://hub.docker.com/_/adminer);