using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class FormularioRaiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormularioRaiz { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        [Required]
        public int IdSolicitud { get;set; }

        [ForeignKey("IdSolicitud")]
        public virtual Solicitud Solicitud { get; set; }

    }
}
