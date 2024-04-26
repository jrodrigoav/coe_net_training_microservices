CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'inventory') THEN
        CREATE SCHEMA inventory;
    END IF;
END $EF$;

CREATE TABLE inventory."Items" (
    "Id" uuid NOT NULL,
    "Available" boolean NOT NULL,
    "ResourceId" uuid NOT NULL,
    CONSTRAINT "PK_Items" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240426210218_Initial', '8.0.4');

COMMIT;

