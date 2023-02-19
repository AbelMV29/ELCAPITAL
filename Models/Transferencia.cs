using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransferencia { get; set; }
        [Required]
        public decimal MontoTransferido { get; set; }
        [Required]
        public DateTime FechaTrans { get; set; }
        [Required]
        public int CVUDestino { get; set; }
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }
    }
}
