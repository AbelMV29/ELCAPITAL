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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestamo.ToListAsync());
        }
        public async Task<IActionResult> IndexCliente()
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var Prestamo = await _context.Prestamo.FirstOrDefaultAsync(b => b.IdCliente == int.Parse(idclaim.Value));
            return View(Prestamo);
        }
        public IActionResult PedirPrestamo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PedirPrestamoPost([Bind("DineroPrestamo")] Prestamo prestamo)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var user = await _context.Cliente.FirstOrDefaultAsync(z => z.IdCliente == int.Parse(idclaim.Value));
            var tieneUno = await _context.Prestamo.FirstOrDefaultAsync(x => x.IdCliente == int.Parse(idclaim.Value));
            if (tieneUno != null && tieneUno.DineroPrestamo == 0)
            {
                if (prestamo.DineroPrestamo < 300000)
                {

                    var prestamoDb = await _context.Prestamo.FirstOrDefaultAsync(a => a.IdCliente == int.Parse(idclaim.Value));
                    var sueldoBanco = await _context.ElCapitalFondos.FirstAsync(x => x.IdBancoUnico == 1);
                    if ((sueldoBanco.FondoMonetario - prestamoDb.DineroPrestamo) <= 0)
                    {
                        return View("SinFondos");
                    }
                    else
                    {
                        prestamoDb.DineroPrestamo = prestamo.DineroPrestamo;
                        user.DineroEnCuenta += prestamo.DineroPrestamo;
                        sueldoBanco.FondoMonetario -= prestamoDb.DineroPrestamo;
                        prestamoDb.FechaLimite = DateTime.Now.AddDays(5);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View("LimiteSobrepasado");
                }
            }
            else
            {
                return View("TienePrestamo");
            }
            
            
            
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
