#!/bin/bash

until /opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P $SA_PASSWORD -Q 'SELECT name FROM master.sys.databases'; do
>&2 echo "SQL Server is starting up"
sleep 1
done

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P $SA_PASSWORD -Q "CREATE DATABASE [$IdentityServer_DB]"
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P $SA_PASSWORD -Q "CREATE DATABASE [$GraphQL_DB]"

/opt/mssql-tools/bin/sqlcmd -d $IdentityServer_DB -S sqlserver -U sa -P $SA_PASSWORD -i migrations-IdentityServerHost.sql
/opt/mssql-tools/bin/sqlcmd -d $GraphQL_DB -S sqlserver -U sa -P $SA_PASSWORD -i migrations-GraphQL.sql