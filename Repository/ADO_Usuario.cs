using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using API_UsuariosCH.Models; // Asegúrate de importar el espacio de nombres donde se encuentra la clase Usuario.

namespace API_UsuariosCH.Repository
{
    public class ADO_Usuario
    {
        private readonly DB_Helper helper;

        public ADO_Usuario()
        {
            helper = new DB_Helper();
        }

        public IEnumerable<Usuario> getUsuarios()
        {
            string script = "SELECT * FROM Usuario";
            DataTable dataTable = helper.GetDataTable(script);

            List<Usuario> usuarios = new List<Usuario>();
            foreach (DataRow row in dataTable.Rows)
            {
                Usuario usuario = new Usuario
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Contraseña = row["Contraseña"].ToString(),
                    Mail = row["Mail"].ToString(),
                    NombreUsuario = row["NombreUsuario"].ToString(),
                };
                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public String getNombreUsuarioPorID(int id)
        {
            Usuario usuario = getUsuarioPorId(id);

            if (usuario == null)
            {
                return null;
            }
            else
            {
                return usuario.Nombre;
            }
        }

        public Usuario getUsuarioPorId(int id )
        {
            string script = "SELECT id, Nombre, Apellido, NombreUsuario, Mail FROM Usuario WHERE Id = @id ";
            helper.AddParametro("@id", id, DB_Helper.TipoDato.Int);
            DataTable dataTable = helper.GetDataTable(script);

            if (dataTable.Rows.Count == 0)
            {
                // Si no se encuentra ningún usuario con el ID dado
                return null;
            }

            // Si solo se encuentra un usuario con el ID dado, creamos y devolvemos el objeto Usuario correspondiente.
            DataRow row = dataTable.Rows[0];
            Usuario usuario = new Usuario
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Mail = row["Mail"].ToString(),
                NombreUsuario = row["NombreUsuario"].ToString(),
            };

            return usuario;
        }

        public bool CrearUsuario(Usuario NuevoUsuario)
        {
            string script = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Email)";
            helper.AddParametro("@Nombre", NuevoUsuario.Nombre, DB_Helper.TipoDato.String);
            helper.AddParametro("@Apellido", NuevoUsuario.Apellido, DB_Helper.TipoDato.String);
            helper.AddParametro("@NombreUsuario", NuevoUsuario.NombreUsuario, DB_Helper.TipoDato.String);
            helper.AddParametro("@Contraseña", NuevoUsuario.Contraseña, DB_Helper.TipoDato.String);
            helper.AddParametro("@Email", NuevoUsuario.Mail, DB_Helper.TipoDato.String);
            int rowsAfected = helper.ExecuteNonQuery(script);
            return rowsAfected > 0;
        }

        public bool VerificarUsuarioExistente(Usuario usuario)
        {

            string script = "SELECT Nombre FROM Usuario WHERE UPPER(NombreUsuario) = UPPER(@NombreUsuario) or Mail = @Email ";
            helper.AddParametro("@NombreUsuario", usuario.NombreUsuario, DB_Helper.TipoDato.String);
            helper.AddParametro("@Email", usuario.Mail, DB_Helper.TipoDato.String);
            DataTable dataTable = helper.GetDataTable(script);
            return dataTable.Rows.Count > 0;
        }

        internal bool ModificarUsuario(Usuario usuarioModificado)
        {
            string script = "UPDATE USUARIO SET Nombre = @NOMBRE, Apellido = @APELLIDO, NombreUsuario = @NOMBREUSUARIO, Contraseña = @CONTRASEÑA, Mail = @MAIL WHERE Id = @ID";

            // Agregar los parámetros correspondientes al script.
            helper.AddParametro("@NOMBRE", usuarioModificado.Nombre, DB_Helper.TipoDato.String);
            helper.AddParametro("@APELLIDO", usuarioModificado.Apellido, DB_Helper.TipoDato.String);
            helper.AddParametro("@NOMBREUSUARIO", usuarioModificado.NombreUsuario, DB_Helper.TipoDato.String);
            helper.AddParametro("@CONTRASEÑA", usuarioModificado.Contraseña, DB_Helper.TipoDato.String);
            helper.AddParametro("@MAIL", usuarioModificado.Mail, DB_Helper.TipoDato.String);
            helper.AddParametro("@ID", usuarioModificado.Id, DB_Helper.TipoDato.Int);

            // Ejecutar el script y obtener el número de filas afectadas por la actualización.
            int filasAfectadas = helper.ExecuteNonQuery(script);

            // Retornar true si se actualizó al menos una fila, o false en caso contrario.
            return filasAfectadas > 0;
        }

        internal bool EliminarUsuarioPorId(int id)
        {
            string script = "DELETE USUARIO WHERE Id = @ID";

            // Agregar los parámetros correspondientes al script.
            helper.AddParametro("@ID", id, DB_Helper.TipoDato.Int);

            // Ejecutar el script y obtener el número de filas afectadas por la actualización.
            int filasAfectadas = helper.ExecuteNonQuery(script);

            // Retornar true si se actualizó al menos una fila, o false en caso contrario.
            return filasAfectadas > 0;
        }

        internal Usuario getUsuarioPorId(int? id)
        {
            throw new NotImplementedException();
        }
    }

}