create procedure [dbo].[taco] (@lettuce int, @tomato int)
as
begin
select 1
from dbo.apple
where id = 1 AND topping = 1;
select 1
from dbo.apple
where id = 1 AND topping = 1
end
