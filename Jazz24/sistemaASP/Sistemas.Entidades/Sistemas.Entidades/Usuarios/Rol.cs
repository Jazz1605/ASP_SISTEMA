using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas.Entidades.Usuarios
{
    public class Rol
    {

        public int idrol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
