
create database FilmsDb
use FilmsDb

create table Films(
	[Id] int primary key identity,
	[Name] nvarchar(200) not null,
	[Rate] nvarchar(200) null
)

insert into Films([Name], [Rate])
values ('X-Man', 'Good')

select * from Films