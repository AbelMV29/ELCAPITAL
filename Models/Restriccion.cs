using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class Restriccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRestriccion { get; set; }

        public string? Motivo { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        [Required]
        public int IdCliente { get; set; }


        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

    }
}
