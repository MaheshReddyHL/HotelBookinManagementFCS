Create Database HotelRoomBooking
Use HotelRoomBooking
create table CustomerLogin
(
CustomerId int primary key identity(1,1),
Name varchar(255),
Password varchar(255),
UserName varchar(50) unique,
)
insert into CustomerLogin values('Hemanth','hem123','chachi420');
insert into CustomerLogin values('Harsh','123','Harsh123');
 
 select * from CustomerLogin

create table AdminLogin
(
AdminId int primary key NOT NULL,
UserName varchar(255),
Password varchar(255),
)
insert into AdminLogin values(2,'Omkar','om123')
 
select * from AdminLogin
-------------------------------------------------------------------------------------------------------------------------
create table HotelRoom (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber NVARCHAR(10) NOT NULL,
    RoomType NVARCHAR(50) NOT NULL,
    AvailabilityStatus BIT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Capacity INT NOT NULL
	)

Insert into HotelRoom (RoomNumber, RoomType, AvailabilityStatus, Price, Capacity)
 Values
('101', 'Single', 1, 500.00,  1),     -- Single room
('102', 'Double', 1, 600.00,  2),     -- Double room
('103', 'Suite',  1,  800.00, 4),      -- Suite room
('104', 'Deluxe', 1, 1000.00, 5),     -- Deluxe room
('105', 'Deluxe', 0, 500.00, 1);
select * from HotelRoom

create table RoomService (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceCharge DECIMAL(10, 2) NOT NULL
);
Insert into RoomService (ServiceName, ServiceCharge)
Values
('Room Cleaning', 80.00),    -- Room cleaning service
('Laundry', 60.00),          -- Laundry service
('Food Service', 100.00),     -- Food service to room
('Internet Service', 50.00); -- Internet service in the room

create table RoomCharge (
    ChargeID INT PRIMARY KEY IDENTITY(1,1),
    RoomID INT FOREIGN KEY REFERENCES HotelRoom(RoomID),
    ServiceID INT FOREIGN KEY REFERENCES RoomService(ServiceID),
    TotalCharge DECIMAL(10, 2) NOT NULL
);
Insert into RoomCharge (RoomID, ServiceID, TotalCharge)
Values
(1, 1, 150.00),    -- Room 101 with Room Cleaning Service
(2, 2, 175.00),    -- Room 102 with Laundry Service
(3, 3, 375.00),    -- Room 103 with Food Service
(4, 4, 410.00);    -- Room 104 with Internet Service

Create table RoomBooking (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES CustomerLogin(CustomerID),
	RoomID INT FOREIGN KEY REFERENCES HotelRoom(RoomID),
	CustomerName NVARCHAR(100) NOT NULL,
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
	Price DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(50) NOT NULL
);
CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    Customername VARCHAR(100),
    Phonenumber VARCHAR(100),
    EmailId VARCHAR(110),
    Roomtype VARCHAR(50),
    CheckInDate DATETIME,
    CheckOutDate DATETIME,
    TotalCharge DECIMAL(18, 0)
);

Insert into RoomBooking ( RoomID, CustomerName, CheckInDate, CheckOutDate, Status,price)
values
( 1, 'Raja',  '2024-10-01', '2024-10-05', 'Confirmed',500.0),   -- Booking for Nithesh (Customer 1)
( 2, 'Ramesh','2024-10-10', '2024-10-15', 'Confirmed',600.0),   -- Booking for Arulselvam (Customer 2)
( 3,  'Abdul','2024-11-01', '2024-11-05', 'Pending',800.0),     -- Booking for Arun (Customer 3)
( 4,  'Ram','2024-12-01', '2024-12-10', 'Cancelled',1000.0);   -- Booking for Dasu (Customer 4)

Create table Staff (
    StaffID INT PRIMARY KEY IDENTITY(1,1),
    StaffName NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(15) NOT NULL,
    StaffRole NVARCHAR(50) NOT NULL
);
Insert into Staff (StaffName,PhoneNumber,StaffRole)
Values
('Abdul','9233445566','Cleaner'),  -- Cleaner staff
('Omkar','8233045566','Manager'),        -- Manager staff
('Rahul','7233445566','Receptionist'), -- Receptionist staff
('Mahesh','8233445566','Chef');         -- Chef staff

Create table CustomerPayment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),    -- Primary key for the payment
    BookingID INT FOREIGN KEY REFERENCES RoomBooking(BookingID), -- Foreign key linking to RoomBooking
	CustromerName NVARCHAR(100) NOT NULL,       --Customer Name
    PaymentAmount DECIMAL(10, 2) NOT NULL,      -- Amount paid by the customer
    PaymentMethod NVARCHAR(50) NOT NULL,        -- Method of payment (e.g., Credit Card,Debit Card, Upi, PayPal, Cash)
    PaymentStatus NVARCHAR(50) NOT NULL         -- Status of the payment (e.g., Completed, Pending, Failed)
);
Insert into CustomerPayment ( CustromerName, PaymentAmount, PaymentMethod,PaymentStatus)
Values
('Raja',500.00, 'Credit Card', 'Booking Completed'),  -- Payment for Booking 1
( 'Ramesh',600.00, 'UPI','Booking Pending'),      -- Payment for Booking 2
( 'Abdul',800.00, 'Debit Card','Booking Failed'),    -- Payment for Booking 3
( 'Ram',1000.00, 'Cash','Booking Completed');           -- Payment for Booking 4
select * from CustomerPayment







