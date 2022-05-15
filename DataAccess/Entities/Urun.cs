using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    //[Table("ETicaretUrunler")]  Tablo ismi değiştirir
    public class Urun : RecordBase
    {
        [Required] // Boş girilemez
        [StringLength(200)] // Adı en fazla 200 karakter alabilir
        public string Adi { get; set; }

        [StringLength(500)]
        public string Aciklamasi { get; set; }

        public double BirimFiyati { get; set; }

        public int StokMiktari { get; set; }

        public DateTime? SonKullanmaTarihi { get; set; }

        public int KategoriId { get; set; }

        public Kategori Kategori { get; set; }
    }
}
