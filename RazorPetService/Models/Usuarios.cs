using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Cita = new HashSet<Citas>();
            Mascota = new HashSet<Mascotas>();
        }


        
        public int IdUsuario { get; set; }
        
        [Required]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoP { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoM { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        
        [Required]
        public string Sexo { get; set; }

        [Required]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string Contra { get; set; }

        
        [Display(Name = "Foto de perfil")]
        public string FotoPerfil { get; set; }
        public int IdRol { get; set; }

        public virtual Roles IdRolNavigation { get; set; }
        public virtual ICollection<Citas> Cita { get; set; }
        public virtual ICollection<Mascotas> Mascota { get; set; }


        //[Url]
        //public string Website { get; set; }

    }
}
