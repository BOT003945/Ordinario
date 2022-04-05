using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Servicios
    {
        public Servicios()
        {
            Cita = new HashSet<Citas>();
        }

        public int IdServicio { get; set; }

        [Display(Name = "Servicio")]
        public string NombreServicio { get; set; }

        public virtual ICollection<Citas> Cita { get; set; }
    }
}
