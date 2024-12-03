using Ecomerce_Web.Data;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce_Web.ViewComponents
{
    public class TopProductViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public TopProductViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            //Show List Type
            var data = db.HangHoas.Select(hanghoa => new HangHoaVM
            {
                TenHangHoa = hanghoa.TenHh,
                DonGia = hanghoa.DonGia ?? 0,
                Hinh = hanghoa.Hinh,
                SoLanXem = hanghoa.SoLanXem
            }).Take(3);

            return View(data);
        }
    }
}
