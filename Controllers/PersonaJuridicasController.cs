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
    public class PersonaJuridicasController : Controller
    {
        private readonly ELCAPITALContext _context;

        public PersonaJuridicasController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: PersonaJuridicas
        public async Task<IActionResult> Index()
        {
              return View(await _context.PersonaJuridica.ToListAsync());
        }

        // GET: PersonaJuridicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonaJuridica == null)
            {
                return NotFound();
            }

            var personaJuridica = await _context.PersonaJuridica
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (personaJuridica == null)
            {
                return NotFound();
            }

            return View(personaJuridica);
        }

        // GET: PersonaJuridicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonaJuridicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreEmpresa,IdCliente,TipoDocumento,NumeroDocumento,Ingresos")] PersonaJuridica personaJuridica)
        {
            personaJuridica.DineroEnCuenta = 0;
            _context.Add(personaJuridica);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Acceso");
        }
        // GET: PersonaJuridicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonaJuridica == null)
            {
                return NotFound();
            }

            var personaJuridica = await _context.PersonaJuridica.FindAsync(id);
            if (personaJuridica == null)
            {
                return NotFound();
            }
            return View(personaJuridica);
        }

        // POST: PersonaJuridicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NombreEmpresa,IdCliente,TipoDocumento,NumeroDocumento")] PersonaJuridica personaJuridica)
        {
            if (id != personaJuridica.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personaJuridica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaJuridicaExists(personaJuridica.IdCliente))
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
            return View(personaJuridica);
        }

        // GET: PersonaJuridicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonaJuridica == null)
            {
                return NotFound();
            }

            var personaJuridica = await _context.PersonaJuridica
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (personaJuridica == null)
            {
                return NotFound();
            }

            return View(personaJuridica);
        }

        // POST: PersonaJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonaJuridica == null)
            {
                return Problem("Entity set 'ELCAPITALContext.PersonaJuridica'  is null.");
            }
            var personaJuridica = await _context.PersonaJuridica.FindAsync(id);
            if (personaJuridica != null)
            {
                _context.PersonaJuridica.Remove(personaJuridica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaJuridicaExists(int id)
        {
          return _context.PersonaJuridica.Any(e => e.IdCliente == id);
        }
    }
}
