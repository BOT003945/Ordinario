using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPetService.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorPetService.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly PetServiceBContext _context;

        public CatalogoController(PetServiceBContext contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Mascotas.ToListAsync());
        }

        public async Task<IActionResult> Consulta_detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var casa = await _context.Mascotas.FirstOrDefaultAsync(m => m.IdMascota == id);
            if (casa == null)
            {
                return NotFound();
            }
            return View(casa);
        }
    }
}
