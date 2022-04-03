using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPetService.Controllers
{
    public class ProductosController : Controller
    {
        private readonly PetServiceBContext _context;

        public ProductosController(PetServiceBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var petServiceBContext = _context.Productos.Include(p => p.IdCategoriaNavigation);
            return View(await petServiceBContext.ToListAsync());
        }

        
        //Crear registro
        public IActionResult Create()
        {

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo, Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.FotoProducto = SubirImagen("productos", archivo);
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria", productos.IdCategoria);
            return View(productos);
        }
        private string SubirImagen(string RutaCarpeta, IFormFile ArchivoSubir)
        {
            //condigo para que se guarde la imagen
            string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", RutaCarpeta);
            string NombreArchivo = Guid.NewGuid().ToString() + "_" + ArchivoSubir.FileName;
            //union de las carpetas
            string RutaArchivoUnico = Path.Combine(carpeta, NombreArchivo);
            //adjuntar la imagen en la carpeta
            using (var InfoArchivo = new FileStream(RutaArchivoUnico, FileMode.Create)) ArchivoSubir.CopyTo(InfoArchivo);
            return NombreArchivo;


        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria", productos.IdCategoria);
            return View(productos);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile archivo, Productos productos)
        {
            if (id != productos.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productos.FotoProducto = SubirImagen("productos", archivo);
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria", productos.IdCategoria);
            return View(productos);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .Include(r => r.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.IdCategoria == id);
        }
    }
}
