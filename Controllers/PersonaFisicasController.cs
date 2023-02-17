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
    public class PersonaFisicasController : Controller
    {
        private readonly ELCAPITALContext _context;

        public PersonaFisicasController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: PersonaFisicas
        public async Task<IActionResult> Index()
        {
              return View(await _context.PersonaFisica.ToListAsync());
        }

        // GET: PersonaFisicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonaFisica == null)
            {
                return NotFound();
            }

            var personaFisica = await _context.PersonaFisica
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (personaFisica == null)
            {
                return NotFound();
            }

            return View(personaFisica);
        }

        // GET: PersonaFisicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonaFisicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,IdCliente,TipoDocumento,NumeroDocumento")] PersonaFisica personaFisica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personaFisica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Acceso");
            }
            return View(personaFisica);
        }

        // GET: PersonaFisicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonaFisica == null)
            {
                return NotFound();
            }

            var personaFisica = await _context.PersonaFisica.FindAsync(id);
            if (personaFisica == null)
            {
                return NotFound();
            }
            return View(personaFisica);
        }

        // POST: PersonaFisicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,IdCliente,TipoDocumento,NumeroDocumento")] PersonaFisica personaFisica)
        {
            if (id != personaFisica.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personaFisica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaFisicaExists(personaFisica.IdCliente))
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
            return View(personaFisica);
        }

        // GET: PersonaFisicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonaFisica == null)
            {
                return NotFound();
            }

            var personaFisica = await _context.PersonaFisica
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (personaFisica == null)
            {
                return NotFound();
            }

            return View(personaFisica);
        }

        // POST: PersonaFisicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonaFisica == null)
            {
                return Problem("Entity set 'ELCAPITALContext.PersonaFisica'  is null.");
            }
            var personaFisica = await _context.PersonaFisica.FindAsync(id);
            if (personaFisica != null)
            {
                _context.PersonaFisica.Remove(personaFisica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaFisicaExists(int id)
        {
          return _context.PersonaFisica.Any(e => e.IdCliente == id);
        }
    }
}
