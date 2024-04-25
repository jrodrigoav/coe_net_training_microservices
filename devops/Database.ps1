$IMAGE="postgres:16.2-alpine3.19";
docker pull $IMAGE;
#docker volume create postgres-data;
docker run --detach --publish 5432:5432 --env-file devops/.env.local.db --name microservicesdb --volume mpostgres-data:/var/lib/postgresql/data $IMAGE;