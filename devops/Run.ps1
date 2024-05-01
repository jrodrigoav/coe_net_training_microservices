docker container stop infraenv;
docker container rm infraenv;
$path="C:\Proyectos\CoE_NET\coe_net_training_microservices\infrastructure\environments\acloudguru\aws\vpc"
docker run --interactive --tty --env-file .env.local --name infraenv --volume ${path}:/workdir/aws infraenv:latest bash;

#eksctl create cluster -f kluster.yaml
#eksctl upgrade cluster -f kluster.yaml

#kubectl create namespace web-sandbox
#kubectl apply -f web-sandbox.yaml
