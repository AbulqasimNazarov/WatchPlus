
create database FilmsDb
use FilmsDb

create table Films(
	[Id] int primary key identity,
	[Name] nvarchar(200) not null,
	[Rate] nvarchar(200) null
)

insert into Films([Name], [Rate])
values ('THOR', 'Very good'),
		('Kingsman', 'Very good')

select * from Films