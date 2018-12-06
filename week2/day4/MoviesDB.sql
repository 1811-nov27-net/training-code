CREATE DATABASE MoviesDB;
GO
CREATE SCHEMA Movies;
GO

CREATE TABLE Movies.Movie
(
	ID INT IDENTITY NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	GenreID INT NOT NULL,
	CONSTRAINT PK_Movies_ID PRIMARY KEY (ID)
);

--DROP TABLE Movies.Genre;
CREATE TABLE Movies.Genre
(
	ID INT IDENTITY NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Genre_ID PRIMARY KEY (ID)
);

ALTER TABLE Movies.Movie
	ADD CONSTRAINT FK_Movies_Genre_GenreID FOREIGN KEY (GenreID) REFERENCES Movies.Genre (ID);

ALTER TABLE Movies.Movie
	ADD ReleaseDate DATETIME2 NULL;

INSERT INTO Movies.Genre (Name) VALUES
	('Action'), -- 1
	('Drama');  -- 2

INSERT INTO Movies.Movie (Name, GenreID) VALUES
	('Indiana Jones', (SELECT ID FROM Movies.Genre WHERE Name = 'Action')),
	('Star Wars', 1); -- or this way, just assuming what i know the ID to be
