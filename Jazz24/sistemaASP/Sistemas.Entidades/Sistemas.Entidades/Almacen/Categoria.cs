using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistemas.Entidades.Almacen
{
   public class Categoria
    {
        public object idarticulo;

        public int idcategoria { get; set; }
        [Required]
        [StringLength(50,MinimumLength=3,ErrorMessage="No escriba más 50 caracteres")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool  condicion { get; set; }

        public ICollection<Articulo> articulos { get; set; }
    }
}
