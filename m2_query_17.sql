use praktikum
go

select Beitr�ge.Benutzer, Dokumente.Titel, Diskussionen.Titel from DokumenteHeftenAnBeitr�ge
inner join Beitr�ge on DokumenteHeftenAnBeitr�ge.BeitragsID = Beitr�ge.ID 
join Diskussionen on Beitr�ge.DiskussionsID = Diskussionen.ID
join Dokumente on DokumenteHeftenAnBeitr�ge.DokumentenID = Dokumente.ID