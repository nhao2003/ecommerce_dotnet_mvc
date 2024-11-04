using ecommerce_dotnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ecommerce_dotnet_mvc.Areas.Admin.Controllers;

[Area("admin")]
[Route("admin")]
[Route("admin/useradmin")]
public class UserAdminController : Controller
{
    private readonly QlBanValiContext _db = new();

    [Route("AllUsers")]
    public IActionResult AllUsers(int? page)
    {
        const int pageSize = 12;
        var pageNumber = page is null or < 0 ? 1 : page.Value;
        var users = _db.TUsers.AsNoTracking().OrderBy(x => x.Username);
        PagedList<TUser> lst = new(users, pageNumber, pageSize);
        return View(lst);
    }

    [Route("AddUser")]
    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [Route("AddUser")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddUser(TUser user)
    {
        if (!ModelState.IsValid) return View(user);
        _db.TUsers.Add(user);
        _db.SaveChanges();
        return RedirectToAction("AllUsers");

    }

    [Route("EditUser")]
    [HttpGet]
    public IActionResult EditUser(string username)
    {
        var user = _db.TUsers.Find(username);
        return View(user);
    }

    [Route("EditUser")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditUser(TUser user)
    {
        if (ModelState.IsValid)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("AllUsers");
        }

        return View(user);
    }

    [Route("DeleteUser")]
    [HttpGet]
    public IActionResult DeleteUser(string username)
    {
        TempData["Message"] = "";

        var khachhang = _db.TKhachHangs.Where(x => x.Username == username).ToList();
        if (khachhang.Count != 0)
        {
            TempData["Message"] = "Không xóa được người dùng này";
            return RedirectToAction("AllUsers", "UserAdmin");
        }

        var nhanvien = _db.TKhachHangs.Where(x => x.Username == username).ToList();
        if (nhanvien.Any())
        {
            TempData["Message"] = "Không xóa được người dùng này";
            return RedirectToAction("AllUsers", "UserAdmin");
        }

        _db.Remove(_db.TUsers.Find(username));
        _db.SaveChanges();
        TempData["Message"] = "Người dùng đã được xóa";
        return RedirectToAction("AllUsers", "UserAdmin");
    }
}