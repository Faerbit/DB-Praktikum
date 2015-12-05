use praktikum
go

select avg(cast(x.AnzahlBeitraege as float)) as Durschnitt from (select Beiträge.DiskussionsID, count(Beiträge.DiskussionsID) as AnzahlBeitraege from Beiträge
group by DiskussionsID) x 