# Frontend

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.2.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.


##Database setup
You need docker destop https://www.docker.com/products/docker-desktop/
Once installed you need to setup the image with the postgres database and the pgadmin utility to be able to run queries
//TODO  add instructions for the image location and loading

once the image is running you can log into the database using pgadmin with the user and password added into the image at build time

then you need to recreate the database schema using the initial.sql from each project, located under the MigrationScripts folder on each project.

##Setup Secrets for each project
on each project you need to right click -> Manage User Secrets which will open a json file that needs to be filled with the following values
from the DB docker image
{
  "ConnectionStrings": {
    "MicroservicesDB": "User ID=xxxxx;Password=xxxxx;Host=localhost;Port=5432;Database=postgres;Pooling=true;Connection Lifetime=0;"
  }
}


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

##DB data setup and management
This can be done via the admin service installed along the db server in docker (pgadmin)
once the docker service is running you can access it via (please use the creds setup in the docker image file)

http://localhost:8080/login

