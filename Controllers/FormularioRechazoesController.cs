using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELCAPITAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace ELCAPITAL.Controllers
{
    [Authorize]
    public class FormularioRechazoesController : Controller
    {
        private readonly ELCAPITALContext _context;

        public FormularioRechazoesController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: FormularioRechazoes
        public async Task<IActionResult> Index()
        {
            var eLCAPITALContext = _context.FormularioRechazos.Include(f => f.Solicitud);
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: FormularioRechazoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormularioRechazos == null)
            {
                return NotFound();
            }

            var formularioRechazo = await _context.FormularioRechazos
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.IdFormularioRechazo == id);
            if (formularioRechazo == null)
            {
                return NotFound();
            }

            return View(formularioRechazo);
        }

        // GET: FormularioRechazoes/Create
        public IActionResult Create()
        {
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud");
            return View();
        }

        // POST: FormularioRechazoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormularioRechazo,Motivo,FechaRechazo,IdSolicitud")] FormularioRechazo formularioRechazo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formularioRechazo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRechazo.IdSolicitud);
            return View(formularioRechazo);
        }

        // GET: FormularioRechazoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormularioRechazos == null)
            {
                return NotFound();
            }

            var formularioRechazo = await _context.FormularioRechazos.FindAsync(id);
            if (formularioRechazo == null)
            {
                return NotFound();
            }
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRechazo.IdSolicitud);
            return View(formularioRechazo);
        }

        // POST: FormularioRechazoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormularioRechazo,Motivo,FechaRechazo,IdSolicitud")] FormularioRechazo formularioRechazo)
        {
            if (id != formularioRechazo.IdFormularioRechazo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formularioRechazo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioRechazoExists(formularioRechazo.IdFormularioRechazo))
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
            ViewData["IdSolicitud"] = new SelectList(_context.Solicitudes, "IdSolicitud", "TipoSolicitud", formularioRechazo.IdSolicitud);
            return View(formularioRechazo);
        }

        // GET: FormularioRechazoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormularioRechazos == null)
            {
                return NotFound();
            }

            var formularioRechazo = await _context.FormularioRechazos
                .Include(f => f.Solicitud)
                .FirstOrDefaultAsync(m => m.IdFormularioRechazo == id);
            if (formularioRechazo == null)
            {
                return NotFound();
            }

            return View(formularioRechazo);
        }

        // POST: FormularioRechazoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormularioRechazos == null)
            {
                return Problem("Entity set 'ELCAPITALContext.FormularioRechazos'  is null.");
            }
            var formularioRechazo = await _context.FormularioRechazos.FindAsync(id);
            if (formularioRechazo != null)
            {
                _context.FormularioRechazos.Remove(formularioRechazo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioRechazoExists(int id)
        {
          return _context.FormularioRechazos.Any(e => e.IdFormularioRechazo == id);
        }
    }
}
