-- a comment

-- first step: make sure the dropdown is set to the right DB, not "master"
-- ("USE adventureworks;" is usually how you switch DBs, but Azure SQL DB doesn't support it)

-- basic SELECT query - all columns and all rows from a given table.
-- SalesLT is the schema name, Customer is the table name.
SELECT *
FROM SalesLT.Customer;

-- highlight a command and press F5 (execute / play button) to run that command.

-- SELECT query/statement/command - read some info from the DB.
-- or, do *anything* that should return/print a value.

-- don't even need to access the DB.
SELECT 1;
SELECT 1 + 1;

-- two clauses in the SELECT statement so far - SELECT clause and FROM clause.
-- FROM clause specifies what table or tables we are looking inside.
-- and SELECT clause specifies the columns of our "result set"
SELECT CustomerID, Title, FirstName, MiddleName, LastName, Suffix
FROM SalesLT.Customer;
-- only get the columns you ask for.

-- we can do calculations from the table's columns
-- and name or "alias" our result columns with "AS", even with spaces, using "" or []
SELECT CustomerID, Title, FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name], Suffix
FROM SalesLT.Customer;

SELECT *
FROM SalesLT.Product;

SELECT Name, ProductNumber, ProductID, ListPrice * 1.08 AS [Price With Tax], ListPrice - StandardCost AS Profit
FROM SalesLt.Product;

SELECT Color, Size
FROM SalesLT.Product; -- returns 295 rows

-- filter duplicate rows out with DISTINCT in the SELECT clause
SELECT DISTINCT Color, Size
FROM SalesLT.Product; -- returns 68 rows

SELECT DISTINCT ProductId, Color, Size
FROM SalesLT.Product; -- returns 295 rows

-- table aliases
-- important when we start selecting from multiple tables, have to distinguish,
-- would rather do so with a short name.
SELECT p.Color, p.Size
FROM SalesLT.Product AS p;

-- WHERE clause
-- allows filtering of rows based on a condition (before any calculated columns in the SELECT clause)
-- in SQL, string literals must be with single quote. double quote is for names of things with spaces in them
SELECT *
FROM SalesLT.Customer
WHERE FirstName = 'Brian';

-- we have boolean operators AND, OR
-- we can group them with parentheses
-- we have comparisons like = for equals
SELECT *
FROM SalesLT.Customer
WHERE FirstName = 'Brian' AND LastName = 'Goldstein';

-- not-equals is <> or !=
SELECT *
FROM SalesLT.Customer
WHERE FirstName = 'Brian' AND LastName != 'Goldstein';

-- we have ordered comparison with < <= > >=
-- of numbers, dates, and also strings ("dictionary"/lexicographic ordering)

-- exercise: all customers with first names that start with B
SELECT *
FROM SalesLT.Customer
WHERE FirstName >= 'B' AND FirstName < 'C';

-- SQL supports, not regex, but some amount of its own pattern matching with LIKE operator.
--   %     any number of characters
--  [abc]  either a, b, or c
--  there's others
SELECT *
FROM SalesLT.Customer
WHERE FirstName LIKE 'B[ar]%';

-- some string functions
SELECT LEFT('123456789', 4); -- returns '1234'
SELECT RIGHT('123456789', 4); -- return '6789'
SELECT LEN('123456789'); -- returns 9, the length
SELECT SUBSTRING('123456789', 3, 5); -- start index and length of substring.
-- string indexes are 1-based in SQL unfortunately
SELECT REPLACE('Hello world', 'world', 'Nick'); -- return Hello Nick
SELECT CHARINDEX(' ', 'Hello world'); -- return 6 (the first index of that character in the string)

-- data types
--   numeric
--     tinyint, smallint, int, bigint
--       (1)       (2)     (4)    (8) byte
--     float, real, decimal
--        use decimal for basically all floating-point stuff
--          decimal as a type accepts parameters
--               decimal(10, 3) means 10 digits, with 3 of them after the decimal place.
--                i.e. 0000000.000
--        for currency values, we have money type

--  string
--     char(10) (fixed length character array)
--     varchar(10)  (variable length up to 10 character array)
--     nchar(10)   ("national" aka Unicode char array)
--     nvarchar(1)   (Unicode variable length)
--  for the varchars we can also pass "MAX" as parameter e.g. NVARCHAR(MAX).
--     no reason to not use NVARCHAR all the time.

-- string literals are technically VARCHAR: 'Hello'
-- we can make Unicode string literals (NVARCHAR) like: N'Hello'

-- we also have BINARY type and VARBINARY for storing binary data directly in the DB
-- e.g. ThumbNailPhoto column in Product table.
SELECT * FROM SalesLT.Product;


-- date and time data types
-- date, time, datetime
-- don't use datetime, use datetime2 because it has more range
-- datetimeoffset (for intervals of time like "5 minutes")

-- we've seen SELECT, FROM, and WHERE
-- the other clauses are GROUP BY, HAVING, and ORDER BY.

-- group all rows by first name, and count the number of duplicates per name.
SELECT FirstName, COUNT(FirstName) AS Count
FROM SalesLT.Customer
GROUP BY FirstName;

-- WHERE clause runs *before* the GROUP BY clause.
-- so we can't filter on any conditions that depend on the grouping.

-- e.g. if i want only the FirstNames that *have* duplicates, i can't use WHERE,
--   because the grouping hasn't happened yet, there's nothing to compare
SELECT FirstName, COUNT(FirstName) AS Count
FROM SalesLT.Customer
WHERE COUNT(FirstName) > 1 -- error
GROUP BY FirstName;

-- COUNT as an aggregate function, this means it operates on many values and returns just one value.
-- (unlike LEN for example, takes one string and returns a number)
-- you use COUNT with GROUP BY.

-- HAVING is for conditions that run AFTER the GROUP BY.
-- (aggregate functions must be in HAVING, not WHERE.)

-- all first names besides Andrew that have any duplicates, and how many there are.
SELECT FirstName, COUNT(FirstName) AS Count
FROM SalesLT.Customer
WHERE FirstName != 'Andrew'
GROUP BY FirstName
HAVING COUNT(FirstName) > 1;

-- ORDER BY sorts the result set and it's the last thing that runs.

-- all products sorted by cost, cheapest first.
SELECT *
FROM SalesLT.Product
ORDER BY StandardCost ASC;

-- ascending order (ASC) is the default so you can skip it
-- descending order is DESC

-- all products sorted by color alphabetically and by most expensive cost.
SELECT *
FROM SalesLT.Product
ORDER BY Color, StandardCost DESC;

-- execution order of a select statement:
--   FROM
--   WHERE
--   GROUP BY
--   HAVING
--   ORDER BY
--   SELECT
--  (in order of writing it, except for SELECT.)
