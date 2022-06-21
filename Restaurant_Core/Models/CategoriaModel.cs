using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class CategoriaModel:ValidationAttribute
    {
        [Display(Name = "Código de Categoria")]
        public string idCategoria { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "Categoria no puede tener contener mas de 100 caracteres")]
        [MinLength(3, ErrorMessage = "Categoria no puede contener menos de 3 caracteres")]
        [Display(Name = "Nombre de Categoria")]
        public string nomCategoria { get; set; }
    }
}
