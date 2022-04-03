using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPetService.Models
{
    public class Categorias
    {
        public Categorias()
        {
            Productos = new HashSet<Productos>();
        }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Productos> Productos { get; set; }
    }
}
