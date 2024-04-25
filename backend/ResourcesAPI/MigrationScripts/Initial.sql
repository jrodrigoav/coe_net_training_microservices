CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'resources') THEN
        CREATE SCHEMA resources;
    END IF;
END $EF$;

CREATE TABLE resources."Resources" (
    "Id" uuid NOT NULL,
    "Name" character varying(254) NOT NULL,
    "DateOfPublication" timestamp with time zone NOT NULL,
    "Author" character varying(254),
    "Tags" text[] NOT NULL,
    "Type" character varying(50) NOT NULL,
    "Description" character varying(500) NOT NULL,
    CONSTRAINT "PK_Resources" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Resources_Name_Description" ON resources."Resources" ("Name", "Description");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240425232529_Initial', '8.0.4');

COMMIT;

