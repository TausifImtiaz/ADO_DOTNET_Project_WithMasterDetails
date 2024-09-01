use master
create database ProductDB
go

use ProductDB
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100),
    Description NVARCHAR(255),
    Price DECIMAL,
    StockQuantity INT,
    ImagePath NVARCHAR(255)  -- Path to product image file
);
go


use ProductDB
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    CustomerName NVARCHAR(100),
    OrderDate DATETIME,
    TotalAmount DECIMAL
);
go

use ProductDB
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity INT,
    UnitPrice DECIMAL,
    TotalPrice DECIMAL
);
go

select * from Products
select * from Orders
select * from OrderDetails
