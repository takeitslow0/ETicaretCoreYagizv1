using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Rol : RecordBase
    {
        [Required]
        [StringLength(10)]
        public string Adi { get; set; }

        public List<Kullanici> Kullanicilar { get; set; }
    }
}
