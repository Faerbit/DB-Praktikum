use praktikum
go

select top(1) Foren.Bezeichnung, x.AnzahlBeitraege, x.AnzahlSichtungen
from Foren
inner join (
select Diskussionen.ForumID, Diskussionen.AnzahlSichtungen, count(Diskussionen.Titel) as AnzahlBeitraege
from Beitr�ge
inner join Diskussionen on Beitr�ge.DiskussionsID = Diskussionen.ID
group by Diskussionen.ForumID, Diskussionen.AnzahlSichtungen) x on  [Foren].ID = x.ForumID
order by AnzahlBeitraege desc