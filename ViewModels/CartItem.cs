namespace Ecomerce_Web.ViewModels
{
    public class CartItem
    {
        public int MaHH { get; set; }
        public string TenHangHoa { get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
