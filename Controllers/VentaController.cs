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
    public class VentaController : ApiController
    {
        private ADO_Venta adoVenta;

        public VentaController()
        {
            adoVenta = new ADO_Venta();
        }
        [HttpGet]
        [Route("Venta/{id?}")]
        public IHttpActionResult GetVenta(int? id = null)
        {
            try
            {
                if (id.HasValue)
                {
                    // Si se proporciona el parámetro {id}, buscamos un  venta específico por su ID.
                    Venta  venta = adoVenta.GetVentaPorId(id.Value);
                    if ( venta != null)
                    {
                        // Si se encontró un  venta con el ID dado, devolvemos una respuesta Ok con ese venta.
                        return Ok(venta);
                    }
                    else
                    {
                        // Si no se encontró un  venta con el ID dado, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
                else
                {
                    // Si no se proporciona el parámetro {id}, obtenemos todos los ventas.
                    var ventas = adoVenta.getventas();

                    if (ventas.Any())
                    {
                        // Si se encontraron ventas, devolvemos una respuesta Ok con la lista de ventas.
                        return Ok(ventas);
                    }
                    else
                    {
                        // Si no se encontraron ventas, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, devolvemos una respuesta BadRequest con el mensaje de error.
                return BadRequest("Ocurrió un error al obtener las ventas: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Venta/NuevaVenta/")]
        public IHttpActionResult NuevaVenta([FromBody] Venta nuevoventa)
        {
            // Validamos que la descripcion del venta no esté vacía.
            if (string.IsNullOrEmpty(nuevoventa.Comentarios))
            {
                return BadRequest("Debe ingresar una descripcion de venta válida.");
            }

            // Creamos un objeto venta con los datos proporcionados por el venta.
            Venta ventaNuevo = new Venta()
            {
                Comentarios = nuevoventa.Comentarios,
                IdUsuar = nuevoventa.IdUsuar,
            };

            // Verificamos si el usuario ya existe en la base de datos.
            if (!IdUsuarioValido(Convert.ToInt16(nuevoventa.IdUsuar)))
            {
                return BadRequest("El usuario no existe en la base de datos.");
            }

            try
            {
                // Intentamos crear el usuario en la base de datos.
                Venta respuesta = adoVenta.Crearventa(nuevoventa);

                // Verificamos si la creación del usuario fue exitosa.
                if (respuesta != null)
                {
                    // Si fue exitosa, devolvemos una respuesta Ok con el objeto venta creado.
                    return Ok(respuesta);
                }
                else
                {
                    // Si hubo un problema en la creación del usuario, devolvemos una respuesta InternalServerError.
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                // Si se produjo una excepción durante la creación del usuario, devolvemos una respuesta BadRequest
                // con un mensaje de error personalizado que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar crear nuevo venta: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("venta/Eliminarventa/{id}")]
        public IHttpActionResult Eliminarventa(int id)
        {
            try
            {
                // Verificamos que el ID proporcionado sea válido (mayor que cero).
                if (id <= 0)
                {
                    return BadRequest("Debe ingresar un ID de venta válido.");
                }
                // Primero, verifica si el venta existe en la base de datos.
                Venta ventaExistente = adoVenta.GetVentaPorId(id);
                if (ventaExistente == null)
                {
                    // Si el venta no existe, devuelve un error NotFound (HTTP 404).
                    return NotFound();
                }

                // Si el venta existe, eliminarlo.
                bool eliminacionExitosa = adoVenta.EliminarventaPorId(id);
                if (eliminacionExitosa)
                {
                    // Si la eliminación fue exitosa, devuelve una respuesta Ok (HTTP 200).
                    return Ok();
                }
                else
                {
                    // Si la eliminación falló, devuelve una respuesta InternalServerError (HTTP 500).
                    return InternalServerError();
                }
            }

            catch (Exception ex)
            {
                // Si ocurre una excepción durante la eliminación o al obtener el venta,
                // devuelve una respuesta BadRequest (HTTP 400) con un mensaje de error que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar eliminar el venta: " + ex.Message);
            }
        }

        private bool IdUsuarioValido(int idUsuario)
        {
            ADO_Usuario adoUsuario = new ADO_Usuario();
            Usuario usu = adoUsuario.getUsuarioPorId(idUsuario);

            return usu != null;
        }
    }


}
