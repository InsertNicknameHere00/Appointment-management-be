USE haircutSalonDB;
GO

CREATE TABLE Appointments(
Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
Status int NOT NULL,
StartDate DateTime2,
EndDate DateTime2,
userId int FOREIGN KEY REFERENCES Users(UserID) not null,
serviceId int FOREIGN KEY REFERENCES SalonService(serviceID) not null
);	
GO