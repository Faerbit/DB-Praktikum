use praktikum
go

select avg(cast(x.AnzahlBeitraege as float)) as Durschnitt from (select Beitr�ge.DiskussionsID, count(Beitr�ge.DiskussionsID) as AnzahlBeitraege from Beitr�ge
group by DiskussionsID) x 