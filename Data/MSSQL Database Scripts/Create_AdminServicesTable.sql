USE haircutSalonDB;
GO

CREATE TABLE AdminServices(
	id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	serviceID int NOT NULL FOREIGN KEY REFERENCES SalonService(serviceID),
	userID int NOT NULL FOREIGN KEY REFERENCES Users(UserID),
	serviceDuration int NULL,
	servicePrice money NULL
);
GO