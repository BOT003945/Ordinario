using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Productos
    {
        public Productos()
        {
            VentaDetalles = new HashSet<VentaDetalles>();
        }

        public int IdProducto { get; set; }

        [Display(Name="Nombre del producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "Cantidad Almacenada")]
        public int? CantidadAlmacenada { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }
        public int IdCategoria { get; set; }

        [Required]
        [Display(Name = "Foto del Producto")]
        public string FotoProducto { get; set; }

        public virtual Categorias IdCategoriaNavigation { get; set; }
        public virtual ICollection<VentaDetalles> VentaDetalles { get; set; }
    }
}
