using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistemas.Web.Models.Almacen.Articulo
{
    public class ArticulosCrear
    {
        [Required]
        public int idcategoria { get; set; }
        [Required]
        public string codigo { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public decimal precio_venta { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public bool condicion { get; set; }
    }
}
