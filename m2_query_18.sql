use praktikum
go

select Dokumente.Titel, Professoren.AkademischerTitel, Benutzer.Nachname, coalesce(nullif(Professoren.B�ro, NULL), 'nicht angegeben') as B�ro 
from Dokumente
inner join Professoren on Dokumente.Benutzer = Professoren.Nickname
join Benutzer on Professoren.Nickname = Benutzer.Nickname