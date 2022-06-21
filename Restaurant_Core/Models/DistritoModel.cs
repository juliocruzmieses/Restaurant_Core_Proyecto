using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class DistritoModel:ValidationAttribute
    {
        [Display(Name = "Código de Distrito")]
        public int idDistrito { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = " Distrito no puede tener contener mas de 100 caracteres")]
        [MinLength(3, ErrorMessage = "Distrito no puede contener menos de 3 caracteres")]
        [Display(Name = "Nombre de Distrito")]
        public string nomDistrito { get; set; }
    }
}
