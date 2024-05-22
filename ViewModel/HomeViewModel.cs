using CuoiKiLTC.Models;

namespace CuoiKiLTC.ViewModels
{
    public class HomeViewModel
    {
        public List<BaiViet> BaiViets { get; set; }
        public List<LichTrinh> LichTrinhs { get; set; }
        public int BaiVietPageCount { get; set; }
        public int LichTrinhPageCount { get; set; }
        public int CurrentPage { get; set; }
    }

}
