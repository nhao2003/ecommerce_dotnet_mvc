using ecommerce_dotnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ecommerce_dotnet_mvc.Areas.Admin.Controllers;

[Area("admin")]
[Route("admin")]
[Route("admin/homeadmin")]
public class HomeAdminController : Controller
{
    private readonly QlBanValiContext _db = new();

    [Route("")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("danhmucsanpham")]
    public IActionResult DanhMucSanPham(int? page)
    {
        var pageSize = 12;
        var pageNumber = page == null || page < 0 ? 1 : page.Value;
        var lstsanpham = _db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
        PagedList<TDanhMucSp> lst = new(lstsanpham, pageNumber, pageSize);
        return View(lst);
    }

    [Route("ThemSanPhamMoi")]
    [HttpGet]
    public IActionResult ThemSanPhamMoi()
    {
        ViewBag.MaChatLieu = new SelectList(_db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
        ViewBag.MaHangSx = new SelectList(_db.THangSxes.ToList(), "MaHangSx", "HangSx");
        ViewBag.MaNuocSx = new SelectList(_db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
        ViewBag.MaLoai = new SelectList(_db.TLoaiSps.ToList(), "MaLoai", "Loai");
        ViewBag.MaDt = new SelectList(_db.TLoaiDts.ToList(), "MaDt", "TenLoai");
        return View();
    }

    [Route("ThemSanPhamMoi")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
    {
        if (ModelState.IsValid)
        {
            _db.TDanhMucSps.Add(sanPham);
            _db.SaveChanges();
            return RedirectToAction("DanhMucSanPham");
        }

        return View(sanPham);
    }

    [Route("SuaSanPham")]
    [HttpGet]
    public IActionResult SuaSanPham(string maSanPham)
    {
        ViewBag.MaChatLieu = new SelectList(_db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
        ViewBag.MaHangSx = new SelectList(_db.THangSxes.ToList(), "MaHangSx", "HangSx");
        ViewBag.MaNuocSx = new SelectList(_db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
        ViewBag.MaLoai = new SelectList(_db.TLoaiSps.ToList(), "MaLoai", "Loai");
        ViewBag.MaDt = new SelectList(_db.TLoaiDts.ToList(), "MaDt", "TenLoai");

        var sanPham = _db.TDanhMucSps.Find(maSanPham);
        return View(sanPham);
    }

    [Route("SuaSanPham")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SuaSanPham(TDanhMucSp sanPham)
    {
        if (ModelState.IsValid)
        {
            _db.Entry(sanPham).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("DanhMucSanPham");
        }

        return View(sanPham);
    }

    [Route("XoaSanPham")]
    [HttpGet]
    public IActionResult XoaSanPham(string maSanPham)
    {
        TempData["Message"] = "";
        var chiTietSanPhams = _db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
        if (chiTietSanPhams.Count() > 0)
        {
            TempData["Message"] = "Không xóa được sản phẩm này";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        var anhSanPhams = _db.TAnhSps.Where(x => x.MaSp == maSanPham);
        if (anhSanPhams.Any()) _db.RemoveRange(anhSanPhams);
        _db.Remove(_db.TDanhMucSps.Find(maSanPham));
        _db.SaveChanges();
        TempData["Message"] = "Sản phẩm đã được xóa";
        return RedirectToAction("DanhMucSanPham", "HomeAdmin");
    }
}