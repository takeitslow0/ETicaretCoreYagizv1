using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kategori : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }

        public string Aciklamasi { get; set; }

        public List<Urun> Urunler { get; set; }
    }
}
