CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'renting') THEN
        CREATE SCHEMA renting;
    END IF;
END $EF$;

CREATE TABLE renting."Rents" (
    "Id" uuid NOT NULL,
    "ResourceId" uuid NOT NULL,
    "ClientId" uuid NOT NULL,
    "RegistrationDate" date NOT NULL,
    "ReturnDate" date NOT NULL,
    "Returned" boolean NOT NULL,
    "CopyId" uuid NOT NULL,
    CONSTRAINT "PK_Items" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240426210122_Initial', '8.0.4');

COMMIT;

