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
    public class ServiciosController : Controller
    {
        private readonly PetServiceBContext _context;

        public ServiciosController(PetServiceBContext context)
        {
            _context = context;
        }
        // GET: CitasController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicios.ToListAsync());
        }

        public IActionResult Create()
        {

            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo, Servicios servicios)
        {
            if (ModelState.IsValid)
            {
                //mascotas.FotoMascota = SubirImagen("images", archivo);
                _context.Add(servicios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        
            return View(servicios);
        }
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicios.FindAsync(id);
            if (servicios == null)
            {
                return NotFound();
            }
            return View(servicios);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicios servicios)
        {
            if (id != servicios.IdServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiciosExists(servicios.IdServicio))
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
            return View(servicios);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicios
                .FirstOrDefaultAsync(m => m.IdServicio == id);
            if (servicios == null)
            {
                return NotFound();
            }

            return View(servicios);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicios = await _context.Servicios.FindAsync(id);
            _context.Servicios.Remove(servicios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiciosExists(int id)
        {
            return _context.Servicios.Any(e => e.IdServicio == id);
        }
    }
}
