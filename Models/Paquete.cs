using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ELCAPITAL.Models
{
    public class Paquete : Producto
    {
        [Required]
        public bool EsCrediticio { get; set; }
        public string Nombre = "Paquete";
        public List<TarjetaDeCredito>? TarjetaDeCreditos { get; set; }
    }
}
