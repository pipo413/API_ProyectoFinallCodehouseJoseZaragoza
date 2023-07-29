using API_UsuariosCH.Models;
using API_UsuariosCH.Repository;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace API_UsuariosCH.Controllers
{
    public class LoginController : ApiController
    {
        private ADO_Login adoUsuarioLoggin;

        public LoginController()
        {
            adoUsuarioLoggin = new ADO_Login();
        }


        [HttpGet]
        [Route("Login/{nombreUsuario}/{contraseña}")]
        public IHttpActionResult Loggin(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario()
            {
                NombreUsuario = nombreUsuario,
                Contraseña = contraseña
            };

            Usuario respuesta = adoUsuarioLoggin.Loggin(usuario);

            if (respuesta.NombreUsuario != "")
            {
                return Ok(respuesta.NombreUsuario);
            }
            else
            {
                return NotFound();
            }
        }


    }

}

