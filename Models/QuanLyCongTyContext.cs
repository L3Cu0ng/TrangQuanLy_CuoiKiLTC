using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CuoiKiLTC.Models
{
    public partial class QuanLyCongTyContext : DbContext
    {
        public QuanLyCongTyContext()
        {
        }

        public QuanLyCongTyContext(DbContextOptions<QuanLyCongTyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<BaiViet> BaiViets { get; set; } = null!;
        public virtual DbSet<LichTrinh> LichTrinhs { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<PhongBan> PhongBans { get; set; } = null!;
        public virtual DbSet<TheLoai> TheLoais { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3H69VAF;Initial Catalog=QuanLyCongTy;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<BaiViet>(entity =>
            {
                entity.Property(e => e.NgayDang).HasColumnType("date");

                entity.Property(e => e.TacGia).HasMaxLength(100);

                entity.Property(e => e.TieuDe).HasMaxLength(150);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.BaiViets)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__BaiViets__AdminI__2E1BDC42");

                entity.HasOne(d => d.TheLoai)
                    .WithMany(p => p.BaiViets)
                    .HasForeignKey(d => d.TheLoaiId)
                    .HasConstraintName("FK__BaiViets__TheLoa__2D27B809");
            });

            modelBuilder.Entity<LichTrinh>(entity =>
            {
                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.TieuDe).HasMaxLength(150);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.LichTrinhs)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__LichTrinh__Admin__30F848ED");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.Property(e => e.ChucVu).HasMaxLength(100);

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.HoTen).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).HasMaxLength(10);

                entity.HasOne(d => d.PhongBan)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.PhongBanId)
                    .HasConstraintName("FK__NhanViens__Phong__267ABA7A");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.TenPhongBan).HasMaxLength(100);
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.TenTheLoai).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
