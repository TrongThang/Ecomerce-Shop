using Ecomerce_Web.Data;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Ecomerce_Web.Helpers;
using Microsoft.CodeAnalysis.QuickInfo;
namespace Ecomerce_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context db;

        public CartController(Hshop2023Context context) {
            db = context;
        }
        public List<CartItem> Cart =>  HttpContext.Session.Get<List<CartItem>>(MySettingConst.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHH == id);
            if (item == null)
            {
                var hangHoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if(hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hoá có mã {id}";
                    return Redirect("/404");
                }

                item = new CartItem
                {
                    MaHH = hangHoa.MaHh,
                    TenHangHoa = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh ?? string.Empty,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }

            HttpContext.Session.Set(MySettingConst.CART_KEY, gioHang);
            //Lưu ý nếu để mỗi Action mà không có Controller thì sẽ chuyển hướng đến trang Index của Controller hiện tại.
            //Tức là nếu Controller hiện tại là Hang Hoa thì nó sẽ chuyển về /HangHoa/Cart(View)
            //Vì vậy để giải quyết ta nên thêm tham số Controller sau tham số Action để giải quyểt vấn đề này
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            //Get Product by ID
            var item = gioHang.SingleOrDefault(p => p.MaHH == id);
            if(item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySettingConst.CART_KEY, gioHang);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
