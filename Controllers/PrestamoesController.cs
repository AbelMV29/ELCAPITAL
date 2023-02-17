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
    public class PrestamoesController : Controller
    {
        private readonly ELCAPITALContext _context;

        public PrestamoesController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: Prestamoes
        public async Task<IActionResult> Index()
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");

            var eLCAPITALContext = _context.Prestamo.Include(p => p.Cliente).Where(b => b.IdCliente == int.Parse(idclaim.Value));
            return View(await eLCAPITALContext.ToListAsync());
        }


        // GET: Prestamoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "TipoCliente");
            return View();
        }

        // POST: Prestamoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EsPrendario,IdProducto")] Prestamo prestamo)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            prestamo.IdCliente = int.Parse(idclaim.Value);
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }


        private bool PrestamoExists(int id)
        {
          return _context.Prestamo.Any(e => e.IdProducto == id);
        }
    }
}
