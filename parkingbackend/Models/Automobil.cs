using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Parkingbackend.Models
{
    [Table("Automobil")]
    public class Automobil{

        [Key]
        [Column("ID")]
        public int ID {get; set;}

        [Column("Tip")]
        [MaxLength(255)]
        public string Tip {get; set;}

        [Column("Automobilibroj")]
        public int Automobilibroj {get; set;}

        [Column("Idpolja")]
        public int Idpolja {get; set;}

        [JsonIgnore]
        public Polje Polje {get; set;}


    }
}