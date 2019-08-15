DECLARE @str VARCHAR(MAX) = 'select select ';

DECLARE @str VARCHAR(MAX) = 'select
select
select';

SELECT *
FROM dbo.A
WHERE Col = 'select'
OR Col = ' select '
OR Col = 'select '
OR Col LIKE '%select'
OR Col LIKE '%select%'
OR Col LIKE 'select%';
