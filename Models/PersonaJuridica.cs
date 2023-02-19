using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class PersonaJuridica : Cliente
    {
        [Required]
        public string NombreEmpresa { get; set; }

    }
}
