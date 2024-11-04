using ecommerce_dotnet_mvc.Models;

namespace ecommerce_dotnet_mvc.ViewModels;

public class HomeProductDetailViewModel
{
    public TDanhMucSp danhMucSp { get; set; }
    public List<TAnhSp> anhSps { get; set; }
}