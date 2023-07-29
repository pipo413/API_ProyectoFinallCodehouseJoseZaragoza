using API_UsuariosCH.Models;
using API_UsuariosCH.Repository;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Results;

namespace API_UsuariosCH.Controllers
{
    public class ProductoController : ApiController
    {
        private ADO_Producto adoProducto;

        public ProductoController()
        {
            adoProducto = new ADO_Producto();
        }
        [HttpGet]
        [Route("Producto/{id?}")]
        public IHttpActionResult GetProductos(int? id = null)
        {
            try
            {
                if (id.HasValue)
                {
                    // Si se proporciona el parámetro {id}, buscamos un Producto específico por su ID.
                    Producto producto = adoProducto.GetProductoPorId(id.Value);
                    if (producto != null)
                    {
                        // Si se encontró un Producto con el ID dado, devolvemos una respuesta Ok con ese Producto.
                        return Ok(producto);
                    }
                    else
                    {
                        // Si no se encontró un Producto con el ID dado, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
                else
                {
                    // Si no se proporciona el parámetro {id}, obtenemos todos los Productos.
                    var productos = adoProducto.getProducto();

                    if (productos.Any())
                    {
                        // Si se encontraron Productos, devolvemos una respuesta Ok con la lista de Productos.
                        return Ok(productos);
                    }
                    else
                    {
                        // Si no se encontraron Productos, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Si se produce una excepción, devolvemos una respuesta BadRequest con el mensaje de error.
                return BadRequest("Ocurrió un error al obtener los usuarios: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Producto/NuevoProducto/")]
        public IHttpActionResult NuevoProducto([FromBody] Producto nuevoProducto)
        {
            // Validamos que la descripcion del producto no esté vacía.
            if (string.IsNullOrEmpty(nuevoProducto.Descripciones))
            {
                return BadRequest("Debe ingresar una descripcion de producto válida.");
            }

            // Creamos un objeto Producto con los datos proporcionados por el Producto.
            Producto productoNuevo = new Producto()
            {
                Descripciones=nuevoProducto.Descripciones,
                Costo = nuevoProducto.Costo,
                IdUsuario = nuevoProducto.IdUsuario,
                PrecioVenta = nuevoProducto.PrecioVenta,
                Stock = nuevoProducto.Stock 
            };

            // Verificamos si el usuario ya existe en la base de datos.
            if (!IdUsuarioValido(nuevoProducto.IdUsuario))
            {
                return BadRequest("El usuario no existe en la base de datos.");
            }

            try
            {
                // Intentamos crear el usuario en la base de datos.
                Producto respuesta = adoProducto.CrearProducto(nuevoProducto);

                // Verificamos si la creación del usuario fue exitosa.
                if (respuesta != null)
                {
                    // Si fue exitosa, devolvemos una respuesta Ok con el objeto Producto creado.
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
                return BadRequest("Ocurrió un error al intentar crear nuevo producto: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("Producto/ModificarProducto/{id}")]
        public IHttpActionResult ModificarProducto(int id, [FromBody] Producto productoModificado)
        {
            // Verificamos que el ID proporcionado sea válido (mayor que cero).
            if (id <= 0)
            {
                return BadRequest("Debe ingresar un ID de producto válido.");
            }

            // Verificamos que el nombre de producto no sea nulo o vacío.
            if (string.IsNullOrEmpty(productoModificado.Descripciones))
            {
                return BadRequest("Debe ingresar una descripcion de producto válida.");
            }

            // Verificamos si el usuarioId ya existe en la base de datos.
            if (!IdUsuarioValido(productoModificado.IdUsuario))
            {
                return BadRequest("El UsuarioId no existe en la base de datos.");
            }

            try
            {
                // Antes de modificar el producto, verificamos si existe en la base de datos.
                Producto productoExistente = adoProducto.GetProductoPorId(id);
                if (productoExistente == null)
                {
                    return NotFound(); // Si el producto no existe, devolvemos NotFound.
                }

                // Actualizamos los datos del producto existente con los datos proporcionados por el cliente.
                Producto productoNuevo = new Producto()
                {
                    Id = id.ToString(),
                    Descripciones = productoModificado.Descripciones,
                    Costo = productoModificado.Costo,
                    IdUsuario = productoModificado.IdUsuario,
                    PrecioVenta = productoModificado.PrecioVenta,
                    Stock = productoModificado.Stock
                };

                // Intentamos guardar los cambios en la base de datos.
                bool exito = adoProducto.ModificarProducto(productoNuevo);

                if (exito)
                {
                    // Si la modificación fue exitosa, devolvemos una respuesta Ok con el objeto Producto modificado.
                    return Ok(productoNuevo);
                }
                else
                {
                    // Si hubo un problema en la modificación del Producto, devolvemos una respuesta InternalServerError.
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                // Si se produjo una excepción durante la modificación del Producto, devolvemos una respuesta BadRequest
                // con un mensaje de error personalizado que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar modificar el Producto: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("Producto/EliminarProducto/{id}")]
        public IHttpActionResult EliminarProducto(int id)
        {
            try
            {
                // Verificamos que el ID proporcionado sea válido (mayor que cero).
                if (id <= 0)
                {
                    return BadRequest("Debe ingresar un ID de producto válido.");
                }
                // Primero, verifica si el Producto existe en la base de datos.
                Producto productoExistente = adoProducto.GetProductoPorId(id);
                if (productoExistente == null)
                {
                    // Si el Producto no existe, devuelve un error NotFound (HTTP 404).
                    return NotFound();
                }

                // Si el Producto existe, eliminarlo.
                bool eliminacionExitosa = adoProducto.EliminarProductoPorId(id);
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
                // Si ocurre una excepción durante la eliminación o al obtener el Producto,
                // devuelve una respuesta BadRequest (HTTP 400) con un mensaje de error que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar eliminar el producto: " + ex.Message);
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
