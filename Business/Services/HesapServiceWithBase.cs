using AppCore.Business.Models.Result;
using Business.Models;

namespace Business.Services
{

    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel model);
    }
    public class HesapService : IHesapService
    {

        private readonly IKullaniciService _kullaniciService;

        public HesapService(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public Result<KullaniciModel> Giris(KullaniciGirisModel model)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k => k.KullaniciAdi == model.KullaniciAdi && k.Sifre == model.Sifre && k.AktifMi == true);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Geçersiz kullanıcı ve şifre!");
            return new SuccessResult<KullaniciModel>(kullanici);
            
        }
    }
}
