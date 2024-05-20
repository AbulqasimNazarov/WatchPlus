create database WatchPlusDB
use WatchPlusDB


create table TVShows(
	[Id] int primary key identity,
	[Name] nvarchar(200) not null,
	[Description] nvarchar(200) not null,
	[Category] nvarchar(200) not null,
	[Star] nvarchar(200) not null,
	[Rate] nvarchar(200) not null,
	[Image] nvarchar(200) null,
	[VideoTrailer] nvarchar(200) null
)

drop table TVShows




select * from TVShows


INSERT INTO TVShows ([Name], [Description], [Category], [Star], [Rate], [Image], [VideoTrailer]) 
VALUES (
    'Planet Earth II', 
    'David Attenborough returns with a new wildlife documentary that shows life in a variety of habitats.', 
    'TV Mini Series', 
    'David Attenborough', 
    'Good', 
    '/Assets/IMG/planetEarth.jpg', 
    'https://www.youtube.com/embed/c8aFcHFu8QM'
),
(
    'Cosmos: A Spacetime Odyssey', 
    'An exploration of our discovery of the laws of nature and coordinates in space and time.', 
    'TV Mini Series', 
    'Neil deGrasse Tyso', 
    'Good', 
    '/Assets/IMG/COSMOS.jpg', 
    'https://www.youtube.com/embed/_erVOAbz420'
);
