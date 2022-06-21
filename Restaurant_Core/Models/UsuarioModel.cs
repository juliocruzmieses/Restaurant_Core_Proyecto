using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Core.Models
{
    public class UsuarioModel: ValidationAttribute
    {



        [Display(Name = "Código de Usuario")]
        public int id_usuario { get; set; }

        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage="El nombre no puede tener contener mas de 100 caracteres")]
        [MinLength(2,ErrorMessage="El nombre no puede contener menos de dos caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string nom_usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Apellido no puede tener contener mas de 100 caracteres")]
        [MinLength(2, ErrorMessage = "El Apellido no puede contener menos de dos caracteres")]
        [Display(Name = "Apellido de Usuario")]
        public string ape_usuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(10, ErrorMessage = "El username no puede tener contener mas de 10 caracteres")]
        [MinLength(2, ErrorMessage = "El username no puede contener menos de 2 caracteres")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
       // [Expresión Regular(@" ^(?=\w*\d)(?=\w*[AZ])(?=\w*[az])\S{8,16}$ ", ErrorMessage = " La contraseña debe tener entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula y al menos una mayúscula. ")]
        public string pass { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@" ^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$ ", ErrorMessage = " Formato incorrecto. ")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1,9,ErrorMessage="Telefono solo puede tener 9 carateres")]
        [Display(Name = "Telefono")]
        public string fono_user { get; set; }
        public int id_rol { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(20, ErrorMessage = "El Rol no puede tener contener mas de 20 caracteres")]
        [MinLength(2, ErrorMessage = "El Rol no puede contener menos de 2 caracteres")]
        [Display(Name = "Rol")]
        public string nom_rol { get; set; }

        [Display(Name = "Distrito")]
        public int id_distrito { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(30, ErrorMessage = "El Distrito no puede tener contener mas de 30 caracteres")]
        [MinLength(3, ErrorMessage = "El Distrito no puede contener menos de 3 caracteres")]
        [Display(Name = "Distrito")]
        public string nom_distrito { get; set; }

       // [Required(ErrorMessage = "Este campo es obligatorio")]
        public int estado { get; set; }
    }
}
