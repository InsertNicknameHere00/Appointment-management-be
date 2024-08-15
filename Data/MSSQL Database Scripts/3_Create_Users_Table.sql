USE haircutSalonDB;
GO

CREATE TABLE Users(
	UserID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FirstName varchar(100) NOT NULL,
	LastName varchar(100) NOT NULL,
	Email varchar(50) NOT NULL UNIQUE,
	PhoneNumber varchar(10) NOT NULL,
	PasswordHash varchar(255) NOT NULL,
	VerificationStatus varchar(50) NOT NULL,
	RoleID int NOT NULL FOREIGN KEY REFERENCES Roles(RoleID),
);
GO