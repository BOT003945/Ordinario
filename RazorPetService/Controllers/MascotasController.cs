using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using System.IO;

namespace RazorPetService.Controllers
{
    public class MascotasController : Controller
    {
        private readonly PetServiceBContext _context;

        public MascotasController(PetServiceBContext context)
        {
            _context = context;
        }
        // GET: CitasController
        public async Task<IActionResult> Index()
        {
            var petServiceBContext = _context.Mascotas.Include(p => p.IdUsuarioNavigation);
            return View(await petServiceBContext.ToListAsync());
        }

        public IActionResult Create()
        {
            
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo,  Mascotas mascotas)
        {
            if (ModelState.IsValid)
            {
                mascotas.FotoMascota = SubirImagen("images", archivo);
                _context.Add(mascotas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", mascotas.IdUsuario);
            return View(mascotas);
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

            var mascotas = await _context.Mascotas.FindAsync(id);
            if (mascotas == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", mascotas.IdUsuario);
            return View(mascotas);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile archivo, Mascotas mascotas)
        {
            if (id != mascotas.IdMascota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    mascotas.FotoMascota = SubirImagen("images", archivo);
                    _context.Update(mascotas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotasExists(mascotas.IdMascota))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", mascotas.IdUsuario);
            return View(mascotas);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotas = await _context.Mascotas
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascotas == null)
            {
                return NotFound();
            }

            return View(mascotas);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mascotas = await _context.Mascotas.FindAsync(id);
            _context.Mascotas.Remove(mascotas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotasExists(int id)
        {
            return _context.Mascotas.Any(e => e.IdMascota == id);
        }
    }
}
