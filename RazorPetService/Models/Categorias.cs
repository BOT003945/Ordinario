using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Productos = new HashSet<Productos>();
        }

        public int IdCategoria { get; set; }

        [Required]
        [Display(Name= "Categoria")]
        public string NombreCategoria { get; set; }

        public virtual ICollection<Productos> Productos { get; set; }
    }
}