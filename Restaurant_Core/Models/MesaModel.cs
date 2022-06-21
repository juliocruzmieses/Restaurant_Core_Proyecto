using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class MesaModel:ValidationAttribute
    {
        [Display(Name = "Código de Mesa")]
        public string idMesa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MinLength(6,ErrorMessage="Debe contener minimo 6 caracteres")]
        [Display(Name = "N° de Mesa")]
        public string descMesa { get; set; }
    }
}
