using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kullanici : RecordBase
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string KullaniciAdi { get; set; }

        [Required]
        [StringLength(10)]
        public string Sifre { get; set; }

        public bool AktifMi { get; set; }

        public int RolId { get; set; }

        public Rol Rol { get; set; }
    }
}