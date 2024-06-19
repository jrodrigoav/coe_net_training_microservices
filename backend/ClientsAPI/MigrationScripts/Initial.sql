CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'clients') THEN
        CREATE SCHEMA clients;
    END IF;
END $EF$;

CREATE TABLE clients."Clients" (
    "Id" uuid NOT NULL,
    "FirstName" character varying(150) NOT NULL,
    "LastName" character varying(150) NOT NULL,
    "Email" text NOT NULL,
    CONSTRAINT "PK_Clients" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Clients_Email" ON clients."Clients" ("Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240425224032_Initial', '8.0.4');

COMMIT;

