using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using System.Threading.Tasks;

namespace RazorPetService.Controllers
{
    public class CatalogoProductosController : Controller
    {
        private readonly PetServiceBContext _context;

        public CatalogoProductosController(PetServiceBContext contexto)
        {
            _context = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        public async Task<IActionResult> Consulta_detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var casa = await _context.Productos.FirstOrDefaultAsync(m => m.IdProducto == id);
            if (casa == null)
            {
                return NotFound();
            }
            return View(casa);
        }
    }
}
