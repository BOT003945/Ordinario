using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPetService.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly PetServiceBContext _context;

        public CategoriasController(PetServiceBContext context)
        {
            _context = context;
        }
        // GET: CitasController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        public IActionResult Create()
        {

            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo, Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                //mascotas.FotoMascota = SubirImagen("images", archivo);
                _context.Add(categorias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categorias);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }
            return View(categorias);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categorias categorias)
        {
            if (id != categorias.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasExists(categorias.IdCategoria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categorias);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorias = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categorias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}
