use praktikum
go

select Nickname, count(Nickname) as AnzahlBeitraege from Benutzer
inner join [Beiträge] on Benutzer.Nickname = Beiträge.Benutzer
order by Nickname