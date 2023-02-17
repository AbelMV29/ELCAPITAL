using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ELCAPITAL.Models
{
    public class Solicitud
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSolicitud { get; set; }
        [Required]
        public string TipoSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }
        public virtual FormularioRaiz? FormularioRaiz { get; set; }
        public virtual FormularioRechazo? FormularioRechazo { get; set; }
    }
}
