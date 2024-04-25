#docker volume rm microservices-mongo
#docker volume create microservices-mongo
#docker pull mongo:7.0.8-jammy
docker run --detach --publish 27017:27017 --env-file devops/.env.local.db --name microservicesdb --volume microservices-mongo:/data/db mongo:7.0.8-jammy