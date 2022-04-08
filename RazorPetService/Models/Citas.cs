using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Citas
    {
        public int IdCita { get; set; }

        [Display(Name = "Servicio:")]
        public int IdServicio { get; set; }

        [Display(Name = "Veterinario:")]
        public int? IdVeterinario { get; set; }

        [Display(Name = "Usuario:")]
        public int IdUsuario { get; set; }

        [Display(Name = "Tu Mascota:")]
        public int IdMascota { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Descripción:")]
        public string Descripcion { get; set; }

        public string Estatus { get; set; }

        [Display(Name = "Mascota:")]
        public virtual Mascotas IdMascotaNavigation { get; set; }

        [Display(Name = "Servicio:")]
        public virtual Servicios IdServicioNavigation { get; set; }

        [Display(Name = "Usuario:")]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [Display(Name = "Veterinario:")]
        public virtual Veterinarios IdVeterinarioNavigation { get; set; }
    }
}
