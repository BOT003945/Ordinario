using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Veterinarios
    {
        public Veterinarios()
        {
            Cita = new HashSet<Citas>();
        }

        public int IdVeterinario { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        [Display(Name="Apellido paterno")]
        public string ApellidoP { get; set; }

        [Display(Name = "Apellido materno")]
        public string ApellidoM { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }


        [Required]
        [Display(Name = "Cita")]
        public virtual ICollection<Citas> Cita { get; set; }


        [Display(Name = "Foto de perfil")]
        public string FotoV { get; set; }   
    }
}
