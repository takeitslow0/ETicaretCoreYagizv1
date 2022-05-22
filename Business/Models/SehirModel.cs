using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Business.Models
{
    public class SehirModel : RecordBase
    {
        #region Entity
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(150, ErrorMessage = "{0} en fazla {1} karakter olmalıdır!")]
        [DisplayName("Şehir Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Ülke")]
        public int? UlkeId { get; set; }
        #endregion
        #region Extra
        public UlkeModel Ulke { get; set; }
        #endregion
    }
}
