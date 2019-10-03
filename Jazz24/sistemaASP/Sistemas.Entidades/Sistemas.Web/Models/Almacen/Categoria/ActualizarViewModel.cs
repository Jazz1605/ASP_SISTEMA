using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistemas.Web.Models.Almacen.Categoria
{
    public class ActualizarViewModel
    {   [Required]
        public int idcategoria { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "No escriba más 50 caracteres ni menos 3")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
       
    }
}
