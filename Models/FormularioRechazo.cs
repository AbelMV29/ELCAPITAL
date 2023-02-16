using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class FormularioRechazo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFormularioRechazo { get; set; }
        [Required]
        public string Motivo { get; set; }

        public DateTime FechaRechazo { get; set; }
        [Required]
        public int IdSolicitud { get; set; }
        [ForeignKey("IdSolicitud")]
        public virtual Solicitud Solicitud { get; set; }

    }
}
