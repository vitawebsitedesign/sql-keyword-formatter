CREATE PROCEDURE [dbo].[taco] (@lettuce INT, @tomato INT)
AS
SELECT 1
FROM dbo.apple
WHERE id = 1 AND topping = 1

CREATE PROCEDURE [dbo].[taco] (@lettuce INT, @tomato INT)
AS
BEGIN
SELECT 1
FROM dbo.apple
WHERE id = 1 AND topping = 1
END