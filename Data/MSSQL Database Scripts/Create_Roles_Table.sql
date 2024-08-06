USE haircutSalonDB;
GO

CREATE TABLE Roles(
	RoleID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	RoleName varchar(10) NOT NULL
);
GO