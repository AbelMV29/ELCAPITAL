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
           [Bind("EsCrediticio,IdProducto")] Paquete paquete,FormularioRaiz formularioRaiz,FormularioRechazo formularioRechazo)
        {
            Random r = new Random();
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            solicitud.IdCliente = int.Parse(idclaim.Value);
            solicitud.TipoSolicitud = "Paquete";
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EsAprobada = false;
            _context.Add(solicitud);
            await _context.SaveChangesAsync();
            var idsolicitud = _context.Solicitudes.OrderByDescending(a => a.IdSolicitud).FirstOrDefault();

            if (solicitud.EsAprobada)
            {
                paquete.IdCliente = int.Parse(idclaim.Value);
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                var idProducto = _context.Producto.OrderByDescending(p => p.IdProducto).FirstOrDefault();

                if (paquete.EsCrediticio)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        TarjetaDeCredito tarjetaDeCredito = new TarjetaDeCredito();
                        tarjetaDeCredito.IdProducto = idProducto.IdProducto;
                        tarjetaDeCredito.CodigoTarjeta = r.Next(1000000, 9999999);
                        _context.Add(tarjetaDeCredito);
                    }
                    formularioRaiz.IdSolicitud = idsolicitud.IdSolicitud;
                    formularioRaiz.FechaAprobacion = DateTime.Now;
                    _context.Add(formularioRaiz);
                }
            }
            else
            {
                formularioRechazo.Motivo = "No permitido";
                formularioRechazo.FechaRechazo = DateTime.Now;
                formularioRechazo.IdSolicitud = idsolicitud.IdSolicitud;
                _context.Add(formularioRechazo);
            }
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSolicitudPrestamo([Bind("IdSolicitud,TipoSolicitud,FechaSolicitud,IdCliente")] 
        Solicitud solicitud, [Bind("EsPrendario,IdProducto")] Prestamo prestamo,FormularioRaiz formularioRaiz,FormularioRechazo formularioRechazo)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            solicitud.IdCliente = int.Parse(idclaim.Value);
            solicitud.TipoSolicitud = "Prestamo";
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EsAprobada = true;
            _context.Add(solicitud);
            await _context.SaveChangesAsync();
            var idsolicitud = _context.Solicitudes.OrderByDescending(a => a.IdSolicitud).FirstOrDefault();
            if (solicitud.EsAprobada)
            {

                prestamo.IdCliente = int.Parse(idclaim.Value);
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                

                formularioRaiz.IdSolicitud = idsolicitud.IdSolicitud;
                formularioRaiz.FechaAprobacion = DateTime.Now;
                _context.Add(formularioRaiz);
            }
            else
            {
                formularioRechazo.Motivo = "No permitido";
                formularioRechazo.FechaRechazo = DateTime.Now;
                formularioRechazo.IdSolicitud = idsolicitud.IdSolicitud;
                _context.Add(formularioRechazo);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

        private bool SolicitudExists(int id)
        {
          return _context.Solicitudes.Any(e => e.IdSolicitud == id);
        }
    }
}
