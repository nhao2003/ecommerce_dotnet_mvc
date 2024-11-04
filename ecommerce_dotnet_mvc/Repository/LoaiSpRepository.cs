using ecommerce_dotnet_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_dotnet_mvc.Repository;

public class LoaiSpRepository : ILoaiSpRepository
{
    private readonly QlBanValiContext _context;

    public LoaiSpRepository(QlBanValiContext context)
    {
        _context = context;
    }

    public TLoaiSp Add(TLoaiSp loaiSp)
    {
        _context.TLoaiSps.Add(loaiSp);
        _context.SaveChanges();
        return loaiSp;
    }

    public TLoaiSp Delete(string maloaiSp)
    {
        var sp = _context.TLoaiSps.Find(maloaiSp);
        _context.TLoaiSps.Remove(sp);
        return sp;
    }

    public IEnumerable<TLoaiSp> GetAllLoaiSp()
    {
        return _context.TLoaiSps;
    }

    public TLoaiSp GetLoaiSp(string maloaiSp)
    {
        return _context.TLoaiSps.Find(maloaiSp);
    }

    public TLoaiSp Update(TLoaiSp loaiSp)
    {
        _context.TLoaiSps.Update(loaiSp);
        _context.SaveChanges();
        return loaiSp;
    }
}