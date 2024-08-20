CREATE TABLE PromoCodes (
    PromoCodeID INT PRIMARY KEY IDENTITY(1,1),
    Code VARCHAR(50) NOT NULL UNIQUE,
    DiscountPercentage INT NOT NULL, 
    ExpiryDate DATETIME NOT NULL,
    IsActive BIT DEFAULT 1
);