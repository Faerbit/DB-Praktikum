use praktikum
go

select Nickname, count(Nickname) as AnzahlBeitraege from Benutzer
inner join [Beitr�ge] on Benutzer.Nickname = Beitr�ge.Benutzer
order by Nickname