#!/bin/bash

# Wait 60 seconds for SQL Server to start up by ensuring that 
# calling SQLCMD does not return an error code, which will ensure that sqlcmd is accessible
# and that system and user databases return "0" which means all databases are in an "online" state

DBSTATUS=1
ERRCODE=1
i=0

for i in {1..60};
do
	DBSTATUS=$(/opt/mssql-tools18/bin/sqlcmd -S localhost -h -1 -t 1 -U sa -P $MSSQL_SA_PASSWORD -C -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
	ERRCODE=$?
	sleep 1
done 

if [ $DBSTATUS -ne 0 ] || [ $ERRCODE -ne 0 ]; then
	echo "SQL Server took more than 60 seconds to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

# Run the setup script to create the DB and the schema in the DB
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -C -d master -i setup-database.sql