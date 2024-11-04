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
    private QlBanValiContext db = new();

    [Route("AllUsers")]
    public IActionResult AllUsers(int? page)
    {
        var pageSize = 12;
        var pageNumber = page == null || page < 0 ? 1 : page.Value;
        var users = db.TUsers.AsNoTracking().OrderBy(x => x.Username);
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
        if (ModelState.IsValid)
        {
            db.TUsers.Add(user);
            db.SaveChanges();
            return RedirectToAction("AllUsers");
        }

        return View(user);
    }

    [Route("EditUser")]
    [HttpGet]
    public IActionResult EditUser(string username)
    {
        var user = db.TUsers.Find(username);
        return View(user);
    }

    [Route("EditUser")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditUser(TUser user)
    {
        if (ModelState.IsValid)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AllUsers");
        }

        return View(user);
    }

    [Route("DeleteUser")]
    [HttpGet]
    public IActionResult DeleteUser(string username)
    {
        TempData["Message"] = "";

        var khachhang = db.TKhachHangs.Where(x => x.Username == username).ToList();
        if (khachhang.Count() > 0)
        {
            TempData["Message"] = "Không xóa được người dùng này";
            return RedirectToAction("AllUsers", "UserAdmin");
        }

        var nhanvien = db.TKhachHangs.Where(x => x.Username == username).ToList();
        if (nhanvien.Count() > 0)
        {
            TempData["Message"] = "Không xóa được người dùng này";
            return RedirectToAction("AllUsers", "UserAdmin");
        }

        db.Remove(db.TUsers.Find(username));
        db.SaveChanges();
        TempData["Message"] = "Người dùng đã được xóa";
        return RedirectToAction("AllUsers", "UserAdmin");
    }
}