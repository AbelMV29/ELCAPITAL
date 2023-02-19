using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class TarjetaDeCredito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarjetaDeCredito { get; set; }
        [Required]
        public int CodigoTarjeta { get; set; }
        [Required]
        public int ClaveTarjeta { get; set; }
        [Required]
        public int CVU { get; set; }
        [Required]
        public decimal DineroEnTarjeta { get; set; }
        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Paquete Paquete { get; set; }

    }
}
