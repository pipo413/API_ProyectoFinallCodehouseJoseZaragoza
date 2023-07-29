using System.ComponentModel.DataAnnotations;

namespace API_UsuariosCH.Models
{

    public class ProductoVendido
    {
        public string Id { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        public string IdVenta { get; set; }

        public ProductoVendido()
        {
            Id = string.Empty;
            IdProducto = 0;
            Stock = 0;
            IdVenta = string.Empty;
        }
    }


}


