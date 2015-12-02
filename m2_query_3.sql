use praktikum
go

IF OBJECT_ID('dbo.beitraege') IS NOT NULL
  DROP FUNCTION beitraege
GO

create function beitraege
(@start datetime, @ende datetime)
returns @rtable table
(
Mitteilung text not null
)
as
begin
	insert into @rtable
	select [Mitteilung] from [Beiträge] 
	return
end;
go


declare @start datetime;
set @start = '1970-10-29T10:00:00';
declare @end datetime;
set @end = '2100-10-29T10:00:00';

select * from beitraege(@start, @end);