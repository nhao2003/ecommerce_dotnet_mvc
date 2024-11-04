using ecommerce_dotnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_dotnet_mvc.Controllers;

public class AccessController : Controller
{
    private QlBanValiContext db = new();

    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return View();
        else
            return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Login(TUser user)
    {
        if (HttpContext.Session.GetString("UserName") == null)

        {
            var u = db.TUsers
                .FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password));
            if (u == null) return View();
            HttpContext.Session.SetString("UserName", u.Username.ToString());
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return View();
        else
            return RedirectToAction("Login", "Access");
    }

    [HttpPost]
    public IActionResult SignUp(TUser user)
    {
        if (HttpContext.Session.GetString("UserName") != null) return View();
        // check user is exit
        var u = db.TUsers.FirstOrDefault(x => x.Username.Equals(user.Username));
        if (u != null)
        {
            ViewBag.Message = "User is exit";
            return RedirectToAction("Login", "Access");
        }

        user.LoaiUser = 0;

        db.TUsers.Add(user);
        db.SaveChanges();
        ViewBag.Message = "Sign up success";

        return RedirectToAction("Login", "Access");

    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserName");
        return RedirectToAction("Login", "Access");
    }
}