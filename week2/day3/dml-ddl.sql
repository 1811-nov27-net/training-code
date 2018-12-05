-- DML
-- Database Manipulation Language
-- the subset of SQL which accesses and modifies individual rows.
-- SELECT, INSERT, UPDATE, DELETE, TRUNCATE TABLE

-- SELECT is by far the more complicated

SELECT * FROM SalesLT.Address;

-- INSERT
-- simple insert with all columns except the identity ones.
INSERT INTO SalesLT.Address VALUES
('123 Main St', NULL, 'Dallas', 'Texas', 'United States', '12345', 'C8DF3BD9-48F0-4654-A8DD-14A67A84D3C7', GETUTCDATE());

-- better insert, more readable - be explicit about column names.
-- allows relying on defaults for nullable columns, for rowguids, etc.
INSERT INTO SalesLT.Address(AddressLine1, City, StateProvince, CountryRegion, PostalCode)
VALUES
('123 Main St', 'Dallas', 'Texas', 'United States', '12345'),
(REPLACE('123 Main St', '123', '456'), 'Dallas', 'Texas', 'United States', '12345');

-- there are ways to do bulk inserts from things like CSV files
-- BULK INSERT SalesLT.Address FROM 'data.csv' WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n')


INSERT INTO SalesLT.Address(AddressLine1, City, StateProvince, CountryRegion, PostalCode)
	SELECT AddressLine1, City, StateProvince, CountryRegion, REVERSE(PostalCode)
	FROM SalesLT.Address
	WHERE ModifiedDate > '2018'; -- YEAR(ModifiedDate) >= 2018

-- we have temporary table variables we can use with #
SELECT AddressLine1, City, StateProvince, CountryRegion, PostalCode
INTO #my_table
FROM SalesLT.Address
WHERE ModifiedDate > '2018';

INSERT INTO SalesLT.Address(AddressLine1, City, StateProvince, CountryRegion, PostalCode)
	SELECT * FROM #my_table;

-- UPDATE
SELECT * FROM SalesLT.Address WHERE YEAR(ModifiedDate) >= 2018;

-- for every recently modified row, update the address line 2 and set the postal code
-- equal to the most recently modified row's postal code.
UPDATE SalesLT.Address
SET AddressLine2 = 'Apt 45',
	PostalCode =
	(
		SELECT TOP(1) PostalCode FROM SalesLT.Address ORDER BY ModifiedDate DESC
	)
WHERE YEAR(ModifiedDate) >= 2018;

-- DELETE

-- delete every row in the table (slow way, one by one)
-- DELETE FROM SalesLT.Address;

DELETE FROM SalesLT.Address
WHERE ModifiedDate > '2018';

-- TRUNCATE TABLE deletes every row in the table all at once, fast way.
-- (table itself still exists, but empty.)
-- TRUNCATE TABLE SalesLT.Address;
