use praktikum
go

select Titel
from Dokumente
inner join Professoren on Dokumente.Benutzer = Professoren.Nickname
where Dokumente.Benutzer = 'ritz'