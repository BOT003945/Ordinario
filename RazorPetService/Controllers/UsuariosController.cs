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
    public class UsuariosController : Controller
    {
        private readonly PetServiceBContext _context;

        public UsuariosController(PetServiceBContext context)
        {
            _context = context;
        }
        // GET: Registrosalumnoes
        public async Task<IActionResult> Index()
        {
            var petServiceBContext = _context.Usuarios.Include(r => r.IdRolNavigation);
            return View(await petServiceBContext.ToListAsync());
        }

        public async Task<IActionResult> Detalle(int? id)
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

        public IActionResult Create()
        {

            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "NombreRol");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile archivo, Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                usuarios.FotoPerfil = SubirImagen("perfiles", archivo);
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "NombreRol", usuarios.IdRol);
            return View(usuarios);
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

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "NombreRol", usuarios.IdRol);
            return View(usuarios);
        }
                   
// POST: Registrosalumnoes/Edit/5
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile archivo, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuarios.FotoPerfil = SubirImagen("images", archivo);
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.IdUsuario))
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
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "NombreRol", usuarios.IdRol);
            return View(usuarios);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(r => r.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }




        // POST: Registrosalumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }

        //public ActionResult CreateUsuario(string usuarios)
        //{
        //    var arregloGeneros = usuarios.Split('/');
        //    return View();
        //}
    }
}
