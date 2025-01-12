
INSERT INTO EmployeeTypes (TypeName) VALUES
( 'Toàn thời gian'),
('Bán thời gian'),
('Thực tập sinh'),
('Tạm thời');
go

INSERT INTO Departments ( DepartmentName, Description, ActiveFrom, ActiveTo) VALUES
( 'Nhân sự', 'Quản lý tuyển dụng và quan hệ nhân viên', '2020-01-01', NULL),
( 'Tài chính', 'Quản lý tài chính của công ty', '2020-01-01', NULL),
( 'Kỹ thuật', 'Phát triển và bảo trì sản phẩm', '2020-01-01', NULL),
( 'Marketing', 'Quảng bá sản phẩm và quản lý quảng cáo', '2020-01-01', NULL),
( 'Bán hàng', 'Quản lý bán hàng và quan hệ khách hàng', '2020-01-01', NULL);
go

INSERT INTO Employees ( FirstName, LastName, DepartmentID, Position, Email, PhoneNumber, TypeID, DayOfBirth, Gender, JoiningDate, Nationality, Address) VALUES
( 'Nguyễn', 'Văn A', 1, 'Quản lý Nhân sự', 'nguyenvana@example.com', '0123456789', 1, '1990-01-01', 'Nam', '2021-01-01', 'Việt Nam', '123 Đường A, Quận B, TP. C'),
( 'Trần', 'Thị B', 2, 'Chuyên viên Tài chính', 'tranthib@example.com', '0987654321', 2, '1985-05-15', 'Nữ', '2021-05-15', 'Việt Nam', '456 Đường B, Quận C, TP. D'),
( 'Lê', 'Văn C', 3, 'Kỹ sư Phần mềm', 'levanc@example.com', '0123344556', 1, '1992-09-30', 'Nam', '2022-09-30', 'Việt Nam', '789 Đường C, Quận D, TP. E'),
( 'Phạm', 'Thị D', 4, 'Chuyên viên Marketing', 'phamthid@example.com', '0677889900', 3, '1995-12-25', 'Nữ', '2023-12-25', 'Việt Nam', '101 Đường D, Quận E, TP. F'),
( 'Hoàng', 'Văn E', 5, 'Nhân viên Bán hàng', 'hoangvane@example.com', '0566778899', 1, '1988-02-20', 'Nam', '2022-02-20', 'Việt Nam', '202 Đường E, Quận F, TP. G');
go