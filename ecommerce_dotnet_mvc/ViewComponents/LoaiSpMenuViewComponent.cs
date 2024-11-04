using ecommerce_dotnet_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_dotnet_mvc.ViewComponents;

public class LoaiSpMenuViewComponent : ViewComponent
{
    private readonly ILoaiSpRepository _loaiSp;

    public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSp)
    {
        _loaiSp = loaiSp;
    }

    public IViewComponentResult Invoke()
    {
        var loaisp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.Loai);
        return View(loaisp);
    }
}