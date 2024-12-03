using AutoMapper;
using Ecomerce_Web.Data;
using Ecomerce_Web.Helpers;
using Ecomerce_Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecomerce_Web.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context db;
        private readonly IMapper _mapper;

        public KhachHangController(Hshop2023Context context, IMapper mapper) 
        { 
            db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        #region Register
        [HttpPost]
        public IActionResult SignUp(RegisterVM register, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<KhachHang>(register);
                    customer.RandomKey = MyUtil.GenerateRandomKey();
                    customer.MatKhau = register.MatKhau.ToMd5Hash(customer.RandomKey);
                    customer.HieuLuc = true; //sẽ xử lý khi dùng mail để active
                    customer.VaiTro = 0;

                    if (image != null)
                    {
                        customer.Hinh = MyUtil.UpLoadHinh(image, "KhachHang");
                    }

                    db.Add(customer);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {

                }

            }
            return View();
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var customer = db.KhachHangs.SingleOrDefault(cus =>
                    cus.MaKh == model.Username);
                if (customer == null)
                {
                    ModelState.AddModelError("error", "Không tồn tại thông tin khách hàng");
                }
                else
                {
                    //if (customer.HieuLuc)
                    //{
                    //    ModelState.AddModelError("error", "Tài khoản khách hàng đã bị khoá, vui lòng liên hệ Admin");
                    //}
                    //else
                    //{
                    //    if (customer.MatKhau == model.Password.ToMd5Hash(customer.RandomKey))
                    //    {
                    //        //Use less Message "Wrong pass", because security below
                    //        ModelState.AddModelError("error", "Sai mật khẩu");
                    //    }
                    //    else
                    //    {
                    //        //
                    //        var claíms = new List<Claim>
                    //        {
                    //            new Claim(ClaimTypes.Email, customer.Email),
                    //            new Claim(ClaimTypes.Name, customer.Name),
                    //            new Claim(ClaimTypes.Email, customer.Email)
                    //        };
                    //    }
                    //}
                }
            }
            return View();
        }
        #endregion
    }
}
