using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class CategoriaModel
    {
        [Display(Name = "Código de Categoria")]
        public string idCategoria { get; set; }
        [Display(Name = "Nombre de Categoria")]
        public string nomCategoria { get; set; }
    }
}
