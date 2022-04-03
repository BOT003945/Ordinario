using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Controllers;
using RazorPetService.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace RazorPetService.Controllers
{
    public class SesionController : Controller
    {
        private readonly PetServiceBContext _context;

        public SesionController(PetServiceBContext contexto)
        {
            _context = contexto;
        }

        public Usuarios Cuenta { get; set; }
       

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Onpost(string Correo, string Contra)
        {
            //comprobar que la cuenta exista
            Cuenta = _context.Usuarios.Where(p => p.Correo == Correo && p.Contra == Contra).FirstOrDefault<Usuarios>();
            //comprobar si existe
            if (Cuenta != null)
            {
                //se crea la sesion y se le asigna un nombre
                HttpContext.Session.SetString("Sesion1", Cuenta.Correo);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AcercaDe", "Home");

        }


        [HttpPost]
        public ActionResult OnpostAdmin(string Correo, string Contra)
        {
            //comprobar que la cuenta exista
            Cuenta = _context.Usuarios.Where(p => p.Correo == Correo && p.Contra == Contra).FirstOrDefault<Usuarios>();
            //comprobar si existe
            if (Cuenta != null)
            {
                //se crea la sesion y se le asigna un nombre
                HttpContext.Session.SetString("Sesion0", Cuenta.Correo);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AcercaDe", "Home");

        }
        //public bool consulta(string Cor, string Con)
        //{
        //    var Cuenta = _context.Usuarios.Where(p => p.Correo == Cor && p.Contra == Con).FirstOrDefault<Usuarios>();

        //    if (Cuenta != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public ActionResult LogOut()
        {
            HttpContext.Session.Remove("Sesion1");
            return RedirectToAction("Login", "Sesion");
        }

        public ActionResult LogOutAdmin()
        {
            HttpContext.Session.Remove("Sesion0");
            return RedirectToAction("LoginAdmin", "Sesion");
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
