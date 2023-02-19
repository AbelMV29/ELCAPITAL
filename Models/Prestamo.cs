using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class Prestamo : Producto
    {
        [Required]
        public bool EsPrendario { get; set; }
        public decimal DineroPrestamo { get; set; }

        public string Nombre = "Prestamo";
        public DateTime? FechaLimite { get; set; }

    }
}
