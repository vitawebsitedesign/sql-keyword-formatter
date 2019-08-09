select *
from dbo.apple
where id = 1;

select *
from dbo.taco
where id <> 1;

select *
from dbo.carrot
where id in (1, 2);

select *
from dbo.carrot
where id is null;

select *
from dbo.carrot
where id is not null;