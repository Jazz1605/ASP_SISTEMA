using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistemas.Web.Models.Usuarios.Persona
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "No escriba más 50 caracteres ni menos de 3")]
        public string tipopersona { get; set; }
        public string nombre { get; set; }
        public string tipodocumento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

    }
}
