use praktikum
go

select top(1) Benutzer, count(Benutzer) as BenutzerCount from [Beiträge] group by Benutzer order by BenutzerCount desc