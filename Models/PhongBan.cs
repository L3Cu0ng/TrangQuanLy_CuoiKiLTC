using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class PhongBan
    {
        public PhongBan()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public int Id { get; set; }
        public string TenPhongBan { get; set; } = null!;
        public string? MoTa { get; set; }

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
