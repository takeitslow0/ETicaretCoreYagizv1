using AppCore.Records.Bases;
using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciDetayiModel : RecordBase
    {
        #region Entity
        public Cinsiyet Cinsiyet { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(200, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }

        [DisplayName("Ülke")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? UlkeId { get; set; }

        [DisplayName("Şehir")]
        [Required(ErrorMessage = "{0} gereklidir!")]
        public int? SehirId { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        public string Adres { get; set; }
        #endregion

        #region ekstra
        // bu class'ta sadece kullanıcının detayı üzerinden şehrini ve ülkesini göstermemiz gerektiğinden ülke ve şehir modelleri üzerinden bunu yapabileceğimiz gibi az (iki) özellik kullanacağımızdan sadece ülke adı ve şehir adı için özellikler oluşturmamız da yeterlidir.
        //public UlkeModel Ulke { get; set; }
        public string UlkeAdiDisplay { get; set; }

        //public SehirModel Sehir { get; set; }
        public string SehirAdiDisplay { get; set; }
        #endregion
    }
}