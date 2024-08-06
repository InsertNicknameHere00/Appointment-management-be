USE haircutSalonDB;
GO

CREATE TABLE SalonService(
	serviceID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	serviceTitle varchar(100) NOT NULL,
	serviceDescription varchar(255) NULL
);
GO