use praktikum
go

select top(3) avg(cast(datalength(Mitteilung) as float)) as laenge, Benutzer from Beitr�ge
group by Benutzer
order by laenge desc