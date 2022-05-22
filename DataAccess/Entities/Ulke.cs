using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Ulke : RecordBase
    {
        [Required]
        [StringLength(105)]
        public string Adi { get; set; }

        public List<Sehir> Sehirler { get; set; }
        public List<KullaniciDetayi> KullaniciDetaylari { get; set; }
    }
}
