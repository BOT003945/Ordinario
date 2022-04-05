using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Mascotas
    {
        public Mascotas()
        {
            Cita = new HashSet<Citas>();
        }

        public int IdMascota { get; set; }
              
        [Display(Name = "Propietario (Yo)")]
        public int IdUsuario { get; set; }
        
        [Required]
        public string Nombre { get; set; }
        
        public decimal? Estatura { get; set; }
        
        public decimal? Peso { get; set; }

        [Required]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Foto de la mascota")]
        public string FotoMascota { get; set; }


        [Display(Name = "Nombre Usuario")]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual ICollection<Citas> Cita { get; set; }
    }
}
