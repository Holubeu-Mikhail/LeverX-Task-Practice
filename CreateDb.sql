go
CREATE DATABASE ProductsDb

go
use ProductsDb

go
CREATE TABLE ProductTypes (
    Id int PRIMARY KEY  NOT NULL,
    Name nvarchar(20)
);

go
CREATE TABLE Products (
    Id int PRIMARY KEY  NOT NULL,
    Name nvarchar(100),
    Quantity int,
    TypeId int,
  FOREIGN KEY (TypeId) REFERENCES ProductTypes (Id) ON DELETE CASCADE ON UPDATE CASCADE
);