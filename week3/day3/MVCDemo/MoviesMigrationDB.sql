SELECT * FROM Movie;
SELECT * FROM CastMember;
SELECT * FROM MovieCastMemberJunction;

INSERT INTO Movie VALUES
	('Star Wars Episode IV', '1983'), -- 1
	('Star Wars Episode V', '1984'),  -- 2
	('Star Wars Episode VI', '1985'); -- 3

INSERT INTO CastMember VALUES
	('Harrison Ford');  -- 1

INSERT INTO MovieCastMemberJunction VALUES
	(1, 1),
	(2, 1),
	(3, 1);