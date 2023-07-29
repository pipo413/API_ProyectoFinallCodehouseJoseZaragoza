using API_UsuariosCH.Models;
using API_UsuariosCH.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_UsuariosCH.Controllers
{
    public class ProductoVendidoController : ApiController
    {
        private ADO_ProductoVendido adoProductoVendido;

        public ProductoVendidoController()
        {
            adoProductoVendido = new ADO_ProductoVendido();
        }
        [HttpGet]
        [Route("ProductoVendido/{id?}")]
        public IHttpActionResult GetProductoVendido(int? id = null)
        {
            try
            {
                if (id.HasValue)
                {
                    // Si se proporciona el parámetro {id}, buscamos un  ProductoVendido específico por su ID.
                    ProductoVendido productoVendido = adoProductoVendido.GetProductoVendidoPorId(id.Value);
                    if (productoVendido != null)
                    {
                        // Si se encontró un  ProductoVendido con el ID dado, devolvemos una respuesta Ok con ese ProductoVendido.
                        return Ok(productoVendido);
                    }
                    else
                    {
                        // Si no se encontró un  ProductoVendido con el ID dado, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
                else
                {
                    // Si no se proporciona el parámetro {id}, obtenemos todos los ProductoVendidos.
                    var ProductoVendidos = adoProductoVendido.getProductoVendidos();

                    if (ProductoVendidos.Any())
                    {
                        // Si se encontraron ProductoVendidos, devolvemos una respuesta Ok con la lista de ProductoVendidos.
                        return Ok(ProductoVendidos);
                    }
                    else
                    {
                        // Si no se encontraron ProductoVendidos, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, devolvemos una respuesta BadRequest con el mensaje de error.
                return BadRequest("Ocurrió un error al obtener las ProductoVendidos: " + ex.Message);
            }
        }
    }
}