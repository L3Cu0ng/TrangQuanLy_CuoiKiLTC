using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class Admin
    {
        public Admin()
        {
            BaiViets = new HashSet<BaiViet>();
            LichTrinhs = new HashSet<LichTrinh>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual ICollection<BaiViet> BaiViets { get; set; }
        public virtual ICollection<LichTrinh> LichTrinhs { get; set; }
    }
}
