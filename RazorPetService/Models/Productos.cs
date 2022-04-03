using System;

namespace RazorPetService.Models
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CantidadAlmacenada { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdCategoria { get; set; }


        public virtual Categorias IdCategoriaNavigation { get; set; }
    }
}
