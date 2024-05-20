using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            BaiViets = new HashSet<BaiViet>();
        }

        public int Id { get; set; }
        public string TenTheLoai { get; set; } = null!;
        public string? MoTa { get; set; }

        public virtual ICollection<BaiViet> BaiViets { get; set; }
    }
}
