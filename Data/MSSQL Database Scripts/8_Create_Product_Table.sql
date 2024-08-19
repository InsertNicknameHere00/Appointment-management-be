USE haircutSalonDB;
GO

CREATE TABLE Product(
ProductID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
ProductName varchar(100) NOT NULL,
ProductDescription varchar(255) NULL,
Price decimal NOT NULL,
Quantity int NULL,
ProductImage varchar(255) NULL
)
