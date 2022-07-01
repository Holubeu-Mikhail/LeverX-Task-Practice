go
CREATE DATABASE ProductsDb

go
use ProductsDb

go
CREATE TABLE Towns (
    Id int PRIMARY KEY  NOT NULL,
    Name nvarchar(100),
    Code int
);

go
CREATE TABLE Brands (
    Id int PRIMARY KEY  NOT NULL,
    Name nvarchar(100),
    Description nvarchar(100),
	TownId int,
  FOREIGN KEY (TownId) REFERENCES Towns (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

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
	BrandId int,
  FOREIGN KEY (TypeId) REFERENCES ProductTypes (Id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (BrandId) REFERENCES Brands (Id) ON DELETE CASCADE ON UPDATE CASCADE
);