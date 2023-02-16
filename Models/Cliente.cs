using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        [Required]
        public string? TipoDocumento { get; set; }
        [Required]
        public int NumeroDocumento { get; set; }

        public List<Producto>? Productos { get; set; }

        public List<Solicitud>? Solicitudes { get; set; }
        public List<Restriccion>? Restricciones { get; set; }

    }
}
