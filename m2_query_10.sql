use praktikum
go

select top(1) Foren.Bezeichnung, x.AnzahlBeitraege, x.AnzahlSichtungen
from Foren
inner join (
select Diskussionen.ForumID, Diskussionen.AnzahlSichtungen, count(Diskussionen.Titel) as AnzahlBeitraege
from Beiträge
inner join Diskussionen on Beiträge.DiskussionsID = Diskussionen.ID
group by Diskussionen.ForumID, Diskussionen.AnzahlSichtungen) x on  [Foren].ID = x.ForumID
order by AnzahlBeitraege desc