using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas.Entidades.Usuarios
{
    public class Persona
    {
        public int idpersona { get; set; }
        public string tipopersona { get; set; }
        public string nombre { get; set; }
        public string tipodocumento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

    }
}
