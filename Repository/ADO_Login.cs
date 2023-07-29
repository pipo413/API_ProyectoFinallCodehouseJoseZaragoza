using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using API_UsuariosCH.Models; // Asegúrate de importar el espacio de nombres donde se encuentra la clase Usuario.

namespace API_UsuariosCH.Repository
{
    public class ADO_Login
    {
        private readonly DB_Helper helper;

        public ADO_Login()
        {
            helper = new DB_Helper();
        }

        public Usuario Loggin(Usuario usuarioLogin)
        {
            string script = "SELECT id, NombreUsuario FROM Usuario WHERE UPPER(NombreUsuario) LIKE UPPER(@NombreUsuario) AND Contraseña LIKE @Contraseña";

            helper.AddParametro("@NombreUsuario", "%" + usuarioLogin.NombreUsuario + "%", DB_Helper.TipoDato.String);
            helper.AddParametro("@Contraseña", usuarioLogin.Contraseña, DB_Helper.TipoDato.String);

            DataTable dataTable = helper.GetDataTable(script);
            Usuario usuario = new Usuario();

            if(dataTable.Rows.Count > 0)
            {
                usuario.Id = Convert.ToInt32(dataTable.Rows[0]["id"]);
                usuario.NombreUsuario = dataTable.Rows[0]["NombreUsuario"].ToString();
            }

            return usuario;

        }

    }
}
