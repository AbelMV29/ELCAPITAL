using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELCAPITAL.Models
{
    public class PersonaFisica : Cliente
    {
        public string Nombre { get; set; }

    }
}
