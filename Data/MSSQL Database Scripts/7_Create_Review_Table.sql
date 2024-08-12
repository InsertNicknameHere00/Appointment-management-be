USE haircutSalonDB;
GO

CREATE TABLE Review(
reviewID int IDENTITY(1,1) primary key not null,
userID int foreign key references Users(UserID) not null,
serviceID int foreign key references AdminServices(Id),
reviewDescription varchar(255)
);