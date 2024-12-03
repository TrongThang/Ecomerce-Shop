using Ecomerce_Web.Data;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce_Web.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;
        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            //Show List Type
            var data = db.Loais.Select(loai => new MenuLoaiVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai,
                SoLuong = loai.HangHoas.Count()
            });

            return View(data); //Default.cshtml
            //return View("TypeProduct", data); //TypeProduct.cshtml
        }
    }
}
