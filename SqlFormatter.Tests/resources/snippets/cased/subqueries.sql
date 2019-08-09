SELECT topping
FROM dbo.taco t
WHERE EXISTS (SELECT 1 FROM dbo.taco x WHERE x.rating = t.rating);

SELECT topping
FROM dbo.taco t
WHERE EXISTS (
	SELECT 1
	FROM dbo.taco x
	WHERE x.rating = t.rating
);

SELECT topping
FROM dbo.taco t
WHERE EXISTS
(
	SELECT 1
	FROM dbo.taco x
	WHERE x.rating = t.rating
);
