use praktikum
go

select Titel from Dokumente
left join DokumenteHeftenAnBeiträge on Dokumente.ID = DokumenteHeftenAnBeiträge.DokumentenID
where BeitragsID is null