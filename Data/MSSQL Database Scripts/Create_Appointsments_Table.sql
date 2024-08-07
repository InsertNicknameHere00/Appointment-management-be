USE haircutSalonDB;
GO

CREATE TABLE Appointsments(
appointsmentID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
appointsmentStatus int NOT NULL,
appointsmentStartDate DateTime2,
appointsmentEndDate DateTime2,
userID int FOREIGN KEY REFERENCES Users(UserID) not null
);	