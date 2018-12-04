-- a comment

-- first step: make sure the dropdown is set to the right DB, not "master"
-- ("USE adventureworks;" is usually how you switch DBs, but Azure SQL DB doesn't support it)

-- basic SELECT query - all columns and all rows from a given table.
-- SalesLT is the schema name, Customer is the table name.
SELECT *
FROM SalesLT.Customer;

-- highlight a command and press F5 (execute / play button) to run that command.

