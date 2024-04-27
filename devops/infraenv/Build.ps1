docker container stop infraenv;
docker container rm infraenv;
docker image rm infraenv:latest;
docker build --tag infraenv:latest .;