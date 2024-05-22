using System;
using System.Collections.Generic;

namespace CuoiKiLTC.Models
{
    public partial class BaiViet
    {
        public int Id { get; set; }
        public string TieuDe { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
        public DateTime? NgayDang { get; set; }
        public string? TacGia { get; set; }
        public int? TheLoaiId { get; set; }
        public int? AdminId { get; set; }
        public string? HinhAnhUrl { get; set; } // Thêm thuộc tính này để lưu đường dẫn của hình ảnh
        public virtual Admin? Admin { get; set; }
        public virtual TheLoai? TheLoai { get; set; }
    }
}
