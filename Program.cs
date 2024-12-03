using Ecomerce_Web.Data;
using Ecomerce_Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Khai báo một nơi nhưng có phạm vi toàn ứng dụng.
//Thông báo làm việc với databasse nào
builder.Services.AddDbContext<Hshop2023Context>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("HShop"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/KhachHang/SignUp";
	//After login, if don't have permission will redirect page "AccessDenied"
	options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(36000);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=KhachHang}/{action=SignUp}/{id?}");

app.Run();
