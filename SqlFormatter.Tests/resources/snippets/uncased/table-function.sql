create function [dbo].[taco] (@topping int)
returns table
as
return
(
	select rating
	from dbo.taco
	where id > 1;
)
