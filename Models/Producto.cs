using System.ComponentModel.DataAnnotations;

namespace API_UsuariosCH.Models
{
    public class Producto
    {
        public string Id { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto()
        {
            Id = string.Empty;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }

        public Producto(string id)
        {
            Id = id;
            Descripciones = string.Empty;
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }

        public Producto(string id, string descripciones, double costo, double precioVenta, int stock, int idUsuario)
        {
            Id = id;
            Descripciones = descripciones;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
        }
    }


}


