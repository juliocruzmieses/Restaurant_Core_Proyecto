using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Restaurant_Core.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant_Core.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IConfiguration _Iconfig;
                
        public RestaurantController(IConfiguration Iconfig)
        {
            _Iconfig = Iconfig;
        }              

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]      

        public IActionResult Login(UsuarioModel objUsuario)
        {
            objUsuario.pass = ConvertirSha256(objUsuario.pass);

            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ValidarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("username", objUsuario.username);
                cmd.Parameters.AddWithValue("pass", objUsuario.pass);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objUsuario.id_usuario = dr.GetInt32(0);
                    objUsuario.nom_usuario = dr.GetString(1);
                    objUsuario.ape_usuario = dr.GetString(2);
                    objUsuario.username = dr.GetString(3);
                    objUsuario.email = dr.GetString(4);
                    objUsuario.fono_user = dr.GetString(5);
                    objUsuario.id_rol = dr.GetInt32(6);
                    objUsuario.id_distrito = dr.GetInt32(7);
                    objUsuario.estado = dr.GetInt32(8);
                }
            }
            if (objUsuario.id_usuario != 0) 
            {                
                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(objUsuario));               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }            
        }
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }
}
