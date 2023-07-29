using API_UsuariosCH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace API_UsuariosCH.Repository
{
    public class ADO_ProductoVendido
    {
        private readonly DB_Helper helper;

        public ADO_ProductoVendido()
        {
            helper = new DB_Helper();
        }
        public IEnumerable<ProductoVendido> getProductoVendidos()
        {
            string script = "SELECT * FROM ProductoVendido";
            DataTable dataTable = helper.GetDataTable(script);

            List<ProductoVendido> ProductoVendidos = new List<ProductoVendido>();
            foreach (DataRow row in dataTable.Rows)
            {
                ProductoVendido ProductoVendido = new ProductoVendido()
                {
                    Id = row["Id"].ToString(),
                    IdProducto = Convert.ToInt32(row["IdProducto"]),
                    IdVenta = row["IdVenta"].ToString(),
                    Stock = Convert.ToInt32(row["Stock"])
                };
                ProductoVendidos.Add(ProductoVendido);
            }
            return ProductoVendidos;
        }

        public ProductoVendido GetProductoVendidoPorId(int id)
        {
            string script = "SELECT Id, IdProducto, IdVenta, Stock FROM ProductoVendido WHERE id = @id ";
            helper.AddParametro("@id", id, DB_Helper.TipoDato.Int);
            DataTable dataTable = helper.GetDataTable(script);

            if (dataTable.Rows.Count == 0)
            {
                // Si no se encuentra ningún ProductoVendido con el ID dado
                return null;
            }

            // Si solo se encuentra un ProductoVendido con el ID dado, creamos y devolvemos el objeto ProductoVendido correspondiente.
            DataRow row = dataTable.Rows[0];
            ProductoVendido ProductoVendido = new ProductoVendido
            {
                Id = row["Id"].ToString(),
                IdProducto = Convert.ToInt32(row["IdProducto"]),
                IdVenta = row["IdVenta"].ToString(),
                Stock = Convert.ToInt32(row["Stock"])
            };

            return ProductoVendido;
        }
    }
}