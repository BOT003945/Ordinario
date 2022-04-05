using System;
using System.Collections.Generic;

#nullable disable

namespace RazorPetService.Models
{
    public partial class VentaDetalles
    {
        public int IdVentaDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        public virtual Productos IdProductoNavigation { get; set; }
        public virtual Ventas IdVentaNavigation { get; set; }
    }
}
