using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class RolModel
    {
        [Display(Name = "Código de Rol")]
        public int idRol { get; set; }
        [Display(Name = "Nombre de Rol")]
        public string nomRol { get; set; }
    }
}
