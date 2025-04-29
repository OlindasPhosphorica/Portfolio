
USE library_management_system;


INSERT INTO Author (AuthorName) VALUES
('J.R.R. Tolkien'),
('Jane Austen'),
('Stephen King'),
('Agatha Christie'),
('George Orwell'),
('Gabriel Garcia Marquez'),
('Chimamanda Ngozi Adichie'),
('Haruki Murakami'),
('Margaret Atwood'),
('Neil Gaiman');


INSERT INTO Genre (GenreName) VALUES
('Fantasy'),
('Romance'),
('Horror'),
('Mystery'),
('Dystopian'),
('Magical Realism'),
('Literary Fiction'),
('Science Fiction'),
('Historical Fiction'),
('Graphic Novel');


INSERT IGNORE INTO Books (Title, AuthorID, GenreID, ISBN, PublicationYear) VALUES
('The Hobbit', 1, 1, '978-0547288226', 1937),
('Pride and Prejudice', 2, 2, '978-0151395318', 1813),
('The Shining', 3, 3, '978-0395321854', 1977),
('Murder on the Orient Express', 4, 4, '978-1063073618', 1934),
('Nineteen Eighty-Four', 5, 5, '978-0351524935', 1949),
('One Hundred Years of Solitude', 6, 6, '978-0461121084', 1967),
('Half of a Yellow Sun', 7, 7, '978-0377454925', 2006),
('Kafka on the Shore', 8, 7, '978-1425043617', 2002),
('The Handmaid''s Tale', 9, 5, '978-0385470418', 1985),
('Sandman Vol. 1: Preludes & Nocturnes', 10, 10, '978-1402245758', 1989),
('The Lord of the Rings', 1, 1, '978-0547926228', 1954),
('Emma', 2, 2, '978-0141485719', 1815);

INSERT IGNORE INTO MembershipTier (TierID, TierName, TierPrice) VALUES
(1, 'Basic', 0.00),
(2, 'Premium', 25.00);

INSERT IGNORE INTO Member (MemberID, FirstName, LastName, Address, Phone, Email, TierID) 
VALUES
(1, 'Alice', 'Smith', '123 Main St, Anytown, USA', '555-1234', 'alice.smith@example.com', 1),
(2, 'Bob', 'Johnson', '456 Oak Ave, Anytown, USA', '555-5678', 'bob.johnson@example.com', 2),
(3, 'Charlie', 'Brown', '789 Pine Ln, Anytown, USA', '555-9012', 'charlie.brown@example.com', 1),
(4, 'Diana', 'Miller', '246 Maple Dr, Anytown, USA', '555-3456', 'diana.miller@example.com', 2),
(5, 'Ethan', 'Davis', '135 Elm St, Anytown, USA', '555-7890', 'ethan.davis@example.com', 1),
(6, 'Fiona', 'Wilson', '678 Willow Rd, Anytown, USA', '555-2345', 'fiona.wilson@example.com', 2),
(7, 'George', 'Garcia', '901 Cedar Ct, Anytown, USA', '555-6789', 'george.garcia@example.com', 1),
(8, 'Hannah', 'Rodriguez', '321 Birch Ave, Anytown, USA', '555-0123', 'hannah.rodriguez@example.com', 2),
(9, 'Isaac', 'Martinez', '543 Spruce St, Anytown, USA', '555-4567', 'isaac.martinez@example.com', 1),
(10, 'Julia', 'Anderson', '876 Oak Pl, Anytown, USA', '555-8901', 'julia.anderson@example.com', 2);


INSERT IGNORE INTO BookPurchasing (MemberID, BookID, PurchaseCost, PurchaseDate) VALUES
(1, 1, 15.99, '2024-01-15'),
(2, 3, 22.50, '2024-02-20'),
(3, 5, 18.00, '2024-03-10'),
(4, 2, 12.00, '2024-04-05'),
(5, 4, 25.00, '2024-05-12'),
(6, 6, 28.00, '2024-06-18'),
(7, 8, 19.99, '2024-07-22'),
(8, 7, 21.00, '2024-08-01'),
(9, 9, 17.50, '2024-09-08'),
(10, 10, 30.00, '2024-10-29');


INSERT IGNORE INTO Reservation (BookID, MemberID, ReservationDate) VALUES
(2, 1, '2024-01-10'),
(4, 3, '2024-02-15'),
(1, 5, '2024-03-05'),
(3, 2, '2024-04-01'),
(5, 4, '2024-05-10'),
(6, 6, '2024-06-15'),
(8, 7, '2024-07-20'),
(7, 8, '2024-07-28'),
(9, 9, '2024-09-01'),
(10, 10, '2024-10-25');


INSERT IGNORE INTO Loans (BookID, MemberID, LoanDate, ReturnDate) VALUES
(1, 1, '2024-01-15', '2024-01-29'),
(3, 2, '2024-02-20', '2024-03-06'),
(5, 3, '2024-03-10', '2024-03-24'),
(2, 4, '2024-04-05', NULL),
(4, 5, '2024-05-12', '2024-05-26'),
(6, 6, '2024-06-18', NULL),
(8, 7, '2024-07-22', '2024-08-05'),
(7, 8, '2024-08-01', '2024-08-15'),
(9, 9, '2024-09-08', '2024-09-22'),
(10, 10, '2024-10-29', '2024-11-12');

INSERT IGNORE INTO OverdueFines (LoanID, FineAmount, PaymentDate) VALUES
(1, 2.50, '2024-01-31'),
(3, 1.00, NULL),
(5, 3.00, '2024-05-28'),
(7, 1.50, '2024-08-07'),
(9, 2.00, NULL);

INSERT IGNORE INTO Staff (StaffFirstName, StaffLastName) VALUES
('Alice', 'Johnson'),
('Bob', 'Williams'),
('Charlie', 'Brown'),
('Diana', 'Miller'),
('Ethan', 'Davis'),
('Fiona', 'Wilson'),
('George', 'Garcia'),
('Hannah', 'Rodriguez'),
('Isaac', 'Martinez'),
('Julia', 'Anderson');

-- 1. INNER JOIN: Get book titles and their authors.
SELECT b.Title, a.AuthorName
FROM Books b
INNER JOIN Author a ON b.AuthorID = a.AuthorID;

-- 2. LEFT JOIN: Get all members and the books they have borrowed (if any).
SELECT m.FirstName, m.LastName, b.Title
FROM Member m
LEFT JOIN Loans l ON m.MemberID = l.MemberID
LEFT JOIN Books b ON l.BookID = b.BookID;

-- 3. Subquery in SELECT: Get book titles and their genre names, including the average publication year of all books.
SELECT b.Title, g.GenreName, (SELECT AVG(PublicationYear) FROM Books) AS AveragePublicationYear
FROM Books b
JOIN Genre g ON b.GenreID = g.GenreID;

-- 4. Subquery in WHERE: Get the members who have borrowed books published before 1950.
SELECT DISTINCT m.FirstName, m.LastName
FROM Member m
WHERE m.MemberID IN (SELECT l.MemberID FROM Loans l JOIN Books b ON l.BookID = b.BookID WHERE b.PublicationYear < 1950);

-- 5. Aggregation with GROUP BY: Count the number of books in each genre.
SELECT g.GenreName, COUNT(b.BookID) AS NumberOfBooks
FROM Genre g
JOIN Books b ON g.GenreID = b.GenreID
GROUP BY g.GenreName;

-- 6. Aggregation with GROUP BY and HAVING: Find genres with more than 2 books.
SELECT g.GenreName, COUNT(b.BookID) AS NumberOfBooks
FROM Genre g
JOIN Books b ON g.GenreID = b.GenreID
GROUP BY g.GenreName
HAVING COUNT(b.BookID) > 2;

-- 7. ORDER BY and LIMIT: Get the titles of the 3 most recently published books.
SELECT Title, PublicationYear
FROM Books
ORDER BY PublicationYear DESC
LIMIT 3;

-- 8. WHERE with Logical Operators: Get members who live in Anytown, USA and have a Premium membership.
SELECT FirstName, LastName, Address, TierName
FROM Member m
JOIN MembershipTier mt ON m.TierID = mt.TierID
WHERE Address LIKE '%Anytown, USA%' AND mt.TierName = 'Premium';

-- 9. Built-in Function: Get the current date and time.
SELECT NOW();

-- 10. Complex Query: Get the title of the book, author name, member first name, and loan date for all books currently out on loan, ordered by loan date.
SELECT b.Title, a.AuthorName, m.FirstName, l.LoanDate
FROM Books b
JOIN Author a ON b.AuthorID = a.AuthorID
JOIN Loans l ON b.BookID = l.BookID
JOIN Member m ON l.MemberID = m.MemberID
WHERE l.ReturnDate IS NULL
ORDER BY l.LoanDate;

