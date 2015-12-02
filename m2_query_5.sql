use praktikum
go

select top(1) x.BenutzerCount, x.Benutzer from (select Benutzer, count(Benutzer) as BenutzerCount from [Beiträge] group by Benutzer) as x order by x.BenutzerCount desc