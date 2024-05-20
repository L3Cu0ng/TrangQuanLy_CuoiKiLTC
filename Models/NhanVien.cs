using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class NhanVien
    {
        public int Id { get; set; }
        public string HoTen { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? ChucVu { get; set; }
        public int? PhongBanId { get; set; }

        public virtual PhongBan? PhongBan { get; set; }
    }
}
