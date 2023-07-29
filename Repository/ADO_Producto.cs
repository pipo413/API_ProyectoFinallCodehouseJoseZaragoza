using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using API_UsuariosCH.Models; // Asegúrate de importar el espacio de nombres donde se encuentra la clase Usuario.

namespace API_UsuariosCH.Repository
{
    public class ADO_Producto
    {
        private readonly DB_Helper helper;

        public ADO_Producto()
        {
            helper = new DB_Helper();
        }

        public IEnumerable<Producto> getProducto()
        {
            string script = "SELECT * FROM Producto";
            DataTable dataTable = helper.GetDataTable(script);

            List<Producto> productos = new List<Producto>();
            foreach (DataRow row in dataTable.Rows)
            {
                Producto producto = new Producto()
                {
                    Id = row["Id"].ToString(),
                    Descripciones = row["Descripciones"].ToString(),
                    Costo = Convert.ToDouble(row["Costo"]),
                    PrecioVenta = Convert.ToDouble(row["PrecioVenta"]),
                    Stock = Convert.ToInt32(row["Stock"]),
                    IdUsuario = Convert.ToInt32(row["IdUsuario"])
                };
                productos.Add(producto);
            }
            return productos;
        }
   
        public Producto GetProductoPorId(int id)
        {
            string script = "SELECT id, Descripciones, Costo, PrecioVenta, Stock,IdUsuario  FROM Producto WHERE id = @id ";
            helper.AddParametro("@id", id, DB_Helper.TipoDato.Int);
            DataTable dataTable = helper.GetDataTable(script);

            if (dataTable.Rows.Count == 0)
            {
                // Si no se encuentra ningún producto con el ID dado
                return null;
            }

            // Si solo se encuentra un producto con el ID dado, creamos y devolvemos el objeto Producto correspondiente.
            DataRow row = dataTable.Rows[0];
            Producto producto = new Producto
            {
                Id = row["Id"].ToString(),
                Descripciones = row["Descripciones"].ToString(),
                Costo = Convert.ToDouble(row["Costo"]),
                PrecioVenta = Convert.ToDouble(row["PrecioVenta"]),
                Stock = Convert.ToInt32(row["Stock"]),
                IdUsuario = Convert.ToInt32(row["IdUsuario"])
            };

            return producto;
        }

        public Producto CrearProducto(Producto NuevoProducto)
        {
            string script = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                "VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario)";
            helper.AddParametro("@Descripciones", NuevoProducto.Descripciones, DB_Helper.TipoDato.String);
            helper.AddParametro("@Costo", NuevoProducto.Costo, DB_Helper.TipoDato.Money);
            helper.AddParametro("@PrecioVenta", NuevoProducto.PrecioVenta, DB_Helper.TipoDato.Money);
            helper.AddParametro("@Stock", NuevoProducto.Stock, DB_Helper.TipoDato.Int);
            helper.AddParametro("@IdUsuario", NuevoProducto.IdUsuario, DB_Helper.TipoDato.Int);

            int rowsAffected = helper.ExecuteNonQuery(script);

            if (rowsAffected == 1)
            {
                return NuevoProducto;
            }
            else
            {
                return null;
            }
        }

        internal bool ModificarProducto(Producto productoModificado)
        {
            string script = "UPDATE Producto SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock, " +
                "IdUsuario = @IdUsuario  WHERE Id = @ID";

            // Agregar los parámetros correspondientes al script.
            helper.AddParametro("@Descripciones", productoModificado.Descripciones, DB_Helper.TipoDato.String);
            helper.AddParametro("@Costo", productoModificado.Costo, DB_Helper.TipoDato.Money);
            helper.AddParametro("@PrecioVenta", productoModificado.PrecioVenta, DB_Helper.TipoDato.Money);
            helper.AddParametro("@Stock", productoModificado.Stock, DB_Helper.TipoDato.Int);
            helper.AddParametro("@IdUsuario", productoModificado.IdUsuario, DB_Helper.TipoDato.Int);
            helper.AddParametro("@Id", productoModificado.Id, DB_Helper.TipoDato.Int);

            // Ejecutar el script y obtener el número de filas afectadas por la actualización.
            int filasAfectadas = helper.ExecuteNonQuery(script);

            // Retornar true si se actualizó al menos una fila, o false en caso contrario.
            return filasAfectadas > 0;
        }

        internal bool EliminarProductoPorId(int id)
        {
            string script = "DELETE PRODUCTO WHERE Id = @ID";

            // Agregar los parámetros correspondientes al script.
            helper.AddParametro("@ID", id, DB_Helper.TipoDato.Int);

            // Ejecutar el script y obtener el número de filas afectadas por la actualización.
            int filasAfectadas = helper.ExecuteNonQuery(script);

            // Retornar true si se actualizó al menos una fila, o false en caso contrario.
            return filasAfectadas > 0;
        }
    }

}