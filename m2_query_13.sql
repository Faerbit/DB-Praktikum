use praktikum
go

select Titel from Dokumente
left join DokumenteHeftenAnBeitr�ge on Dokumente.ID = DokumenteHeftenAnBeitr�ge.DokumentenID
where BeitragsID is null