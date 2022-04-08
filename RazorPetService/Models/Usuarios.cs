using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable disable

namespace RazorPetService.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Cita = new HashSet<Citas>();
            Mascota = new List<Mascotas>();
            Venta = new HashSet<Ventas>();
        }
        public List<Usuarios> Listar()
        {
            var usuarios = new List<Usuarios>();
            try
            {
                using (var context = new PetServiceBContext())
                {
                    usuarios = context.Usuarios.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return usuarios;
        }

        //public Mascotas Obtener(int id)
        //{
        //    var usuarios = new Mascotas();
        //    try
        //    {
        //        using (var context = new PetServiceBContext())
        //        {
        //            usuarios = context.Mascotas
        //                            .Include("Mascotas")
        //                            .Where(x => x.id == id)
        //                            .Single();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //    return usuarios;
        //}
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

        [Display(Name = "Tipo de rol")]
        public int IdRol { get; set; }


        [Display(Name = "Tipo de rol")]
        public virtual Roles IdRolNavigation { get; set; }
        public virtual ICollection<Citas> Cita { get; set; }
        public virtual ICollection<Mascotas> Mascota { get; set; }
        public virtual ICollection<Ventas> Venta { get; set; }
    }
}
