select topping
from dbo.taco t
where exists (select 1 from dbo.taco x where x.rating = t.rating);

select topping
from dbo.taco t
where exists (
	select 1
	from dbo.taco x
	where x.rating = t.rating
);

select topping
from dbo.taco t
where exists
(
	select 1
	from dbo.taco x
	where x.rating = t.rating
);
