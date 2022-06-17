using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Restaurant_Core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Restaurant_Core.Controllers
{
    public class CarritoController : Controller
    {
        
        private readonly IConfiguration _Iconfig;

        public CarritoController(IConfiguration Iconfig)
        {
            _Iconfig = Iconfig;
        }


        IEnumerable<Carrito> Productos()
        {
            List<Carrito> lista = new List<Carrito>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("spListarProducto", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Carrito()
                    {
                        id_producto = dr.GetInt32(0),
                        nom_producto = dr.GetString(1),
                        precio = dr.GetDecimal(2),
                        ruta_imagen = dr.GetString(3),
                        nombre_imagen = dr.GetString(4),
                        nom_categoria = dr.GetString(5),
                        stock = dr.GetInt32(6),
                        activo = dr.GetBoolean(7)
                    });
                }
            }
            return lista;
        }

        public async Task<IActionResult> Listado()
        {
            return View(await Task.Run(() => Productos()));
        }



          IEnumerable<MesaModel> Mesas()
        {
            List<MesaModel> listaMesa = new List<MesaModel>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_listarMesa", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listaMesa.Add(new MesaModel()
                    {
                        idMesa = dr.GetString(0),
                        descMesa = dr.GetString(1),
                 
                    });
                }
            }
            return listaMesa;
        }

        public async Task<IActionResult> ListadoMesas()
        {
            return View(await Task.Run(() => Mesas()));
        }



        public IActionResult Index()
        {
            return View();
        }

    }
}
