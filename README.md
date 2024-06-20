# Microservices Training .NET

## Current Versions of the packages
- Backend
    - .NET 8

> You can find in [here](_docs/api.md) the documentation of the APIs.

### ClientsAPI URLs
[https://clientsapi.127.0.0.1.nip.io:7214]
[https://clientsapi.127.0.0.1.nip.io]
[https://clientsapi.localtest.me]


## To Launch the started dependencies
 docker compose -f .\docker-compose.yaml --project-name microsvcs up --detach  
 
 
 
##Git repo

Please install git source control app from https://git-scm.com/download


Make sure you have a github account https://github.com



Please fork from
https://github.com/jrodrigoav/coe_net_training_microservices


clone the fork from your repo with the command

git clone https://github.com/your-github-user/coe_net_training_microservices.git




##Database setup
You need docker destop https://www.docker.com/products/docker-desktop/
Once installed you need to setup the image with the postgres database and the pgadmin utility to be able to run queries
//TODO  add instructions for the image location and loading
In order to create the postgress image which will be loaded into docker you need to:
- Edit the file "docker-compose.yaml"  and change the user and password in the following vars
  PGADMIN_DEFAULT_EMAIL
  PGADMIN_DEFAULT_PASSWORD
  POSTGRES_PASSWORD

please make note that these will be used later and are marked as xxxxx in future references on this doc.

- run the command 
docker compose -f .\docker-compose.yaml --project-name microsvcs up --detach

once the image is running you can log into the database using pgadmin with the user and password added into the image at build time

then you need to recreate the database schema using the initial.sql from each project (please check DB data setup section, below), located under the MigrationScripts folder on each project.

#Visual Studio
Please open the coe_net_microservices.sln located in coe_net_training_microservices\backend
and try building


##Setup Secrets for each project
on each project you need to right click -> Manage User Secrets which will open a json file that needs to be filled with the following values
from the DB docker image
{
  "ConnectionStrings": {
    "MicroservicesDB": "User ID=xxxxx;Password=xxxxx;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;"
  }
}

##DB data setup and management
This can be done via the admin service installed along the db server in docker (pgadmin)
once the docker service is running you can access it via (please use the creds setup in the docker image file)

http://localhost:8080/login




##API (Back end)
In order to run the Backend you need to verify that each of the projects has the correct secrets
(right click on the project and then set the correct values) 

--ClientAPI
--InvevtoryAPI
--RentingAPI
--ResourcesAPI


{
  "ConnectionStrings": {
    "MicroservicesDB": "User ID=xxxxx;Password=xxxxx;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;"
  }
}

Then please also check the settings on the appsettings.Development.json of each project

--ClientAPI
--RentingAPI
--ResourcesAPI
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
}



--InvetoryAPI  (this one has an extra setting)
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ResourceAPISettings": {
    "Url": "http://localhost:5183/api/resources"
  }
}

Now please build the project and launch it, do not forget to also launch the docker image of the db server.
Also please check that the projects to start include all the projects in the solution (please use right click and configure start up projects).
you should be able to check the services starting by using a URL similar to this (change the port depending on the service, more info can be
found in the launchSettings.json file of each service)
http://localhost:5183

you should be able to test DB to API connectivity by calling 
http://localhost:5183/api/resources/list  (GET) you should see no errors



##ANGULAR front end
In the command line you can go to frontend/bookstore folder and run the following commands

npm install
npm run start

that should give no errors and lauch a server with the UI, which you should be able to access using this url
http://localhost:4200/



##REACT front end
In the command line you can go to frontend/bookstore-react folder and run the following commands

npm install
npm run build
npm run dev

that should give no errors and lauch a server with the UI, which you should be able to access using this url
http://localhost:5173/