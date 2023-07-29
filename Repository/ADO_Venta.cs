using API_UsuariosCH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace API_UsuariosCH.Repository
{
    public class ADO_Venta
    {
        private readonly DB_Helper helper;

        public ADO_Venta()
        {
            helper = new DB_Helper();
        }

        public IEnumerable<Venta> getventas()
        {
            string script = "SELECT * FROM venta";
            DataTable dataTable = helper.GetDataTable(script);

            List<Venta> ventas = new List<Venta>();
            foreach (DataRow row in dataTable.Rows)
            {
                Venta venta = new Venta()
                {
                    Id = row["Id"].ToString(),
                    Comentarios = row["Comentarios"].ToString(),
                    IdUsuar = row["IdUsuario"].ToString()
                };
                ventas.Add(venta);
            }
            return ventas;
        }

        public Venta GetVentaPorId(int id)
        {
            string script = "SELECT Id, Comentarios, IdUsuario  FROM venta WHERE id = @id ";
            helper.AddParametro("@id", id, DB_Helper.TipoDato.Int);
            DataTable dataTable = helper.GetDataTable(script);

            if (dataTable.Rows.Count == 0)
            {
                // Si no se encuentra ningún venta con el ID dado
                return null;
            }

            // Si solo se encuentra un venta con el ID dado, creamos y devolvemos el objeto venta correspondiente.
            DataRow row = dataTable.Rows[0];
            Venta venta = new Venta
            {
                Id = row["Id"].ToString(),
                Comentarios = row["Comentarios"].ToString(),
                IdUsuar = row["IdUsuario"].ToString()
            };

            return venta;
        }


        public Venta Crearventa(Venta NuevaVenta)
        {
            string script = "INSERT INTO venta (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario)";
            helper.AddParametro("@Comentarios", NuevaVenta.Comentarios, DB_Helper.TipoDato.String);
            helper.AddParametro("@IdUsuario", NuevaVenta.IdUsuar, DB_Helper.TipoDato.Int);
 
            int rowsAffected = helper.ExecuteNonQuery(script);

            if (rowsAffected == 1)
            {
                return NuevaVenta;
            }
            else
            {
                return null;
            }
        }

        internal bool EliminarventaPorId(int id)
        {
            string script = "DELETE venta WHERE Id = @ID";

            // Agregar los parámetros correspondientes al script.
            helper.AddParametro("@ID", id, DB_Helper.TipoDato.Int);

            // Ejecutar el script y obtener el número de filas afectadas por la actualización.
            int filasAfectadas = helper.ExecuteNonQuery(script);

            // Retornar true si se actualizó al menos una fila, o false en caso contrario.
            return filasAfectadas > 0;
        }

    }
}