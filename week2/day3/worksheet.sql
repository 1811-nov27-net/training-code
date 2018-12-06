-- Show the first name and the email address of customer with CompanyName 'Bike World'
SELECT FirstName, EmailAddress
FROM SalesLT.Customer
WHERE CompanyName = 'Bike World';

-- Show the CompanyName for all customers with an address in City 'Dallas'.
SELECT CompanyName
FROM SalesLT.Customer
WHERE CustomerID IN
(
	SELECT ca.CustomerID
	FROM SalesLT.CustomerAddress AS ca
		INNER JOIN SalesLT.Address AS a ON ca.AddressID = a.AddressID
	WHERE a.City = 'Dallas'
);

-- How many items with ListPrice more than $1000 have been sold?
SELECT SUM(sod.OrderQty)
FROM SalesLT.SalesOrderDetail AS sod
	INNER JOIN SalesLT.Product AS p ON sod.ProductID = p.ProductID
WHERE p.ListPrice > 1000;

-- Give the CompanyName of those customers with orders over $100000. Include the subtotal plus tax plus freight.
SELECT c.CompanyName, soh.SubTotal, soh.TaxAmt, soh.Freight, soh.TotalDue
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID
WHERE soh.TotalDue > 100000;

-- Find the number of left racing socks ('Racing Socks, L') ordered by CompanyName 'Riding Cycles'.
SELECT SUM(sod.OrderQty)
FROM SalesLT.SalesOrderDetail AS sod
	INNER JOIN SalesLT.Product AS p ON sod.ProductID = p.ProductID
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON sod.SalesOrderID = soh.SalesOrderID
	INNER JOIN SalesLT.Customer AS c ON soh.CustomerID = c.CustomerID
WHERE p.Name = 'Racing Socks, L' AND c.CompanyName = 'Riding Cycles';

-- Show the SalesOrderID and the UnitPrice for every customer order WHERE only one item is ordered.
SELECT SalesOrderID, UnitPrice
FROM SalesLT.SalesOrderDetail
WHERE OrderQty = 1;

-- List the product name and the CompanyName for all Customers who ordered ProductModel 'Racing Socks'.
SELECT p.Name, c.CompanyName
FROM SalesLT.Product AS p
	INNER JOIN SalesLT.ProductModel AS pm ON p.ProductModelID = pm.ProductModelID
	INNER JOIN SalesLT.SalesOrderDetail AS sod ON p.ProductID = sod.ProductID
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON sod.SalesOrderID = soh.SalesOrderID
	INNER JOIN SalesLT.Customer AS c ON soh.CustomerID = c.CustomerID
WHERE pm.Name = 'Racing Socks';

-- Show the product description for culture 'fr' for product with ProductID 736.
SELECT pd.Description
FROM SalesLT.ProductDescription AS pd
	INNER JOIN SalesLT.ProductModelProductDescription AS pmpd ON pmpd.ProductDescriptionID = pd.ProductDescriptionID
	INNER JOIN SalesLT.Product AS p ON p.ProductModelID = pmpd.ProductModelID
WHERE pmpd.Culture = 'fr' and p.ProductID = 736;

-- Use the SubTotal value in SalesOrderHeader to list orders from the largest to the smallest. For each order show the CompanyName and the SubTotal and the total weight of the order.
SELECT c.CompanyName, soh.SubTotal, SUM(sod.OrderQty * p.Weight) AS TotalWeight
FROM SalesLT.Customer AS c
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID
	INNER JOIN SalesLT.SalesOrderDetail AS sod ON soh.SalesOrderID = sod.SalesOrderID
	INNER JOIN SalesLT.Product AS p ON sod.ProductID = p.ProductID
GROUP BY soh.SalesOrderID, c.CompanyName, soh.SubTotal
ORDER BY soh.SubTotal DESC;

-- How many products in ProductCategory 'Cranksets' have been sold to an address in 'London'?
SELECT SUM(sod.OrderQty)
FROM SalesLT.SalesOrderDetail AS sod
	INNER JOIN SalesLT.Product AS p ON sod.ProductID = p.ProductID
	INNER JOIN SalesLT.ProductCategory AS pc ON p.ProductCategoryID = pc.ProductCategoryID
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON sod.SalesOrderID = soh.SalesOrderID
	INNER JOIN SalesLT.Address AS a ON soh.ShipToAddressID = a.AddressID
WHERE pc.Name = 'Cranksets' and a.City = 'London';

-- For every customer with a 'Main Office' in Dallas show AddressLine1 of the 'Main Office' and AddressLine1 of the 'Shipping' address - if there is no shipping address leave it blank. Use one row per customer.
SELECT a_mo.AddressLine1 AS MainOfficeAddress, a_s.AddressLine1 AS ShippingAddress
FROM SalesLT.Address AS a_mo
	INNER JOIN SalesLT.CustomerAddress AS ca_mo ON
		a_mo.AddressID = ca_mo.AddressID AND ca_mo.AddressType = 'Main Office'
	LEFT JOIN SalesLT.CustomerAddress AS ca_s ON
		ca_mo.CustomerID = ca_s.CustomerID AND ca_s.AddressType = 'Shipping'
	LEFT JOIN SalesLT.Address AS a_s ON ca_s.AddressID = a_s.AddressID
WHERE a_mo.City = 'Dallas';

-- Show the best selling item by value.
SELECT TOP(1) p.ProductID, p.Name, SUM(sod.OrderQty * sod.UnitPrice) AS TotalOrderValue
FROM SalesLT.Product AS p
	INNER JOIN SalesLT.SalesOrderDetail AS sod ON p.ProductID = sod.ProductID
GROUP BY p.ProductID, p.Name
ORDER BY SUM(sod.OrderQty * sod.UnitPrice) DESC;

-- Show the total order value for each CountryRegion. List by value with the highest first.
SELECT a.CountryRegion, SUM(soh.TotalDue) AS TotalOrderValue
FROM SalesLT.Address AS a
	INNER JOIN SalesLT.SalesOrderHeader AS soh ON a.AddressID = soh.BillToAddressID
GROUP BY a.CountryRegion
ORDER BY SUM(soh.TotalDue) DESC;

-- Find the best customer in each region.
SELECT sq.CountryRegion, sq.CustomerID, c.FirstName, c.MiddleName, c.LastName, sq.TotalOrderValue
FROM SalesLT.Customer AS c
	INNER JOIN
	(
		SELECT
			a.CountryRegion,
			c.CustomerID,
			SUM(soh.TotalDue) AS TotalOrderValue,
			ROW_NUMBER() OVER (PARTITION BY a.CountryRegion ORDER BY SUM(soh.TotalDue) DESC) AS Rank
		FROM SalesLT.SalesOrderHeader AS soh
			INNER JOIN SalesLT.Address AS a ON soh.BillToAddressID = a.AddressID
			INNER JOIN SalesLT.Customer AS c ON soh.CustomerID = c.CustomerID
		GROUP BY a.CountryRegion, c.CustomerID
	) AS sq
		ON c.CustomerID = sq.CustomerID AND sq.Rank = 1;