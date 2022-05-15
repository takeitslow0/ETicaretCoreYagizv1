using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        // entity
        [Required]
        [StringLength(100)]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [DisplayName("Açıklaması")]
        public string Aciklamasi { get; set; }

        // sayfadaki ihtiyaç
        [DisplayName("Ürün Sayısı")]
        public int UrunSayisiDisplay { get; set; }
    }
}
