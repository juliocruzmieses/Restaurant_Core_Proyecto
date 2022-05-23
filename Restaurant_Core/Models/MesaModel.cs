using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class MesaModel
    {
        [Display(Name = "Código de Mesa")]
        public string idMesa { get; set; }
        [Display(Name = "N° de Mesa")]
        public string descMesa { get; set; }
    }
}
