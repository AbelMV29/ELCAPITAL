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
    public class TarjetaDeCreditoesController : Controller
    {
        private readonly ELCAPITALContext _context;

        public TarjetaDeCreditoesController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: TarjetaDeCreditoes
        public async Task<IActionResult> Index()
        {
            var eLCAPITALContext = _context.TarjetaDeCreditos.Include(t => t.Paquete);
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: TarjetaDeCreditoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TarjetaDeCreditos == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.TarjetaDeCreditos
                .Include(t => t.Paquete)
                .FirstOrDefaultAsync(m => m.IdTarjetaDeCredito == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // GET: TarjetaDeCreditoes/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Paquete, "IdProducto", "CualProducto");
            return View();
        }

        // POST: TarjetaDeCreditoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarjetaDeCredito,CodigoTarjeta,IdProducto")] TarjetaDeCredito tarjetaDeCredito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarjetaDeCredito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Paquete, "IdProducto", "CualProducto", tarjetaDeCredito.IdProducto);
            return View(tarjetaDeCredito);
        }

        // GET: TarjetaDeCreditoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TarjetaDeCreditos == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.TarjetaDeCreditos.FindAsync(id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Paquete, "IdProducto", "CualProducto", tarjetaDeCredito.IdProducto);
            return View(tarjetaDeCredito);
        }

        // POST: TarjetaDeCreditoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarjetaDeCredito,CodigoTarjeta,IdProducto")] TarjetaDeCredito tarjetaDeCredito)
        {
            if (id != tarjetaDeCredito.IdTarjetaDeCredito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjetaDeCredito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaDeCreditoExists(tarjetaDeCredito.IdTarjetaDeCredito))
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
            ViewData["IdProducto"] = new SelectList(_context.Paquete, "IdProducto", "CualProducto", tarjetaDeCredito.IdProducto);
            return View(tarjetaDeCredito);
        }

        // GET: TarjetaDeCreditoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TarjetaDeCreditos == null)
            {
                return NotFound();
            }

            var tarjetaDeCredito = await _context.TarjetaDeCreditos
                .Include(t => t.Paquete)
                .FirstOrDefaultAsync(m => m.IdTarjetaDeCredito == id);
            if (tarjetaDeCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaDeCredito);
        }

        // POST: TarjetaDeCreditoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TarjetaDeCreditos == null)
            {
                return Problem("Entity set 'ELCAPITALContext.TarjetaDeCreditos'  is null.");
            }
            var tarjetaDeCredito = await _context.TarjetaDeCreditos.FindAsync(id);
            if (tarjetaDeCredito != null)
            {
                _context.TarjetaDeCreditos.Remove(tarjetaDeCredito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaDeCreditoExists(int id)
        {
          return _context.TarjetaDeCreditos.Any(e => e.IdTarjetaDeCredito == id);
        }
    }
}
