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
    public class TransferenciasController : Controller
    {
        private readonly ELCAPITALContext _context;

        public TransferenciasController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: Transferencias
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var eLCAPITALContext = _context.transferencias.Include(t => t.Cliente);
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: Transferencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.transferencias == null)
            {
                return NotFound();
            }

            var transferencia = await _context.transferencias
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(m => m.IdTransferencia == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // GET: Transferencias/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "TipoCliente");
            return View();
        }
        public IActionResult CreateCVU()
        {
            return View();
        }
        public  async Task <IActionResult> RealizarTransferencia(TarjetaDeCredito tarjetaDeCredito)
        {
            int cvu = tarjetaDeCredito.CVU;
            var cliente = await _context.Cliente.Include(c =>c.Productos).FirstOrDefaultAsync(x=>x.Productos.Any(p=>p.IdProducto==tarjetaDeCredito.IdProducto));
            ViewData["cvu"] = cvu;
            return View(cliente);
        }

        public async Task <IActionResult> Pagar(decimal Monto , string cvu,int idRecibe)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var clienteTrans = await _context.Cliente.FirstOrDefaultAsync(x => x.IdCliente == int.Parse(idclaim.Value));
            var clienteReci = await _context.Cliente.FirstOrDefaultAsync(x => x.IdCliente == idRecibe);
            if (clienteTrans.DineroEnCuenta>=Monto)
            {
                Transferencia transferencia = new Transferencia();
                transferencia.MontoTransferido = Monto;
                transferencia.IdCliente = int.Parse(idclaim.Value);
                transferencia.CVUDestino = int.Parse(cvu);
                transferencia.FechaTrans = DateTime.Now;
                clienteTrans.DineroEnCuenta -= Monto;
                clienteReci.DineroEnCuenta += Monto;
                await _context.AddAsync(transferencia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Insuficiente");
            }
            
        }

        public IActionResult NoEncontrado()
        {
            return View();
        }


        // POST: Transferencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCVU([Bind ("CVU")] TarjetaDeCredito tarjetaDeCredito)
        {
            var resultado = await _context.TarjetaDeCreditos.FirstAsync(x => x.CVU == tarjetaDeCredito.CVU);
            if (resultado!=null)
            {
                return RedirectToAction("RealizarTransferencia", resultado);
            }
            else
            {
                return View("NoEncontrado");
            }
        }

        // GET: Transferencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.transferencias == null)
            {
                return NotFound();
            }

            var transferencia = await _context.transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "TipoCliente", transferencia.IdCliente);
            return View(transferencia);
        }

        // POST: Transferencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransferencia,MontoTransferido,MontoRecibido,IdCliente")] Transferencia transferencia)
        {
            if (id != transferencia.IdTransferencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferenciaExists(transferencia.IdTransferencia))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "TipoCliente", transferencia.IdCliente);
            return View(transferencia);
        }

        // GET: Transferencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.transferencias == null)
            {
                return NotFound();
            }

            var transferencia = await _context.transferencias
                .Include(t => t.Cliente)
                .FirstOrDefaultAsync(m => m.IdTransferencia == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // POST: Transferencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.transferencias == null)
            {
                return Problem("Entity set 'ELCAPITALContext.transferencias'  is null.");
            }
            var transferencia = await _context.transferencias.FindAsync(id);
            if (transferencia != null)
            {
                _context.transferencias.Remove(transferencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferenciaExists(int id)
        {
          return _context.transferencias.Any(e => e.IdTransferencia == id);
        }
    }
}
