using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class KullaniciDetayi //RecordBase'den türetmemen gerekiyor çünkü Id özelliği olmaması gerekiyor.
    {
        [Key]
        public int KullaniciId { get; set; }

        public Kullanici Kullanici { get; set; }

        // 01 KALABALIKTA ŞEKİL YAPANIN TENHADA AFFI OLMAZ HE ONA GÖRE -GİZLİ NOT
        [Required]
        [StringLength(200)]
        public string Eposta { get; set; }

        [Required]
        public string Adres { get; set; }

        public Cinsiyet? Cinsiyet { get; set; } 

    }
}
