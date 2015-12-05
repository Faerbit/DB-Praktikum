use praktikum
go

select Benutzer.Nickname, Benutzer.Vorname, Benutzer.Nachname
from Benutzer
inner join
(select Benutzer.Vorname, count(Benutzer.Vorname) as VornameCount
from Benutzer
where Benutzer.Vorname != ''
group by Benutzer.Vorname
having count(Vorname) > 1) x
on x.Vorname = Benutzer.Vorname
group by Benutzer.Nickname, Benutzer.Vorname, Benutzer.Nachname
order by Benutzer.Vorname