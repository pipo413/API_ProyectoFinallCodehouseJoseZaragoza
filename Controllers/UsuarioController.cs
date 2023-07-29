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
    public class UsuarioController : ApiController
    {
        private ADO_Usuario adoUsuario;

        public UsuarioController()
        {
            adoUsuario = new ADO_Usuario();
        }
        [HttpGet]
        [Route("Usuarios/{id?}")]
        public IHttpActionResult GetUsuarios(int? id = null)
        {
            try
            {

                // Verificamos que el ID proporcionado sea válido (mayor que cero).
                if (id <= 0)
                {
                    return BadRequest("Debe ingresar un ID válido.");
                }

                if (id.HasValue)
                {
                    // Si se proporciona el parámetro {id}, buscamos un usuario específico por su ID.
                    Usuario usuario = adoUsuario.getUsuarioPorId(id.Value);
                    if (usuario != null)
                    {
                        // Si se encontró un usuario con el ID dado, devolvemos una respuesta Ok con ese usuario.
                        return Ok(usuario);
                    }
                    else
                    {
                        // Si no se encontró un usuario con el ID dado, devolvemos una respuesta NotFound.
                        return NotFound();
                    }
                }
                else
                {
                    // Si no se proporciona el parámetro {id}, obtenemos todos los usuarios.
                    var usuarios = adoUsuario.getUsuarios();

                    if (usuarios.Any())
                    {
                        // Si se encontraron usuarios, devolvemos una respuesta Ok con la lista de usuarios.
                        return Ok(usuarios);
                    }
                    else
                    {
                        // Si no se encontraron usuarios, devolvemos una respuesta NotFound.
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

        [HttpGet]
        [Route("Usuarios/NombreUsuario/{id}")]
        public IHttpActionResult GetNombreUsuarioPorId(int? id = null)
        {
            // Verificamos que el ID proporcionado sea válido (mayor que cero).
            if (id <= 0 || id == null)
            {
                return BadRequest("Debe ingresar un ID válido.");
            }

            try
            {
                // Obtenemos el nombre de usuario por el ID proporcionado.
                string respuesta = adoUsuario.getNombreUsuarioPorID((int)id);

                // Verificamos si se encontró un nombre de usuario con el ID dado.
                if (!string.IsNullOrEmpty(respuesta))
                {
                    // Si se encontró, devolvemos una respuesta Ok con el nombre de usuario.
                    return Ok(respuesta);
                }
                else
                {
                    // Si no se encontró un nombre de usuario con el ID dado, devolvemos una respuesta NotFound.
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Si se produce cualquier otra excepción, devolvemos una respuesta BadRequest con un mensaje de error que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar obtener el nombre de usuario: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("Usuarios/NuevoUsuario/")]
        public IHttpActionResult NuevoUsuario([FromBody] Usuario nuevoUsuario)
        {
            // Validamos que el nombre de usuario no sea nulo o vacío.
            if (string.IsNullOrEmpty(nuevoUsuario.NombreUsuario))
            {
                return BadRequest("Debe ingresar un nombre de usuario válido.");
            }

            // Creamos un objeto Usuario con los datos proporcionados por el cliente.
            Usuario usuarioNuevo = new Usuario()
            {
                Nombre = nuevoUsuario.Nombre,
                Apellido = nuevoUsuario.Apellido,
                NombreUsuario = nuevoUsuario.NombreUsuario,
                Contraseña = nuevoUsuario.Contraseña,
                Mail = nuevoUsuario.Mail
            };

            // Verificamos si el usuario ya existe en la base de datos.
            if (adoUsuario.VerificarUsuarioExistente(usuarioNuevo))
            {
                return Content(HttpStatusCode.Conflict, "El usuario ya existe en la base de datos.");
            }

            try
            {
                // Intentamos crear el usuario en la base de datos.
                bool respuestaOk = adoUsuario.CrearUsuario(usuarioNuevo);

                // Verificamos si la creación del usuario fue exitosa.
                if (respuestaOk)
                {
                    // Si fue exitosa, devolvemos una respuesta Ok con el objeto Usuario creado.
                    return Ok(usuarioNuevo);
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
                return BadRequest("Ocurrió un error al intentar crear el usuario: " + ex.Message);
            }
        }


        [HttpPut]
        [Route("Usuarios/ModificarUsuario/")]
        public IHttpActionResult ModificarUsuario([FromBody] Usuario usuarioModificado)
        {

            // Verificamos que el ID proporcionado sea válido (mayor que cero).
            if ( usuarioModificado.Id <= 0)
            {
                // Si el Id es null o menor o igual a cero, devolvemos un BadRequest con un mensaje de error.
                return BadRequest("Debe proporcionar un ID válido para modificar el usuario.");
            }

            // Verificamos que el nombre de usuario no sea nulo o vacío.
            if (string.IsNullOrEmpty(usuarioModificado.NombreUsuario))
            {
                return BadRequest("Debe ingresar un nombre de usuario válido.");
            }

            try
            {
                // Antes de modificar el usuario, verificamos si existe en la base de datos.
                Usuario usuarioExistente = adoUsuario.getUsuarioPorId(usuarioModificado.Id);
                if (usuarioExistente == null)
                {
                    return NotFound(); // Si el usuario no existe, devolvemos NotFound.
                }

                // Actualizamos los datos del usuario existente con los datos proporcionados por el cliente.
                Usuario usuario = new Usuario()
                {
                    Nombre = usuarioModificado.Nombre,
                    Apellido = usuarioModificado.Apellido,
                    NombreUsuario = usuarioModificado.NombreUsuario,
                    Contraseña = usuarioModificado.Contraseña,
                    Mail = usuarioModificado.Mail
                };

                // Intentamos guardar los cambios en la base de datos.
                bool exito = adoUsuario.ModificarUsuario(usuarioModificado);

                if (exito)
                {
                    // Si la modificación fue exitosa, devolvemos una respuesta Ok con el objeto Usuario modificado.
                    return Ok(usuario);
                }
                else
                {
                    // Si hubo un problema en la modificación del usuario, devolvemos una respuesta InternalServerError.
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {
                // Si se produjo una excepción durante la modificación del usuario, devolvemos una respuesta BadRequest
                // con un mensaje de error personalizado que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar modificar el usuario: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("Usuarios/EliminarUsuario/{id}")]
        public IHttpActionResult EliminarUsuario(int? id=null)
        {

            // Verificamos que el ID proporcionado sea válido (mayor que cero).
            
            if (id <= 0 || id == null)
            {
                return BadRequest("Debe ingresar un ID válido.");
            }
            try
            {
                // Primero, verifica si el usuario existe en la base de datos.
                Usuario usuarioExistente = adoUsuario.getUsuarioPorId((int)id);
                if (usuarioExistente == null)
                {
                    // Si el usuario no existe, devuelve un error NotFound (HTTP 404).
                    return NotFound();
                }

                // Si el usuario existe, procede a eliminarlo.
                bool eliminacionExitosa = adoUsuario.EliminarUsuarioPorId((int)id);
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
                // Si ocurre una excepción durante la eliminación o al obtener el usuario,
                // devuelve una respuesta BadRequest (HTTP 400) con un mensaje de error que indica la causa del problema.
                return BadRequest("Ocurrió un error al intentar eliminar el usuario: " + ex.Message);
            }
        }


    }
}
