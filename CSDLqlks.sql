CREATE DATABASE QLKS;
GO

USE QLKS;
GO

-- Bảng Users (Người dùng/Nhân viên)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);

-- Bảng Rooms (Phòng)
CREATE TABLE Rooms (
    RoomID CHAR(10) PRIMARY KEY,
    RoomType NVARCHAR(50) NOT NULL,
    RoomStatus NVARCHAR(20), -- Available, Occupied, Maintenance, Reserved
    Capacity INT NOT NULL,
    BasePrice DECIMAL(10, 3) NOT NULL
);

-- Bảng Customers (Khách hàng)
CREATE TABLE Customers (
    CustomerID CHAR(10) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    CitizenID NVARCHAR(20), -- CMND/CCCD
    Phone NVARCHAR(20)
);

-- Bảng Bookings (Đặt phòng)
CREATE TABLE Bookings (
    BookingID CHAR(10) PRIMARY KEY,
    CustomerID CHAR(10) FOREIGN KEY REFERENCES Customers(CustomerID),
    BookingDate DATETIME DEFAULT GETDATE(),
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL
);

-- Bảng BookingDetails (Chi tiết đặt phòng)
CREATE TABLE BookingDetails (
    BookingID CHAR(10) FOREIGN KEY REFERENCES Bookings(BookingID),
    RoomID CHAR(10) FOREIGN KEY REFERENCES Rooms(RoomID),
    RoomPrice DECIMAL(10, 3) NOT NULL,
    CheckInDate DATETIME,
    CheckOutDate DATETIME,
    PRIMARY KEY (BookingID, RoomID)
);

-- Bảng Services (Dịch vụ)
CREATE TABLE Services (
    ServiceID CHAR(10) PRIMARY KEY,
    ServiceName NVARCHAR(100) NOT NULL,
    Price DECIMAL(10, 3) NOT NULL
);

-- Bảng ServiceUsage (Sử dụng dịch vụ)
CREATE TABLE ServiceUsage (
    ServiceUsageID CHAR(10) PRIMARY KEY,
    ServiceUsageDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 3) NOT NULL,
    TotalService DECIMAL(10, 3) NOT NULL,
    PaymentID CHAR(10) FOREIGN KEY REFERENCES Payments(PaymentID),
    RoomID CHAR(10) FOREIGN KEY REFERENCES Rooms(RoomID)
);

-- Bảng ServiceDetails (Chi tiết sử dụng dịch vụ)
CREATE TABLE ServiceDetails (
    ServiceUsageID CHAR(10) FOREIGN KEY REFERENCES ServiceUsage(ServiceUsageID),
    ServiceID CHAR(10) FOREIGN KEY REFERENCES Services(ServiceID),
    Quantity INT NOT NULL,
    PRIMARY KEY (ServiceUsageID, ServiceID)
);

-- Bảng Payments (Thanh toán)
CREATE TABLE Payments (
    PaymentID CHAR(10) PRIMARY KEY,
    BookingID CHAR(10) FOREIGN KEY REFERENCES Bookings(BookingID),
    PaymentDate DATETIME DEFAULT GETDATE()
);

-- Thêm dữ liệu vào bảng Users
INSERT INTO Users (Username, Password, Phone) VALUES 
('KIMNGHI', 'KN123',  '0123456789'),
('NGOCVU', 'NV123',  '0987654321'),
('TRUNGKIEN', 'TK123',  '0123987654');

-- Thêm dữ liệu vào bảng Rooms
INSERT INTO Rooms (RoomID, RoomType, RoomStatus, Capacity, BasePrice) VALUES 
('R001', 'Standard', 'Available', 2, 100.000),
('R002', 'VIP', 'Occupied', 2, 150.000),
('R003', 'Luxurious', 'Available', 4, 300.000),
('R004', 'Standard', 'Occupied', 2, 100.000);

-- Thêm dữ liệu vào bảng Customers
INSERT INTO Customers (CustomerID, FullName, CitizenID, Phone) VALUES 
('C001', 'Nguyen Van A', '123456789', '0912345678'),
('C002', 'Tran Thi B', '987654321', '0908765432'),
('C003', 'Le Van C', '456123789', '0981234567');

-- Thêm dữ liệu vào bảng Bookings
INSERT INTO Bookings (BookingID, CustomerID, BookingDate, CheckInDate, CheckOutDate) VALUES 
('B001', 'C001', GETDATE(), '2024-11-10', '2024-11-12'),
('B002', 'C002', GETDATE(), '2024-11-11', '2024-11-15'),
('B003', 'C003', GETDATE(), '2024-11-12', '2024-11-14');

-- Thêm dữ liệu vào bảng BookingDetails
INSERT INTO BookingDetails (BookingID, RoomID, RoomPrice, CheckInDate, CheckOutDate) VALUES 
('B001', 'R001', 100.000, '2024-11-10', '2024-11-12'),
('B002', 'R002', 150.000, '2024-11-11', '2024-11-15'),
('B003', 'R003', 300.000, '2024-11-12', '2024-11-14');

-- Thêm dữ liệu vào bảng Services
INSERT INTO Services (ServiceID, ServiceName, Price) VALUES 
('S001', 'Massage', 50.000),
('S002', 'Wash and Fold', 50.000),
('S003', 'Airport Pickup', 100.000),
('S004', 'Food Delivery', 100.000),
('S005', 'Breakfast', 50.000);

-- Thêm dữ liệu vào bảng ServiceUsage
INSERT INTO ServiceUsage (ServiceUsageID, ServiceUsageDate, TotalAmount, TotalService, PaymentID, RoomID) VALUES 
('SU001', GETDATE(), 50.000, 2, 'P001', 'R001'),
('SU002', GETDATE(), 50.000, 1, 'P002', 'R002');

-- Thêm dữ liệu vào bảng ServiceDetails
INSERT INTO ServiceDetails (ServiceUsageID, ServiceID, Quantity) VALUES 
('SU001', 'S001', 1),
('SU001', 'S002', 1),
('SU002', 'S003', 1);

-- Thêm dữ liệu vào bảng Payments
INSERT INTO Payments (PaymentID, BookingID, PaymentDate) VALUES 
('P001', 'B001', GETDATE()),
('P002', 'B002', GETDATE()),
('P003', 'B003', GETDATE());
