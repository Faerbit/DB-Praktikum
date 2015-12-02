USE [Praktikum]
GO

select Nickname from Benutzer where (Nickname like 's%' or Nickname like 'S%' or Nickname like 't%' or Nickname like 'T%')