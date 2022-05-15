using AppCore.Business.Models.Result;
using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework;
using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IUrunService : IService<UrunModel, Urun, ETicaretContext>
    {

    }

    public class UrunService : IUrunService
    {
        public RepoBase<Urun, ETicaretContext> Repo { get; set; } = new Repo<Urun, ETicaretContext>();
        public Result Add(UrunModel model)
        {
            if(Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Belirtilen ürün adına sahip kayıt bulunmaktadır!");
            if(model.SonKullanmaTarihi.HasValue && model.SonKullanmaTarihi.Value < DateTime.Today)
                return new ErrorResult("Son kullanma tarihi bugün veya daha sonrası olmalıdır!");
            Urun entity = new Urun()
            {
                Aciklamasi = model.Aciklamasi?.Trim(),
                Adi = model.Adi?.Trim(),
                BirimFiyati = model.BirimFiyati.Value,
                KategoriId = model.KategoriId.Value,
                SonKullanmaTarihi = model.SonKullanmaTarihi,
                StokMiktari = model.StokMiktari.Value
            };
            Repo.Add(entity);
            return new SuccessResult("Ürün başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            Repo.Delete(u => u.Id == id);
            return new SuccessResult("Ürün başarıyla silindi!");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<UrunModel> Query()
        {
            // AutoMapper
            return Repo.Query("Kategori").OrderBy(u => u.Adi).Select(u => new UrunModel()
            {
                Id = u.Id,
                Aciklamasi = u.Aciklamasi,
                Adi = u.Adi,
                BirimFiyati = u.BirimFiyati,
                KategoriId = u.KategoriId,
                SonKullanmaTarihi = u.SonKullanmaTarihi,
                StokMiktari = u.StokMiktari,

                BirimFiyatiDisplay = u.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")), // en-US

                SonKullanmaTarihiDisplay = u.SonKullanmaTarihi.HasValue ? u.SonKullanmaTarihi.Value.ToString("yyyy-MM-dd") : "",
            
                KategoriAdiDisplay = u.Kategori.Adi
            });
        }
        
        public Result Update(UrunModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim() && u.Id != model.Id))
                return new ErrorResult("Belirtilen ürün adına sahip kayıt bulunmaktadır!");
            if (model.SonKullanmaTarihi.HasValue && model.SonKullanmaTarihi.Value < DateTime.Today)
                return new ErrorResult("Son kullanma tarihi bugün veya daha sonrası olmalıdır!");

            //Urun entity = Repo.Query().SingleOrDefault(u => u.Id == model.Id);
            Urun entity = Repo.Query(u => u.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            entity.BirimFiyati = model.BirimFiyati.Value;
            entity.KategoriId = model.KategoriId.Value;
            entity.SonKullanmaTarihi = model.SonKullanmaTarihi;
            entity.StokMiktari = model.StokMiktari.Value;
            Repo.Update(entity); //https://10015.io/tools/css-loader-generator
            return new SuccessResult("Ürün başarıyla güncellendi.");
        }
    }
}