go
CREATE DATABASE ProductsDb

go
use ProductsDb

go
CREATE TABLE ProductTypes (
    TypeID int PRIMARY KEY  NOT NULL,
    Name nvarchar(20)
);

go
CREATE TABLE Products (
    ProductID int PRIMARY KEY  NOT NULL,
    Name nvarchar(100),
    Quantity int,
    TypeID int,
  FOREIGN KEY (TypeID) REFERENCES ProductTypes (TypeID) ON DELETE CASCADE ON UPDATE CASCADE
);