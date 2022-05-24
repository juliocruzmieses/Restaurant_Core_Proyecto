using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Restaurant_Core.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Restaurant_Core.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _Iconfig;
        public UsuarioController(IConfiguration Iconfig)
        {
            _Iconfig = Iconfig;
        }

        IEnumerable<UsuarioModel> Usuarios()
        {
            List<UsuarioModel> lista = new List<UsuarioModel>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarUsuarios", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new UsuarioModel()
                    {
                        id_usuario = dr.GetInt32(0),
                        nom_usuario = dr.GetString(1),
                        ape_usuario = dr.GetString(2),
                        username = dr.GetString(3),
                        email = dr.GetString(4),
                        fono_user = dr.GetString(5),
                        nom_rol = dr.GetString(6),
                        nom_distrito = dr.GetString(7)
                    });
                }
            }
            return lista;
        }

        public async Task<IActionResult> ListadoUsuarios()
        {
            return View(await Task.Run(() => Usuarios()));
        }

        UsuarioModel BuscarUsuario(int id)
        {
            UsuarioModel reg = Usuarios().Where(c => c.id_usuario == id).FirstOrDefault();
            return reg;
        }

        public IActionResult Create()
        {
            return View(new UsuarioModel());
        }

        [HttpPost]
        public IActionResult Create(UsuarioModel reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_RegistrarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom_usuario", reg.nom_usuario);
                    cmd.Parameters.AddWithValue("@ape_usuario", reg.ape_usuario);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    cmd.Parameters.AddWithValue("@fono_user", reg.fono_user);
                    cmd.Parameters.AddWithValue("@id_rol", reg.id_rol);
                    cmd.Parameters.AddWithValue("@id_distrito", reg.id_distrito);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {num} usuario (s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                return View(reg);
            }
        }

        public IActionResult Edit(int id)
        {
            UsuarioModel reg = BuscarUsuario(id);
            if (reg == null)
                return RedirectToAction("ListadoUsuarios");
            return View(reg);
        }

        [HttpPost]
        public IActionResult Edit(UsuarioModel reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_ActualizarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", reg.id_usuario);
                    cmd.Parameters.AddWithValue("@nom_usuario", reg.nom_usuario);
                    cmd.Parameters.AddWithValue("@ape_usuario", reg.ape_usuario);
                    cmd.Parameters.AddWithValue("@email", reg.email);
                    cmd.Parameters.AddWithValue("@fono_user", reg.fono_user);
                    cmd.Parameters.AddWithValue("@id_rol", reg.id_rol);
                    cmd.Parameters.AddWithValue("@id_distrito", reg.id_distrito);
                    cmd.Parameters.AddWithValue("@estado", reg.estado);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {num} usuario (s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                return View(reg);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
