use praktikum
go

select Dokumente.Titel, Professoren.AkademischerTitel, Benutzer.Nachname, coalesce(nullif(Professoren.Büro, NULL), 'nicht angegeben') as Büro 
from Dokumente
inner join Professoren on Dokumente.Benutzer = Professoren.Nickname
join Benutzer on Professoren.Nickname = Benutzer.Nickname