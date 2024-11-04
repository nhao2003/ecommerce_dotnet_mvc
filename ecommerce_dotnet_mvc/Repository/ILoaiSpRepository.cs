using ecommerce_dotnet_mvc.Models;

namespace ecommerce_dotnet_mvc.Repository;

public interface ILoaiSpRepository
{
    TLoaiSp Add(TLoaiSp loaiSp);
    TLoaiSp Update(TLoaiSp loaiSp);
    TLoaiSp Delete(string maloaiSp);
    TLoaiSp GetLoaiSp(string maloaiSp);
    IEnumerable<TLoaiSp> GetAllLoaiSp();
}