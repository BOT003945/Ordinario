using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int IdRol { get; set; }

        [Display(Name = "Tipo del rol")]
        public string NombreRol { get; set; }

        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}

