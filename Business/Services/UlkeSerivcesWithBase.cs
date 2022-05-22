using AppCore.Business.Models.Result;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IUlkeService : IService<UlkeModel, Ulke, ETicaretContext>
    {
        Result<List<UlkeModel>> UlkeleriGetir();
    }

    public class UlkeService : IUlkeService
    {
        public RepoBase<Ulke, ETicaretContext> Repo { get; set; } = new Repo<Ulke, ETicaretContext>();

        public IQueryable<UlkeModel> Query()
        {
            return Repo.Query().OrderBy(u => u.Adi).Select(u => new UlkeModel()
            {
                Id = u.Id,
                Adi = u.Adi
            });
        }

        public Result Add(UlkeModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz ülke adına sahip kayıt bulunmaktadır!");
            Ulke entity = new Ulke()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Update(UlkeModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToUpper() == model.Adi.ToUpper().Trim() && u.Id != model.Id))
                return new ErrorResult("Girdiğiniz ülke adına sahip kayıt bulunmaktadır!");
            Ulke entity = Repo.Query(u => u.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            Repo.Update(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Ulke entity = Repo.Query(u => u.Id == id, "Sehirler", "KullaniciDetaylari").SingleOrDefault();
            if (entity.Sehirler != null && entity.Sehirler.Count > 0)
                return new ErrorResult("Silinmek istenen ülkeye ait şehirler bulunmaktadır!");
            if (entity.KullaniciDetaylari != null && entity.KullaniciDetaylari.Count > 0)
                return new ErrorResult("Silinmek istenen ülkeye ait kullanıcılar bulunmaktadır!");
            Repo.Delete(u => u.Id == id);
            return new SuccessResult("Ülke başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        // servisler ihtiyaca göre yeni tanımlanan methodlar üzerinden özelleştirilebilir.
        public Result<List<UlkeModel>> UlkeleriGetir()
        {
            var ulkeler = Query().ToList();
            if (ulkeler.Count == 0)
                return new ErrorResult<List<UlkeModel>>("Ülke bulunamadı!");
            return new SuccessResult<List<UlkeModel>>(ulkeler);
        }
    }
}