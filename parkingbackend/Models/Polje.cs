using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Parkingbackend.Models
{
    [Table("Polje")]
    public class Polje
    {
        [Key]
        [Column("ID")]
        public int ID {get; set;}

        [Column("Idparkinga")]
        public int Idparkinga {get; set;}

        [Column("X")]
        public int X {get; set;}

        [Column("Y")]
        public int Y {get; set;}

        [Column("Boja")]
        [MaxLength(255)]
        public string Boja {get; set;}

        [Column("Nazivpolja")]
        [MaxLength(255)]
        public string Nazivpolja {get; set;}

        [Column("Brojautomobila")]
        public int Brojautomobila {get; set;}

        [Column("Maxkapacitet")]
        public int Maxkapacitet {get; set;}

        public virtual List<Automobil> Automobili { get; set;}

        [JsonIgnore]
        public Parking Parking {get; set;}
    }
}