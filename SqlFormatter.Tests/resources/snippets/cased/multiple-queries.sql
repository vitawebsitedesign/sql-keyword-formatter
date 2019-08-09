SELECT *
FROM dbo.apple
WHERE id = 1;

SELECT *
FROM dbo.taco
WHERE id <> 1;

SELECT *
FROM dbo.carrot
WHERE id IN (1, 2);

SELECT *
FROM dbo.carrot
WHERE id IS NULL;

SELECT *
FROM dbo.carrot
WHERE id IS NOT NULL;