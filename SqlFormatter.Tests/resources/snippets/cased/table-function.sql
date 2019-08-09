CREATE FUNCTION [dbo].[taco] (@topping INT)
RETURNS TABLE
AS
RETURN
(
	SELECT rating
	FROM dbo.taco
	WHERE id > 1;
)
