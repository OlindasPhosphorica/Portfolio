USE library_management_system;


CREATE TABLE IF NOT EXISTS Author (
    AuthorID INT PRIMARY KEY AUTO_INCREMENT,
    AuthorName VARCHAR(255) NOT NULL
);


CREATE TABLE IF NOT EXISTS Genre (
    GenreID INT PRIMARY KEY AUTO_INCREMENT,
    GenreName VARCHAR(255) NOT NULL
);


CREATE TABLE IF NOT EXISTS Books (
    BookID INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    AuthorID INT,
    GenreID INT,
    ISBN VARCHAR(20) UNIQUE, 
    PublicationYear INT,  
    FOREIGN KEY (AuthorID) REFERENCES Author(AuthorID),
    FOREIGN KEY (GenreID) REFERENCES Genre(GenreID)
);

CREATE TABLE IF NOT EXISTS MembershipTier (
    TierID INT PRIMARY KEY AUTO_INCREMENT,
    TierName VARCHAR(255) NOT NULL,
    TierPrice DECIMAL(10, 2) NOT NULL
);

CREATE TABLE IF NOT EXISTS Member (
    MemberID INT PRIMARY KEY AUTO_INCREMENT,
    TierID INT,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Address VARCHAR(255),
    Phone VARCHAR(20),  
    Email VARCHAR(255) UNIQUE, 
    FOREIGN KEY (TierID) REFERENCES MembershipTier(TierID)
);

CREATE TABLE IF NOT EXISTS BookPurchasing (
    RequestID INT PRIMARY KEY AUTO_INCREMENT,
    MemberID INT,
    BookID INT,
    PurchaseCost DECIMAL(10, 2) NOT NULL,
    PurchaseDate DATE,  
    FOREIGN KEY (MemberID) REFERENCES Member(MemberID),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);


CREATE TABLE IF NOT EXISTS Reservation (
    ReservationID INT PRIMARY KEY AUTO_INCREMENT,
    BookID INT,
    MemberID INT,
    ReservationDate DATE,   
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (MemberID) REFERENCES Member(MemberID)
);


CREATE TABLE IF NOT EXISTS Loans (
    LoanID INT PRIMARY KEY AUTO_INCREMENT,
    BookID INT,
    MemberID INT,
    LoanDate DATE NOT NULL,
    ReturnDate DATE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (MemberID) REFERENCES Member(MemberID)
);


CREATE TABLE IF NOT EXISTS OverdueFines (
    FineID INT PRIMARY KEY AUTO_INCREMENT,
    LoanID INT,
    FineAmount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATE,  
    FOREIGN KEY (LoanID) REFERENCES Loans(LoanID)
);


CREATE TABLE IF NOT EXISTS Staff (
    StaffID INT PRIMARY KEY AUTO_INCREMENT,
    StaffFirstName VARCHAR(255) NOT NULL,
    StaffLastName VARCHAR(255) NOT NULL
);