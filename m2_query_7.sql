use praktikum
go

select Studenten.Nickname,  Benutzer.Vorname, Benutzer.Nachname 
from Studenten 
inner join Benutzer on Studenten.Nickname = Benutzer.Nickname
where (Studenten.Nickname like 's%' or Studenten.Nickname like 'S%' or Studenten.Nickname like 't%' or Studenten.Nickname like 'T%');