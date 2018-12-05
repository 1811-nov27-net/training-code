-- DDL
-- Data Definition Language
-- for defining columns and tables, altering the structure of our database.
-- does not have access to individual rows or their contents

-- CREATE, ALTER, DROP

--CREATE DATABASE Pizza;

CREATE SCHEMA PS;
GO

-- GO separates "batches" of commands
-- the red squigglies will complain if you don't put it for some commands that want to be in their own batch.
-- even though you're only going to send that command by itself with highlighting anyway.

DROP TABLE PS.Pizza;

-- columns are nullable by default (aka NULL)
-- but you should always be explicit because nullable columns are more unusual.

CREATE TABLE PS.Pizza
(
	PizzaID INT IDENTITY(1,1) NOT NULL, -- an identity column that starts at 1 and increments by 1
	Name NVARCHAR(100) NOT NULL,
	CrustID INT NOT NULL,
	ModifiedDate DATETIME2 NOT NULL,
	CreatorName NVARCHAR(100) NULL
);

-- we can add constraints within CREATE TABLE
-- or afterwards in ALTER TABLE

ALTER TABLE PS.Pizza
	ADD CONSTRAINT PK_Pizza_PizzaID PRIMARY KEY (PizzaID);

ALTER TABLE PS.Pizza
	ADD CONSTRAINT U_Pizza_Name UNIQUE (Name);

CREATE TABLE PS.Crust
(
	CrustID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE
);

ALTER TABLE PS.Pizza
	ADD CONSTRAINT FK_Pizza_Crust_CrustID FOREIGN KEY (CrustID) REFERENCES PS.Crust (CrustID);

-- the only thing that actually accomplishes is make the database throw an error
-- if you violate referential integrity
-- (either delete or update a referenced PK value, or, insert or update an
--   FK value that doesn't exist in the referenced table)


ALTER TABLE PS.Pizza
	DROP CONSTRAINT FK_Pizza_Crust_CrustID;
	
-- ON DELETE CASCADE (and ON UPDATE CASCADE) specify that if the referenced PK value is deleted/updated,
--   then, this table should automatically get corresponding changes.

-- e.g. - deleting a crust would automatically delete all pizzas that use it instead of throwing an error
ALTER TABLE PS.Pizza
	ADD CONSTRAINT FK_Pizza_Crust_CrustID FOREIGN KEY (CrustID) REFERENCES PS.Crust (CrustID)
		ON DELETE CASCADE
		ON UPDATE CASCADE;

-- constraints:
--    PRIMARY KEY
--    UNIQUE
--    FORIEGN KEY
--    NOT NULL (and NULL, kind of a fake constraint)
--    DEFAULT
--    CHECK

-- we can add columns with ALTER TABLE also.
--   (these columns must be nullable or have defaults if there's already rows in the table.)

-- adds an "Active" boolean column to enable "deleting" a row without actually deleting it.
ALTER TABLE PS.Crust
	ADD Active BIT NOT NULL DEFAULT(1);

-- CHECK constraint is for any condition you want on the value.
--ALTER TABLE PS.Pizza DROP CONSTRAINT CK_Pizza_DateNotInFuture;
ALTER TABLE PS.Pizza
	ADD CONSTRAINT CK_Pizza_DateNotInFuture CHECK (ModifiedDate <= GETDATE() + '00:00:01');
-- add one second to avoid default value failing the check


INSERT INTO PS.Crust (CrustID, Name) VALUES
	(1, 'Plain');
INSERT INTO PS.Pizza (PizzaID, Name, CrustID, ModifiedDate) VALUES
	(1, 'Standard', 1, '2018-01-01'); -- error, can't insert explicit values to identity column
INSERT INTO PS.Pizza (Name, CrustID, ModifiedDate) VALUES
	('Standard', 1, '2018-01-01');
SELECT * FROM PS.Crust;

-- demo cascade on delete - also deletes the pizza using this crust
DELETE FROM PS.Crust;

SELECT * FROM PS.Pizza;

ALTER TABLE PS.Pizza
	ADD CONSTRAINT D_Pizza_ModifiedDate DEFAULT GETUTCDATE() FOR ModifiedDate;
INSERT INTO PS.Pizza (PizzaID, Name, CrustID) VALUES
	(1, 'Standard', 1);

-- we have "computed columns" in SQL
ALTER TABLE PS.Crust
	DROP COLUMN InternalName;
ALTER TABLE PS.Crust
	ADD InternalName AS (CONVERT(VARCHAR(20), CrustID) + '_' + Name);
-- that one is recalculated on every query

SELECT * FROM PS.Crust;

ALTER TABLE PS.Pizza
	ADD InternalName AS (CONVERT(VARCHAR(20), PizzaID) + '_' + Name) PERSISTED;
-- that one (PERSISTED) is calculated once and then stored until
-- the row is updated

SELECT * FROM PS.Pizza;

-- aggregate functions
--    COUNT
--    SUM
--    AVG (average)
--    MIN
--    MAX

-- Views
-- views are not tables, but they can be treated like read-only tables.
-- they are like "computed columns" but for a whole table.

SELECT * FROM PS.Pizza;
SELECT * FROM PS.Crust;

GO
CREATE VIEW PS.ActivePizzas
AS
SELECT CrustID, Name, InternalName
FROM PS.Crust
WHERE Active = 1;

SELECT * FROM PS.ActivePizzas;

DELETE FROM PS.ActivePizzas;


-- user-defined functions
GO
CREATE FUNCTION PS.FirstPizzaName()
RETURNS NVARCHAR(100)
AS
BEGIN
	DECLARE @ret NVARCHAR(100);

	SELECT TOP(1) @ret = Name
	FROM PS.Pizza;

	RETURN @ret;
END

SELECT PS.FirstPizzaName(); -- returns 'Standard' (in my case)

-- function to return the name of the first crust with the given "active" status.
CREATE FUNCTION PS.FirstCrustName(@status BIT)
RETURNS NVARCHAR(100)
AS
BEGIN
	DECLARE @ret NVARCHAR(100);

	SELECT TOP(1) @ret = Name
	FROM PS.Crust
	WHERE Active = @status;

	RETURN @ret;
END

SELECT PS.FirstCrustName(1); -- returns 'Plain' (in my case)
SELECT PS.FirstCrustName(0); -- returns NULL

-- user-defined functions can be table-valued (return value is a whole table)
GO
CREATE FUNCTION PS.AllNames()
RETURNS TABLE
AS
	RETURN SELECT Name FROM PS.Pizza UNION SELECT Name FROM PS.Crust;

SELECT * FROM PS.AllNames(); -- returns all pizza and crust names in the DB as a 1-column table

-- functions are not allowed to modify the database / insert rows, etc.
-- they are read-only access

-- if we want to modify the database in this encapsulated kind of way, 
-- we use "stored procedures".

-- functions can be used in SELECT clause and things like that, but procedures can't.
GO
DROP PROCEDURE PS.ChangePizzaNames;
GO
CREATE PROCEDURE PS.ChangePizzaNames(@newname NVARCHAR(100))
AS
BEGIN
	BEGIN TRY
		IF (EXISTS (SELECT * FROM PS.Pizza)) -- if there are any rows in the table
		BEGIN
			UPDATE PS.Pizza
			SET Name = @newname;
		END
		ELSE
		BEGIN
			RAISERROR('No pizzas found', 16, 1)
		END
	END TRY
	BEGIN CATCH
		PRINT 'unable to change pizza names for reason:'
		PRINT ERROR_MESSAGE();
	END CATCH
END

EXECUTE PS.ChangePizzaNames 'Great Pizza';

TRUNCATE TABLE PS.Pizza;

EXECUTE PS.ChangePizzaNames 'Great Pizza';

-- you can use functions in select statements and where clauses etc.
-- but you can't do that with procedures
SELECT * FROM PS.Crust WHERE Name = PS.FirstCrustName();

-- transaction
-- a transaction is a set of statements which follow the ACID properties
--   A atomic / atomicity
--      a transaction must be all-or-nothing. must not be allowed to partially succeed and then fail.
--      if there is a failure, we must be returned to the original state.
--   C consistent / consistency
--      a transaction is not allowed to violate DB constraints or referential integrity
--   I isolated / isolation
--      the behavior of one transaction should not interfere with that of another transaction
--      each transaction should be able to think of itself as the only one currently running
--      we compromise on "isolated" part of ACID heavily in practice
--   D durable / durability
--      effects/result must not only be in "volatile memory", they must be persisted to disk
--      or some permanent storage.

-- in SQL Server, we have four isolation levels to give us flexibility in isolation
--  why? because full isolation is lower performance (and higher possibility of deadlock)
--     read_uncommitted, read_committed (default), repeatable, serializable

-- every SQL statement by default is already a transaction in itself -
-- an error part-way through will result in rolling back anything changed up to that point.

-- with explicit "BEGIN TRANSACTION" etc, we can have multi-statement transactions.


-- Triggers

-- trigger that will update the "modified date" any time someone updates a row,
-- automatically.
GO
CREATE TRIGGER PS.TR_Pizza ON PS.Pizza
AFTER UPDATE
AS
BEGIN
	-- in a trigger, you have access to two special tables
	-- called inserted and deleted.
	-- (like git, we think of updates as an insert and a delete.)
	UPDATE PS.Pizza
	SET ModifiedDate = GETDATE()
	WHERE PizzaID IN (SELECT PizzaID FROM inserted);
END

UPDATE PS.Pizza
SET Name = 'New Pizza';

-- triggers also support INSTEAD OF and BEFORE where i put AFTER
