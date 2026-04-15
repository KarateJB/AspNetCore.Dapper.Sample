/* MS SQL SERVER */

CREATE TABLE Folks
(
    Id UNIQUEIDENTIFIER DEFAULT NEWID(),
    Name NVARCHAR(50),
    Birthday DATETIMEOFFSET,
    Address NVARCHAR(100),
    Phone VARCHAR(20),
    EmploymentOn DATETIMEOFFSET,
    CONSTRAINT PK_Folks PRIMARY KEY NONCLUSTERED (Id)
)

/* POSTGRESQL */

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
create table "Folks"
(
    "Id"    uuid default uuid_generate_v4() not null
            constraint "PK_Folks" primary key,
    "Name"       varchar(50),
    "Birthday"   timestamptz,
    "Address"   varchar(100),
    "Phone"      varchar(20),
    "EmploymentOn" timestamptz
);

