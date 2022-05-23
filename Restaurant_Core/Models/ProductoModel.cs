using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class ProductoModel
    {
		[Display(Name = "Código de Producto")]
		public int id_producto { get; set; }
		[Display(Name = "Nombre de Producto")]
		public string nom_producto { get; set; }
		[Display(Name = "Precio")]
		public decimal precio { get; set; }
		[Display(Name = "Ruta de img")]
		public string ruta_imagen { get; set; }
		[Display(Name = "Nombre de img")]
		public string nombre_imagen { get; set; }
		[Display(Name = "Categoria")]
		public string nom_categoria { get; set; }
		public int id_categoria { get; set; }
		[Display(Name = "Stock")]
		public int stock { get; set; }
		[Display(Name = "Estado")]
		public Boolean activo { get; set; }
	}
}
