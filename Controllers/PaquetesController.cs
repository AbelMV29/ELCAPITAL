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
    public class PaquetesController : Controller
    {
        private readonly ELCAPITALContext _context;

        public PaquetesController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");

            var eLCAPITALContext = _context.Paquete.Include(p => p.Cliente).Where(b=>b.IdCliente==int.Parse(idclaim.Value));
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paquete == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquete
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: Paquetes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "TipoCliente");
            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EsCrediticio,IdProducto")] Paquete paquete)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            paquete.IdCliente = int.Parse(idclaim.Value);
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        } 

        private bool PaqueteExists(int id)
        {
          return _context.Paquete.Any(e => e.IdProducto == id);
        }
    }
}
