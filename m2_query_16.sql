use praktikum
go

select avg(cast(datalength(Mitteilung) as float)) as laenge, Benutzer from Beitr�ge
group by Benutzer
having avg(cast(datalength(Mitteilung) as float)) < 50

