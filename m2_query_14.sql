use praktikum
go

select * from
(select Benutzer.Nickname , 'Student' as Rolle from Benutzer
inner join Studenten on Studenten.Nickname = Benutzer.Nickname) s
union all
(select Benutzer.Nickname , 'Professor' as Rolle from Benutzer
inner join Professoren on Professoren.Nickname = Benutzer.Nickname)
union all
(select Benutzer.Nickname , 'Mitarbeiter' as Rolle from Benutzer
where Benutzer.Nickname not in (select Studenten.Nickname from Studenten) 
and Benutzer.Nickname not in (select Professoren.Nickname from Professoren))
order by Rolle, Nickname