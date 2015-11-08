alter table [Student]
add constraint fk_studenten
	foreign key (BenutzerID)
	references [Benutzer](ID) on delete
	cascade