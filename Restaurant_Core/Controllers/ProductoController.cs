using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Restaurant_Core.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant_Core.Controllers
{
    public class ProductoController : Controller
    {
        
        private readonly IConfiguration _Iconfig;
        
        public ProductoController(IConfiguration Iconfig)
        {
            _Iconfig = Iconfig;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// 

        IEnumerable<CategoriaModel> listaCategorias()
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();
            SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]);
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        ///
        IEnumerable<ProductoModel> Productos()
        {
            List<ProductoModel> lista = new List<ProductoModel>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("spListarProducto", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ProductoModel()
                    {
                        id_producto = dr.GetInt32(0),
                        nom_producto = dr.GetString(1),
                        precio = dr.GetDecimal(2),
                        ruta_imagen = dr.GetString(3),
                        nombre_imagen = dr.GetString(4),
                        id_categoria = dr.GetInt32(5),
                        nom_categoria = dr.GetString(6),
                        stock = dr.GetInt32(7),
                        activo = dr.GetBoolean(8)
                    });
                }
            }
            return lista;
        }

        public async Task<IActionResult> ListadoProductos()
        {
            return View(await Task.Run(() => Productos()));
        }

        ProductoModel BuscarProductos(int id)
        {
            ProductoModel reg = Productos().Where(c => c.id_producto == id).FirstOrDefault();
            return reg;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria");
            return View(new ProductoModel());
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductoModel reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria", reg.id_categoria);
                return View(reg);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("SPRegistrarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomproducto", reg.nom_producto);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@rutaimag", reg.ruta_imagen);
                    cmd.Parameters.AddWithValue("@nomimag", reg.nombre_imagen);
                    cmd.Parameters.AddWithValue("@idcategoria", reg.id_categoria);
                    cmd.Parameters.AddWithValue("@stock", reg.stock);
                    cmd.Parameters.AddWithValue("@activo", reg.activo);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {num} producto (s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria", reg.id_categoria);
                return View(reg);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductoModel reg = BuscarProductos(id);
            if (reg == null)
                return RedirectToAction("ListadoProductos");
            ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria", reg.id_categoria);
            return View(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductoModel reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria", reg.id_categoria);
                return View(reg);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("spactualizarproducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idproducto", reg.id_producto);
                    cmd.Parameters.AddWithValue("@nomproducto", reg.nom_producto);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@rutaimag", reg.ruta_imagen);
                    cmd.Parameters.AddWithValue("@nomimag", reg.nombre_imagen);
                    cmd.Parameters.AddWithValue("@idcategoria", reg.id_categoria);
                    cmd.Parameters.AddWithValue("@stock", reg.stock);
                    cmd.Parameters.AddWithValue("@activo", reg.activo);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {num} producto (s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                ViewBag.categorias = new SelectList(await Task.Run(() => listaCategorias()), "idCategoria", "nomCategoria", reg.id_categoria);
                return View(reg);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
