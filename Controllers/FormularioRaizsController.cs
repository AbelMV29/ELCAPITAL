using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELCAPITAL.Models;

namespace ELCAPITAL.Controllers
{
    public class FormularioRaizsController : Controller
    {
        private readonly ELCAPITALContext _context;

        public FormularioRaizsController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: FormularioRaizs
        public async Task<IActionResult> Index()
        {
            var eLCAPITALContext = _context.FormularioRaizs.Include(f => f.Solicitud);
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: FormularioRaizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormularioRaizs == null)
            {
                return NotFound();
            }

            var formularioRaiz = await _context.FormularioRaizs
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.IdFormularioRaiz == id);
            if (formularioRaiz == null)
            {
                return NotFound();
            }

            return View(formularioRaiz);
        }

        // GET: FormularioRaizs/Create
        public IActionResult Create()
        {
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud");
            return View();
        }

        // POST: FormularioRaizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormularioRaiz,FechaAprobacion,IdSolicitud")] FormularioRaiz formularioRaiz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formularioRaiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRaiz.IdSolicitud);
            return View(formularioRaiz);
        }

        // GET: FormularioRaizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormularioRaizs == null)
            {
                return NotFound();
            }

            var formularioRaiz = await _context.FormularioRaizs.FindAsync(id);
            if (formularioRaiz == null)
            {
                return NotFound();
            }
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRaiz.IdSolicitud);
            return View(formularioRaiz);
        }

        // POST: FormularioRaizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormularioRaiz,FechaAprobacion,IdSolicitud")] FormularioRaiz formularioRaiz)
        {
            if (id != formularioRaiz.IdFormularioRaiz)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formularioRaiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioRaizExists(formularioRaiz.IdFormularioRaiz))
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
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRaiz.IdSolicitud);
            return View(formularioRaiz);
        }

        // GET: FormularioRaizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormularioRaizs == null)
            {
                return NotFound();
            }

            var formularioRaiz = await _context.FormularioRaizs
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.IdFormularioRaiz == id);
            if (formularioRaiz == null)
            {
                return NotFound();
            }

            return View(formularioRaiz);
        }

        // POST: FormularioRaizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormularioRaizs == null)
            {
                return Problem("Entity set 'ELCAPITALContext.FormularioRaizs'  is null.");
            }
            var formularioRaiz = await _context.FormularioRaizs.FindAsync(id);
            if (formularioRaiz != null)
            {
                _context.FormularioRaizs.Remove(formularioRaiz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioRaizExists(int id)
        {
          return _context.FormularioRaizs.Any(e => e.IdFormularioRaiz == id);
        }
    }
}
