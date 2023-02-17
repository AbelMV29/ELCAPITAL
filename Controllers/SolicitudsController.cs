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
    public class SolicitudsController : Controller
    {
        private readonly ELCAPITALContext _context;

        public SolicitudsController(ELCAPITALContext context)
        {
            _context = context;
        }

        // GET: Solicituds
        public async Task<IActionResult> Index()
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var eLCAPITALContext = _context.Solicitudes.Include(s => s.Cliente).Where(b=>b.IdCliente== int.Parse(idclaim.Value));
            return View(await eLCAPITALContext.ToListAsync());
        }

        // GET: Solicituds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Solicitudes == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitudes
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Solicituds/Create
        public IActionResult CreateSolicitudPaquete()
        {
            
            return View();
        }
        public IActionResult CreateSolicitudPrestamo()
        {

            return View();
        }

        // POST: Solicituds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSolicitudPaquete([Bind("IdSolicitud,TipoSolicitud,FechaSolicitud,IdCliente")] Solicitud solicitud,
           [Bind("EsCrediticio,IdProducto")] Paquete paquete)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            solicitud.IdCliente = int.Parse(idclaim.Value);
            paquete.IdCliente = int.Parse(idclaim.Value);
            solicitud.TipoSolicitud = "Paquete";
            solicitud.FechaSolicitud = DateTime.Now;
                _context.Add(solicitud);
            _context.Add(paquete);
            await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSolicitudPrestamo([Bind("IdSolicitud,TipoSolicitud,FechaSolicitud,IdCliente")] 
        Solicitud solicitud, [Bind("EsPrendario,IdProducto")] Prestamo prestamo)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            solicitud.IdCliente = int.Parse(idclaim.Value);
            prestamo.IdCliente = int.Parse(idclaim.Value);
            solicitud.TipoSolicitud = "Prestamo";
            solicitud.FechaSolicitud = DateTime.Now;
            _context.Add(solicitud);
            _context.Add(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(int id)
        {
          return _context.Solicitudes.Any(e => e.IdSolicitud == id);
        }
    }
}
