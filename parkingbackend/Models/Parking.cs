using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parkingbackend.Models
{
    [Table("Parking")]
    public class Parking
    {
        [Key]
        [Column("ID")]
        public int ID {get; set;}

        [Column("N")]
        public int N {get; set;}

        [Column("M")]
        public int M {get; set;}

        [Column("Ime")]
        [MaxLength(255)]
        public string Ime {get; set;}

        [Column("Kapacitet")]
        public int Kapacitet {get; set;}

        public virtual List<Polje> ParkingPolja {get; set;}
    }
}