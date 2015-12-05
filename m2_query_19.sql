use praktikum
go

with flat(OberID , UnterID, Tiefe) as
(
	select OberforumID, ID as UnterID, 1 as Tiefe
	from Foren
	where OberforumID is not null

	union all

	select flat.OberID, Foren.ID AS UnterID, Flat.Tiefe +1
	from flat
	join Foren on Foren.OberforumID = Flat.UnterID
)
select Bezeichnung, Tiefe from flat 
join Foren on flat.UnterID = Foren.ID
order by OberID, Tiefe