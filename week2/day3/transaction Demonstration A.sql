-- Demonstration A

-- Step 1: ensure you are connected to the adventureworks database

-- Step 2: Create a table to support the demonstrations
-- Clean up if the tables already exists
IF OBJECT_ID('dbo.SimpleOrderDetails','U') IS NOT NULL
	DROP TABLE dbo.SimpleOrderDetails;
IF OBJECT_ID('dbo.SimpleOrders','U') IS NOT NULL
	DROP TABLE dbo.SimpleOrders;
GO
CREATE TABLE dbo.SimpleOrders(
	orderid int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	custid int NOT NULL FOREIGN KEY REFERENCES SalesLT.Customer(customerid),
	empid int NOT NULL FOREIGN KEY REFERENCES SalesLT.Customer(customerid),
	orderdate datetime NOT NULL
);
GO
CREATE TABLE dbo.SimpleOrderDetails(
	orderid int NOT NULL FOREIGN KEY REFERENCES dbo.SimpleOrders(orderid),
	productid int NOT NULL FOREIGN KEY REFERENCES SalesLT.Product(productid),
	unitprice money NOT NULL,
	qty smallint NOT NULL,
 CONSTRAINT PK_OrderDetails PRIMARY KEY (orderid, productid)
);
GO

-- Step 3: Execute a multi-statement batch with error
-- NOTE: THIS STEP WILL CAUSE AN ERROR

BEGIN TRY
	INSERT INTO dbo.SimpleOrders(custid, empid, orderdate) VALUES (70,10,'2006-07-12');
	INSERT INTO dbo.SimpleOrders(custid, empid, orderdate) VALUES (70,10,'2006-07-15');
	INSERT INTO dbo.SimpleOrderDetails(orderid,productid,unitprice,qty) VALUES (1, 680,15.20,20);
	INSERT INTO dbo.SimpleOrderDetails(orderid,productid,unitprice,qty) VALUES (999,680,26.20,15);
END TRY
BEGIN CATCH
	SELECT ERROR_NUMBER() AS ErrNum, ERROR_MESSAGE() AS ErrMsg;
END CATCH;

-- Step 4: Show that even with exception handling,
-- partial success occurred and some rows were inserted
SELECT  orderid, custid, empid, orderdate
FROM dbo.SimpleOrders;
SELECT  orderid, productid, unitprice, qty
FROM dbo.SimpleOrderDetails;


-- Step 5: Clean up demonstration tables
IF OBJECT_ID('dbo.SimpleOrderDetails','U') IS NOT NULL
	DROP TABLE dbo.SimpleOrderDetails;
IF OBJECT_ID('dbo.SimpleOrders','U') IS NOT NULL
	DROP TABLE dbo.SimpleOrders;
