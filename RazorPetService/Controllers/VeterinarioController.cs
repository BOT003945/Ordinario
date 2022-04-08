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
    public class VeterinarioController : Controller
    {
        private readonly PetServiceBContext _context;

        public VeterinarioController(PetServiceBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veterinarios.ToListAsync());
        }
        public async Task<IActionResult> Catalogo()
        {
            return View(await _context.Veterinarios.ToListAsync());
        }


        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var veter = await _context.Veterinarios.FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veter == null)
            {
                return NotFound();
            }
            return View(veter);
        }

        public IActionResult Create()
        {

            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo, Veterinarios veterinarios)
        {
            if (ModelState.IsValid)
            {
                veterinarios.FotoV = SubirImagen("veterinarios", archivo);
                _context.Add(veterinarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(veterinarios);
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

            var veterinarios = await _context.Veterinarios.FindAsync(id);
            if (veterinarios == null)
            {
                return NotFound();
            }
            return View(veterinarios);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile archivo, Veterinarios veterinarios)
        {
            if (id != veterinarios.IdVeterinario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    veterinarios.FotoV = SubirImagen("images", archivo);
                    _context.Update(veterinarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariosExists(veterinarios.IdVeterinario))
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
            return View(veterinarios);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios
                .FirstOrDefaultAsync(m => m.IdVeterinario == id);
            if (veterinarios == null)
            {
                return NotFound();
            }

            return View(veterinarios);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarios = await _context.Veterinarios.FindAsync(id);
            _context.Veterinarios.Remove(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariosExists(int id)
        {
            return _context.Veterinarios.Any(e => e.IdVeterinario == id);
        }
    }
}
