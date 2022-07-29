go
CREATE DATABASE ProductsDb

go
use ProductsDb

go
CREATE TABLE Cities (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name nvarchar(100),
    Code int
);

go
CREATE TABLE Brands (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name nvarchar(100),
    Description nvarchar(100),
	CityId UNIQUEIDENTIFIER,
  FOREIGN KEY (CityId) REFERENCES Cities (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

go
CREATE TABLE ProductTypes (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name nvarchar(20)
);

go
CREATE TABLE Products (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name nvarchar(100),
    Quantity int,
    TypeId UNIQUEIDENTIFIER,
	BrandId UNIQUEIDENTIFIER,
  FOREIGN KEY (TypeId) REFERENCES ProductTypes (Id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (BrandId) REFERENCES Brands (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

drop table Products
drop table ProductTypes
drop table Brands
drop table Cities