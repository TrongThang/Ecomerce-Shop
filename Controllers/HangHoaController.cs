using Ecomerce_Web.Data;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce_Web.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        //Nhúng CSDL
        public HangHoaController(Hshop2023Context context) {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            //Get All Hang Hoa
            var hangHoas = db.HangHoas.AsQueryable();

            if(loai.HasValue)
            {
                hangHoas = hangHoas.Where(p => p.MaLoai == loai.Value); 
            }

            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHangHoa = p.MaHh,
                TenHangHoa = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            })/*.OrderBy(p => p.SoLanXem)*/;

            return View(result);
        }
        [Route("/404")]
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult Search(string? query)
        {
            //Get Hang Hoa
            var hangHoas = db.HangHoas.AsQueryable();

            if (query != null)
            {
                hangHoas = hangHoas.Where(p => p.TenHh.Contains(query));
            }

            var result = hangHoas.Select(p => new HangHoaVM
            {
                MaHangHoa = p.MaHh,
                TenHangHoa = p.TenHh,
                DonGia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Detail(int idProduct)
        {
            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == idProduct);

            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm {idProduct}";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                MaHangHoa = data.MaHh,
                TenHangHoa = data.TenHh,
                DonGia = data.DonGia ?? 0,
                Hinh = data.Hinh ?? string.Empty,
                ChiTiet = data.MoTa ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10, //check sau
                DiemDanhGia = 5, //Check sau
            };
            return View(result);
        }

        //[Route("cart")]
        //public IActionResult Cart()
        //{
        //    var hangHoas = db.HangHoas.AsQueryable();

        //    var result = hangHoas.Select(p => new CartItem
        //    {
        //        TenHangHoa = p.TenHh,
        //        DonGia = p.DonGia ?? 0,
        //        Hinh = p.Hinh ?? ""
        //    });
        //    return View(result);
        //}
    }
}
