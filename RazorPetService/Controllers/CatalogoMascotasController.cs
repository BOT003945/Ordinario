using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using System.Threading.Tasks;

namespace RazorPetService.Controllers
{
    public class CatalogoMascotasController : Controller
    {
        private readonly PetServiceBContext _context;

        public CatalogoMascotasController(PetServiceBContext contexto)
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
            var mascotas = await _context.Mascotas.FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascotas == null)
            {
                return NotFound();
            }
            return View(mascotas);
        }
    }
}
