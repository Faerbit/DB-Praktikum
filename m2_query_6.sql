use praktikum
go

select ID, Titel from Diskussionen where len(Titel) > 15 order by ID, Titel