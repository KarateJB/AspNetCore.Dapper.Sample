# AspNetCore.Dapper.Sample

ASP.NET Core x Dapper sample to synchorize a Microsoft SQL Server table's data to PostgresSQL table.

## Stacks

- dotnet 8
- Dapper

## Support Database

- Microsoft SQL Server
- PostgreSQL

## Get Started

1. Run the SQL in `init/init_table.sql` to create tables. 
2. Update the "ConnectionStrings" in `src/AspNetCore.Dapper.WebApi/appsettings.Development.json` and run the application.
3. Use the curl commands in `init/gen_data.sh` to create/update a record to database (SQL Server by default).

