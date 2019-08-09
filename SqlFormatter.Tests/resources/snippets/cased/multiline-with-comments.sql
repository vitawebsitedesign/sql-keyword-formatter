-- comment
-- extra comment
SELECT *
FROM dbo.taco
WHERE id = 1;

SELECT *	-- comment
FROM dbo.taco -- comment
WHERE id = 1;

SELECT *
FROM dbo.taco	-- comment
WHERE id = 1;	-- comment

SELECT *		-- comment
FROM dbo.taco
WHERE id = 1;	-- comment

SELECT *	/* comment */
FROM dbo.taco /* comment */
WHERE id = 1;

SELECT *
FROM dbo.taco	/* comment */
WHERE id = 1;	/* comment */

SELECT *		/* comment */
FROM dbo.taco
WHERE id = 1;	/* comment */
