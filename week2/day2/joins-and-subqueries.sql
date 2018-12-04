-- joins combine data from two tables in one SELECT.
-- inner, left, right, full outer, and cross
-- (self for joining with same table)

-- explore the tables to see what columns to use
SELECT * FROM SalesLT.Customer;
SELECT * FROM SalesLT.CustomerAddress;
SELECT * FROM SalesLT.Address;

-- get the name and addresses of all customers in Houston.
SELECT c.FirstName, c.LastName, a.AddressLine1, a.AddressLine2, a.City, a.StateProvince
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.CustomerAddress AS ca ON c.CustomerID = ca.CustomerID
	INNER JOIN SalesLT.Address AS a ON ca.AddressID = a.AddressID
WHERE a.City = 'Houston' AND a.StateProvince = 'Texas';

-- find all customers with multiple addresses
SELECT c.FirstName, c.LastName, COUNT(ca.AddressID) AS AddressCount
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.CustomerAddress AS ca ON c.CustomerID = ca.CustomerID
GROUP BY c.CustomerID, c.FirstName, c.LastName
HAVING COUNT(ca.AddressID) > 1;

-- match up all customers and addresses, including
-- addresses with no customer and customers with no address
SELECT c.FirstName, c.LastName, a.AddressLine1, a.AddressLine2, a.City, a.StateProvince
FROM SalesLT.Customer AS c
	FULL OUTER JOIN SalesLT.CustomerAddress AS ca ON c.CustomerID = ca.CustomerID
	FULL OUTER JOIN SalesLT.Address AS a ON ca.AddressID = a.AddressID;


-- subqueries are an alternative way to accomplish what joins do.

-- we have operators like IN (checking membership in a list), NOT IN, EXISTS
-- ANY or ALL (checking for all true or any true in boolean list)
-- (in SQL, boolean is "BIT")

-- get the name of all customers in Houston, the subquery way.
SELECT FirstName, LastName
FROM SalesLT.Customer
WHERE CustomerID IN
(
	SELECT CustomerID
	FROM SalesLT.CustomerAddress
	WHERE AddressID IN
	(
		SELECT AddressID
		FROM SalesLT.Address
		WHERE City = 'Houston' AND StateProvince = 'Texas'
	)
);

-- every first name that is also a last name with join
SELECT DISTINCT c.FirstName
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.Customer AS a ON c.FirstName = a.LastName;
-- every first name that is also a last name with subquery
SELECT DISTINCT FirstName
FROM SalesLT.Customer
WHERE FirstName IN
(
	SELECT LastName
	FROM SalesLT.Customer
);

-- we also have set operators in SQL
-- UNION, INTERSECT, and EXCEPT (set difference)
-- these don't go "inside" a SELECT statement, they *combine* two SELECT statements.

-- all the first names and last names starting with A
SELECT FirstName FROM SalesLT.Customer WHERE FirstName LIKE 'A%'
UNION
SELECT LastName FROM SalesLT.Customer WHERE LastName LIKE 'A%';

-- all the set operators "by default" remove duplicates.
-- but they all have "ALL" versions - UNION ALL, INTERSECT ALL, and EXCEPT ALL

-- with union all, we see the duplicates. the ALL versions are always faster.
-- (no loop to remove the duplicates)
SELECT FirstName FROM SalesLT.Customer WHERE FirstName LIKE 'A%'
UNION ALL
SELECT LastName FROM SalesLT.Customer WHERE LastName LIKE 'A%';

-- UNION gives all records in EITHER of the two result sets ("addition")
-- INTERSECT gives all records in BOTH result sets
-- EXCEPT gives all records in the FIRST BUT NOT in the SECOND result set.

-- every first name that is also a last name with set operators
SELECT FirstName FROM SalesLT.Customer
INTERSECT
SELECT LastName FROM SalesLT.Customer;

-- people with same first name and last name
SELECT * FROM SalesLT.Customer WHERE FirstName = LastName;

-- NULL represents a missing piece of data
-- all comparisons with NULL come out false, even = NULL.
-- all things combined with null come out null.

-- use "IS NULL" to check for something being null

-- we have COALESCE function which takes in a list, returns a list
-- with all the NULLs converted to something else.
SELECT CustomerID,
	Title,
	COALESCE(FirstName, '') + ' ' + COALESCE(MiddleName, '') + ' ' + COALESCE(LastName, '') AS [Full Name],
	Suffix
FROM SalesLT.Customer;
