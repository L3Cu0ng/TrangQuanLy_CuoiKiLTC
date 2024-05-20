using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class LichTrinh
    {
        public int Id { get; set; }
        public string TieuDe { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int? AdminId { get; set; }

        public virtual Admin? Admin { get; set; }
    }
}
