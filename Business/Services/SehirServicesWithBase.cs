using AppCore.Business.Models.Result;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface ISehirService : IService<SehirModel, Sehir, ETicaretContext>
    {
        Result<List<SehirModel>> List();
    }

    public class SehirService : ISehirService
    {
        public RepoBase<Sehir, ETicaretContext> Repo { get; set; } = new Repo<Sehir, ETicaretContext>();

        public IQueryable<SehirModel> Query()
        {
            return Repo.Query("Ulke").OrderBy(s => s.Adi).Select(s => new SehirModel()
            {
                Id = s.Id,
                Adi = s.Adi,
                UlkeId = s.UlkeId,
                
                Ulke = new UlkeModel()
                {
                    Id = s.Ulke.Id,
                    Adi = s.Ulke.Adi
                }
            });
        }

        public Result Add(SehirModel model)
        {
            //if (Repo.Query().Any(s => s.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
            if (Query().Any(s => s.Adi.ToUpper() == model.Adi.ToUpper().Trim()))
                return new ErrorResult("Girdiğiniz şehir adına sahip kayıt bulunmaktadır!");
            Sehir entity = new Sehir()
            {
                Adi = model.Adi.Trim(),
                UlkeId = model.UlkeId.Value
            };
            Repo.Add(entity);
            return new SuccessResult();
        }

        public Result Update(SehirModel model)
        {
            //if (Repo.Query().Any(s => s.Adi.ToUpper() == model.Adi.ToUpper().Trim() && s.Id != model.Id))
            if (Query().Any(s => s.Adi.ToUpper() == model.Adi.ToUpper().Trim() && s.Id != model.Id))
                return new ErrorResult("Girdiğiniz şehir adına sahip kayıt bulunmaktadır!");
            Sehir entity = Repo.Query(s => s.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            entity.UlkeId = model.UlkeId.Value;
            Repo.Update(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Sehir entity = Repo.Query(s => s.Id == id, "KullaniciDetaylari").SingleOrDefault();
            if (entity.KullaniciDetaylari != null && entity.KullaniciDetaylari.Count > 0)
                return new ErrorResult("Silinmek istenen şehre ait kullanıcılar bulunmaktadır!");
            Repo.Delete(s => s.Id == id);
            return new SuccessResult("Şehir başarıyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public Result<List<SehirModel>> List()
        {
            var list = Query().ToList();
            if (list.Count == 0)
                return new ErrorResult<List<SehirModel>>("Şehir bulunamadı !");
            return new SuccessResult<List<SehirModel>>(list.Count + " adet şehir bulundu.", list);
        }
    }
}