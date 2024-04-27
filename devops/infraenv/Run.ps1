docker container stop infraenv;
docker container rm infraenv;
docker run --interactive --tty --env-file .env.local --name infraenv infraenv:latest bash;
