use praktikum
go

select Beiträge.Benutzer, Dokumente.Titel, Diskussionen.Titel from DokumenteHeftenAnBeiträge
inner join Beiträge on DokumenteHeftenAnBeiträge.BeitragsID = Beiträge.ID 
join Diskussionen on Beiträge.DiskussionsID = Diskussionen.ID
join Dokumente on DokumenteHeftenAnBeiträge.DokumentenID = Dokumente.ID