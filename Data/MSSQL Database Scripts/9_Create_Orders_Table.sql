CREATE TABLE Orders(
OrderId int IDENTITY(1,1) PRIMARY KEY NOT NULL,
UserId int FOREIGN KEY REFERENCES Users(UserID) NOT NULL,
OrderDate DateTime NOT NULL,
OrderAddress varchar(255) NOT NULL,
TotalPrice money NOT NULL
)