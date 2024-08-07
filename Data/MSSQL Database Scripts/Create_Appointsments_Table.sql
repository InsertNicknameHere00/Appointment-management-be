USE haircutSalonDB;
GO

CREATE TABLE Appointments(
appointmentID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
appointmentStatus int NOT NULL,
appointmentStartDate DateTime2,
appointmentEndDate DateTime2,
userID int FOREIGN KEY REFERENCES Users(UserID) not null
);	