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
    public class MesaController : Controller
    {

        private readonly IConfiguration _IConfig;

        public MesaController(IConfiguration IConfig)
        {
            _IConfig = IConfig;
        }

        // Listar Mesa
        IEnumerable<MesaModel> listaMesas()
        {
            List<MesaModel> lista = new List<MesaModel>();
            SqlConnection cn = new SqlConnection(_IConfig["ConnectionStrings:cn"]);
            SqlCommand cmd = new SqlCommand("usp_listarMesa", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MesaModel objMesa = new MesaModel()
                    {
                        idMesa = dr[0].ToString(),
                        descMesa = dr[1].ToString()
                    };
                    lista.Add(objMesa);
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
        public async Task<IActionResult> ListadoMesas()
        {
            return View(await Task.Run(() => listaMesas()));
        }

        // Buscar Mesa
        MesaModel BuscarMesa(string id)
        {
            MesaModel reg = listaMesas().Where(c => c.idMesa == id).FirstOrDefault();
            return reg;
        }

        // Crear Mesa
        public async Task<IActionResult> Create()
        {
            return View(new MesaModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(MesaModel reg)
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
                    SqlCommand cmd = new SqlCommand("usp_Merge_Mesas", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idmesa", reg.idMesa);
                    cmd.Parameters.AddWithValue("@descmesa", reg.descMesa);

                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"¡Se agrego {num} mesa(s) correctamente!";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                return View(reg);
            }
        }

        // Editar Mesa
        public IActionResult Edit(string id)
        {
            MesaModel reg = BuscarMesa(id);
            if (reg == null)
                return RedirectToAction("ListadoMesas");
            return View(reg);
        }

        [HttpPost]

        public IActionResult Edit(MesaModel reg)
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
                    SqlCommand cmd = new SqlCommand("usp_Merge_Mesas", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idmesa", reg.idMesa);
                    cmd.Parameters.AddWithValue("@descmesa", reg.descMesa);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {num} mesa (s)";
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
