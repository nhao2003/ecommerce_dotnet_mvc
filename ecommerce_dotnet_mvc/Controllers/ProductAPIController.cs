using ecommerce_dotnet_mvc.Models;
using ecommerce_dotnet_mvc.Models.ProductModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_dotnet_mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductApiController : ControllerBase
{
    private readonly QlBanValiContext _db = new();

    [HttpGet]
    public IEnumerable<Product> GetAllProducts()
    {
        //return db.TDanhMucSps.ToList();
        var products = (from p in _db.TDanhMucSps
            select new Product
            {
                MaSp = p.MaSp,
                TenSp = p.TenSp,
                MaLoai = p.MaLoai,
                AnhDaiDien = p.AnhDaiDien,
                GiaNhoNhat = p.GiaNhoNhat
            }).ToList();

        return products;
    }

    [HttpGet("{maloai}")]
    public IEnumerable<Product> GetAllProductsByCategory(string maloai)
    {
        //return db.TDanhMucSps.ToList();
        var products = (from p in _db.TDanhMucSps
            where p.MaLoai == maloai
            select new Product
            {
                MaSp = p.MaSp,
                TenSp = p.TenSp,
                MaLoai = p.MaLoai,
                AnhDaiDien = p.AnhDaiDien,
                GiaNhoNhat = p.GiaNhoNhat
            }).ToList();

        return products;
    }
}