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
	select [Mitteilung] from [Beiträge] where [Änderungsdatum] between @start and @ende;
	return
end;
go


declare @start datetime;
set @start = '2015-09-23T14:12:35';
declare @end datetime;
set @end = '2015-09-25T18:12:35';

select * from beitraege(@start, @end);