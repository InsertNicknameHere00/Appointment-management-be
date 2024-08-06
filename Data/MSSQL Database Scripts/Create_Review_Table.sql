create table Review(
reviewID Integer primary key not null,
userID Integer foreign key references Users(UserID) not null,
serviceID Integer foreign key references AdminServices(id),
reviewDescription varchar(255)
)