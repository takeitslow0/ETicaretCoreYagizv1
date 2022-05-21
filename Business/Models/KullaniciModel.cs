using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciModel : RecordBase
    {
        #region Entityler
        [Required(ErrorMessage = "{0} gereklidir!")]
        [MinLength(3, ErrorMessage = "{0} minimum {1} karakter olmalıdır!")]
        [MaxLength(50, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(10, ErrorMessage = "{0} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        [DisplayName("Aktif")]
        public bool AktifMi { get; set; }
        [DisplayName("Rol")]
        public int RolId { get; set; }
        #endregion

        #region Sayfanın İhtiyaçları
        [DisplayName("Rol Adı")]
        public string RolAdiDisplay { get; set; }
        #endregion
    }
}
