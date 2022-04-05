using System;
using System.Collections.Generic;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Ventas
    {
        public Ventas()
        {
            VentaDetalles = new HashSet<VentaDetalles>();
        }

        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<VentaDetalles> VentaDetalles { get; set; }
    }
}
