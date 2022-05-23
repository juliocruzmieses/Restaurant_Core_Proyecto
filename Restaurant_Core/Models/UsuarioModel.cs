using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Core.Models
{
    public class UsuarioModel
    {

        public int id_usuario { get; set; }
        public string nom_usuario { get; set; }
        public string ape_usuario { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string fono_user { get; set; }
        public int id_rol { get; set; }
        public int id_distrito { get; set; }
        public int estado { get; set; }

    }
}
