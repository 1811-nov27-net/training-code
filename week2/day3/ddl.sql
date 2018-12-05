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
	PizzaID INT NOT NULL,
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
	(1, 'Standard', 1, '2018-01-01');
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