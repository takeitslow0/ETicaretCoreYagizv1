using AppCore.Business.Models.Result;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services;

public interface IKullaniciService : IService<KullaniciModel, Kullanici, ETicaretContext>
{

}

public class KullaniciService : IKullaniciService
{
    public RepoBase<Kullanici, ETicaretContext> Repo { get; set; } = new Repo<Kullanici, ETicaretContext>();

    public Result Add(KullaniciModel model)
    {
        throw new NotImplementedException();
    }

    public Result Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Repo.Dispose();
    }

    public IQueryable<KullaniciModel> Query()
    {
        return Repo.Query().Select(e => new KullaniciModel()
        {
            AktifMi = e.AktifMi,
            Id = e.Id,
            KullaniciAdi = e.KullaniciAdi,
            RolId = e.RolId,
            Sifre = e.Sifre,
        });
    }

    public Result Update(KullaniciModel model)
    {
        throw new NotImplementedException();
    }
}
