--CREATE DATABASE QL_CT_PM
USE QL_CT_PM
GO

-- 1. Loại nhân viên
CREATE TABLE EmployeeTypes (
    TypeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    TypeName NVARCHAR(100) NULL
);

-- 2. Phòng ban
CREATE TABLE Departments (
    DepartmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    DepartmentName NVARCHAR(100) NULL,
    Description NVARCHAR(MAX) NULL,
    ActiveFrom DATE NULL,
    ActiveTo DATE NULL
);

-- 3. Nhân viên
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    FirstName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NULL,
    DepartmentID INT NULL,
    Position NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(15) NULL,
    TypeID INT NULL,
    DayOfBirth DATE NOT NULL,
    Gender NVARCHAR(10) NULL,
    JoiningDate DATE NOT NULL,
    Nationality NVARCHAR(50) NULL,
    Address NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    CONSTRAINT FK_Employees_EmployeeTypes FOREIGN KEY (TypeID) REFERENCES EmployeeTypes(TypeID)
);

-- 4. Hợp đồng lao động
CREATE TABLE EmploymentContracts (
    ContractID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NULL,
    ContractType NVARCHAR(50) NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    CONSTRAINT FK_EmploymentContracts_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 5. Lương
CREATE TABLE Salaries (
    EmployeeID INT NOT NULL PRIMARY KEY, 
    Salary DECIMAL(10, 2) NULL,
    Bonus DECIMAL(10, 2) NULL,
    PaymentDate DATE NOT NULL,
    CONSTRAINT FK_Salaries_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 6. KPIs
CREATE TABLE PerformanceKPIs (
    KPIID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NULL,
    KPIValue DECIMAL(10, 2) NULL,
    KPIMonth DATE NULL,
    CONSTRAINT FK_PerformanceKPIs_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 9. Khách hàng
CREATE TABLE Clients (
    ClientID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    CompanyName NVARCHAR(255) NULL,
    ContactName NVARCHAR(100) NULL,
    ContactEmail NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(15) NULL
);

-- 7. Dự án
CREATE TABLE Projects (
    ProjectID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    ProjectName NVARCHAR(255) NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Status NVARCHAR(50) NULL,
    ManagerID INT NULL,
    ClientID INT NULL,
    CONSTRAINT FK_Projects_Manager FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_Projects_Clients FOREIGN KEY (ClientID) REFERENCES Clients(ClientID)
);

-- 13. Công việc
CREATE TABLE Tasks (
    TaskID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    TaskName NVARCHAR(255) NOT NULL,
    ProjectID INT NOT NULL,
    AssignedTo INT NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Status NVARCHAR(50) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID),
    CONSTRAINT FK_Tasks_Employees FOREIGN KEY (AssignedTo) REFERENCES Employees(EmployeeID)
);

-- 8. Theo dõi thời gian
CREATE TABLE TimeTracking (
    EntryID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NULL,
    TaskID INT NULL,
    Date DATE NULL,
    HoursWorked DECIMAL(5, 2) NULL,
    CONSTRAINT FK_TimeTracking_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_TimeTracking_Tasks FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID)
);

-- 10. Chi tiết dự án
CREATE TABLE ProjectDetails (
    DetailsID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    ClientID INT NULL,
    ProjectID INT NOT NULL,
    DetailedDescription NVARCHAR(MAX) NULL,
    EstimatedBudget DECIMAL(10, 2) NULL,
    ActualBudget DECIMAL(10, 2) NULL,
    SRS NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_ProjectDetails_Clients FOREIGN KEY (ClientID) REFERENCES Clients(ClientID),
    CONSTRAINT FK_ProjectDetails_Projects FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID)
);

-- 11. Công nghệ
CREATE TABLE Technologies (
    TechID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    TechName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL
);

CREATE TABLE EmployeeTechnologies (
    EmployeeID INT NOT NULL,
    TechID INT NOT NULL,
    ProficiencyLevel INT NOT NULL,
    CONSTRAINT FK_EmployeeTechnologies_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_EmployeeTechnologies_Technologies FOREIGN KEY (TechID) REFERENCES Technologies(TechID),
    PRIMARY KEY (EmployeeID, TechID)
);

-- 12. Đào tạo
CREATE TABLE TrainingPrograms (
    ProgramID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    ProgramName NVARCHAR(255) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NULL,
    Trainer NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX) NULL
);

CREATE TABLE EmployeeTrainings (
    EmployeeID INT NOT NULL,
    ProgramID INT NOT NULL,
    CompletionStatus NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_EmployeeTrainings_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    CONSTRAINT FK_EmployeeTrainings_TrainingPrograms FOREIGN KEY (ProgramID) REFERENCES TrainingPrograms(ProgramID),
    PRIMARY KEY (EmployeeID, ProgramID)
);

-- 14. Bảo hiểm
CREATE TABLE Insurances (
    InsuranceCode INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NOT NULL,
    Type NVARCHAR(255) NOT NULL,
    Status INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CoverageAmount DECIMAL(8, 2) NOT NULL,
    Created_at DATETIME NOT NULL DEFAULT GETDATE(),
    Updated_at DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Insurances_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 15. Nghỉ phép
CREATE TABLE Leaves (
    LeaveID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Reason NVARCHAR(255) NOT NULL,
    Status INT NOT NULL,
    Created_at DATETIME NOT NULL DEFAULT GETDATE(),
    Updated_at DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Leaves_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 16. Người dùng
CREATE TABLE Users (
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- Đã thêm IDENTITY
    EmployeeID INT NOT NULL,
    Email NVARCHAR(191) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role INT NOT NULL,
    Created_at DATETIME NOT NULL DEFAULT GETDATE(),
    Updated_at DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Users_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
