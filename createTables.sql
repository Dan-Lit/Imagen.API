create table usuario
(
userID int identity(1,1) primary key,
username varchar(20) not null
);

create table image 
(
imageID varchar(50) primary key, 
imageURL varchar(110),
tagged bit default 0, --boolean
processed bit default 0, --boolean
userID int foreign key references usuario
);

create table tag 
(
tagID varchar(20) primary key
);

create table imagetagconfig 
(
imageID varchar(50) foreign key references image,
tagID varchar(20) foreign key references tag,
primary key (imageiD, tagID)
);

