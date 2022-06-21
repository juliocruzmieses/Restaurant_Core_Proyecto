using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class RolModel:ValidationAttribute

    {
        [Display(Name = "Código de Rol")]
        public int idRol { get; set; }



        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(30, ErrorMessage = "Rol no puede tener contener mas de 30 caracteres")]
        [MinLength(3, ErrorMessage = "Rol no puede contener menos de 3 caracteres")]
        [Display(Name = "Nombre de Rol")]
        public string nomRol { get; set; }
    }
}
