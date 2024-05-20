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
