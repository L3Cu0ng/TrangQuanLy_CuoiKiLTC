create database QuanLyCongTy
go
use QuanLyCongTy
go

CREATE TABLE PhongBans (
    Id INT PRIMARY KEY IDENTITY,
    TenPhongBan NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)
);

CREATE TABLE NhanViens (
    Id INT PRIMARY KEY IDENTITY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(10) NOT NULL,
    Email NVARCHAR(100),
    DiaChi NVARCHAR(255),
    NgaySinh DATE,
    ChucVu NVARCHAR(100),
    PhongBanId INT,
    FOREIGN KEY (PhongBanId) REFERENCES PhongBans(Id)
);

CREATE TABLE TheLoais (
    Id INT PRIMARY KEY IDENTITY,
    TenTheLoai NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255)
);

CREATE TABLE Admins (
    Id INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);

CREATE TABLE BaiViets (
    Id INT PRIMARY KEY IDENTITY,
    TieuDe NVARCHAR(150) NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    NgayDang DATE,
    TacGia NVARCHAR(100),
    TheLoaiId INT,
    AdminId INT,
    FOREIGN KEY (TheLoaiId) REFERENCES TheLoais(Id),
    FOREIGN KEY (AdminId) REFERENCES Admins(Id)
);

CREATE TABLE LichTrinhs (
    Id INT PRIMARY KEY IDENTITY,
    TieuDe NVARCHAR(150) NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    AdminId INT,
    FOREIGN KEY (AdminId) REFERENCES Admins(Id)
);

INSERT INTO Admins (UserName, PasswordHash)
VALUES ('admin', '123456789'),
       ('admin2', '123456789'),
       ('admin3', '123456789');

	   -- Thêm dữ liệu cho bảng PhongBans
INSERT INTO PhongBans (TenPhongBan, MoTa)
VALUES (N'Phòng ban A', N'Mô tả phòng ban A'),
       (N'Phòng ban B', N'Mô tả phòng ban B'),
       (N'Phòng ban C', N'Mô tả phòng ban C');

-- Thêm dữ liệu cho bảng NhanViens
INSERT INTO NhanViens (HoTen, SoDienThoai, Email, DiaChi, NgaySinh, ChucVu, PhongBanId)
VALUES (N'Nguyễn Văn A', '0123456789', 'nguyenvana@example.com', N'Địa chỉ A', '1990-01-01', N'Quản lý', 1),
       (N'Trần Thị B', '0987654321', 'tranthib@example.com', N'Địa chỉ B', '1995-05-10', N'Nhân viên', 2),
       (N'Lê Văn C', '0369841275', 'levanc@example.com', N'Địa chỉ C', '1985-12-20', N'Kế toán', 3);

-- Insert into TheLoais
INSERT INTO TheLoais (TenTheLoai, MoTa) VALUES ('Công Nghệ', 'Các bài viết liên quan đến công nghệ');
INSERT INTO TheLoais (TenTheLoai, MoTa) VALUES ('Kinh Doanh', 'Các bài viết liên quan đến kinh doanh');
INSERT INTO TheLoais (TenTheLoai, MoTa) VALUES ('Giải Trí', 'Các bài viết liên quan đến giải trí');

-- Insert into BaiViets
INSERT INTO BaiViets (TieuDe, NoiDung, NgayDang, TacGia, TheLoaiId, AdminId) 
VALUES ('Bài viết về công nghệ 1', 'Nội dung bài viết về công nghệ 1', '2023-05-01', 'Nguyễn Văn A', 1, 1);

INSERT INTO BaiViets (TieuDe, NoiDung, NgayDang, TacGia, TheLoaiId, AdminId) 
VALUES ('Bài viết về kinh doanh 1', 'Nội dung bài viết về kinh doanh 1', '2023-06-01', 'Trần Thị B', 2, 2);

-- Insert into LichTrinhs
INSERT INTO LichTrinhs (TieuDe, NoiDung, NgayBatDau, NgayKetThuc, AdminId) 
VALUES ('Lịch trình sự kiện công nghệ', 'Nội dung lịch trình sự kiện công nghệ', '2023-07-01', '2023-07-05', 1);

INSERT INTO LichTrinhs (TieuDe, NoiDung, NgayBatDau, NgayKetThuc, AdminId) 
VALUES ('Lịch trình hội thảo kinh doanh', 'Nội dung lịch trình hội thảo kinh doanh', '2023-08-01', '2023-08-03', 2);

ALTER TABLE BaiViets
ADD HinhAnhUrl NVARCHAR(MAX);
