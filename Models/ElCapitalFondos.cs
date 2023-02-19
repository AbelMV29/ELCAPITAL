using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class ElCapitalFondos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBancoUnico { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public decimal FondoMonetario { get; set; } = 5000000;
    }
}
