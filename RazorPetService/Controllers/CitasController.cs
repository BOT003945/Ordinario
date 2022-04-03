using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPetService.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RazorPetService.Controllers
{
    public class CitasController : Controller
    {

        private readonly PetServiceBContext _context;

        public CitasController(PetServiceBContext context)
        {
            _context = context;
        }

        // GET: CitasController
        public async Task<IActionResult> Index()
        {
            var petServiceBContext = _context.Citas.Include(p => p.IdServicioNavigation).Include(p => p.IdUsuarioNavigation).Include(p=>p.IdMascotaNavigation);
            return View(await petServiceBContext.ToListAsync());
        }

        // GET: Proyectoes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var proyecto = await _context.Proyectos
        //        .Include(p => p.IddivisionesNavigation)
        //        .FirstOrDefaultAsync(m => m.Idproyectos == id);
        //    if (proyecto == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(proyecto);
        //}

        public IActionResult Create()
        {
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres");
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Citas citas)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(citas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", citas.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", citas.IdUsuario);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", citas.IdMascota);
           
            return View(citas);
        }


        // GET: Registrosalumnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", citas.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", citas.IdUsuario);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", citas.IdMascota);
            return View(citas);
        }

        // POST: Registrosalumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Citas citas)
        {
            if (id != citas.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //registrosalumno.Fotoalumno = SubirImagen("img", archivo);
                    _context.Update(citas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitasExists(citas.IdCita))
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
            ViewData["IdServicio"] = new SelectList(_context.Servicios, "IdServicio", "NombreServicio", citas.IdServicio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombres", citas.IdUsuario);
            ViewData["IdMascota"] = new SelectList(_context.Mascotas, "IdMascota", "Nombre", citas.IdMascota);
            return View(citas);
        }

        // GET: Registrosalumnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .Include(r => r.IdServicioNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .Include(r => r.IdMascotaNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citas = await _context.Citas.FindAsync(id);
            _context.Citas.Remove(citas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitasExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
