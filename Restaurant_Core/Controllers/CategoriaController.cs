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
    public class CategoriaController : Controller
    {

        private readonly IConfiguration _IConfig;

        public CategoriaController(IConfiguration IConfig)
        {
            _IConfig = IConfig;
        }

        // Listar Categoria
        IEnumerable<CategoriaModel> listaCategorias()
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();
            SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("usp_listarCategoria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CategoriaModel objCategoria = new CategoriaModel()
                    {
                        idCategoria = dr[0].ToString(),
                        nomCategoria = dr[1].ToString()
                    };
                    lista.Add(objCategoria);
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return lista;
        }
        public async Task<IActionResult> ListadoCategorias()
        {
            return View(await Task.Run(() => listaCategorias()));
        }

        // Buscar Categoria
        CategoriaModel BuscarCategoria(string id)
        {
            CategoriaModel reg = listaCategorias().Where(c => c.idCategoria == id).FirstOrDefault();
            return reg;
        }

        // Crear Categoria
        public async Task<IActionResult> Create()
        {
            return View(new CategoriaModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaModel reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }

            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Merge_Categoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcategoria", reg.idCategoria);
                    cmd.Parameters.AddWithValue("@nomcategoria", reg.nomCategoria);

                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"¡Se agrego {num} categoria(s) correctamente!";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                return View(reg);
            }
        }

        // Editar Categoria
        public IActionResult Edit(string id)
        {
            CategoriaModel reg = BuscarCategoria(id);
            if (reg == null)
                return RedirectToAction("ListadoCategorias");
            return View(reg);
        }

        [HttpPost]

        public IActionResult Edit(CategoriaModel reg)
        {
            if (!ModelState.IsValid)
            {
                return View(reg);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Merge_Categoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcategoria", reg.idCategoria);
                    cmd.Parameters.AddWithValue("@nomcategoria", reg.nomCategoria);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {num} categoria (s)";
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
