using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ecommerce_dotnet_mvc.Models;
using ecommerce_dotnet_mvc.Models.Authentication;
using ecommerce_dotnet_mvc.ViewModels;
using X.PagedList;

namespace ecommerce_dotnet_mvc.Controllers;

public class HomeController : Controller
{
    private QlBanValiContext db = new();
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authentication]
    public IActionResult Index(int? page)
    {
        var pageSize = 8;
        var pageNumber = page == null || page < 0 ? 1 : page.Value;
        var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
        PagedList<TDanhMucSp> lst = new(lstsanpham, pageNumber, pageSize);
        return View(lst);
    }

    [Authentication]
    public IActionResult SanPhamTheoLoai(string maloai, int? page)
    {
        var pageSize = 8;
        var pageNumber = page == null || page < 0 ? 1 : page.Value;
        var lstsanpham = db.TDanhMucSps.AsNoTracking().Where
            (x => x.MaLoai == maloai).OrderBy(x => x.TenSp);
        PagedList<TDanhMucSp> lst = new(lstsanpham,
            pageNumber, pageSize);
        ViewBag.maloai = maloai;
        return View(lst);
    }

    [Authentication]
    public IActionResult ChiTietSanPham(string maSp)
    {
        var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
        var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
        ViewBag.anhSanPham = anhSanPham;
        return View(sanPham);
    }

    [Authentication]
    public IActionResult ProductDetail(string maSp)
    {
        var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
        var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
        var homeProductDetailViewModel = new HomeProductDetailViewModel
        {
            danhMucSp = sanPham,
            anhSps = anhSanPham
        };
        return View(homeProductDetailViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}