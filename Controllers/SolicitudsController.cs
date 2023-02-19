using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ELCAPITAL.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Build.Framework;

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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.Solicitudes.ToListAsync());
        }

        // GET: Solicituds/Details/5
        [Authorize(Roles = "Admin")]
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
        public IActionResult Error(string codigo)
        {

            return View();
        }
        public IActionResult ErrorSolicutd()
        {

            return View();
        }
        [Authorize(Roles ="Admin")]
        public async Task <IActionResult> AceptarSolicitud(int id,FormularioRaiz formularioRaiz)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.IdCliente == solicitud.IdCliente);
            solicitud.Estado = "Aceptada";
            if (solicitud.TipoSolicitud=="Paquete")
            {
                Paquete paquete = new Paquete();
                paquete.IdCliente = cliente.IdCliente;
                _context.Add(paquete);

            }
            else if(solicitud.TipoSolicitud=="Prestamo")
            {
                Prestamo prestamo = new Prestamo();
                prestamo.IdCliente = cliente.IdCliente;
                _context.Add(prestamo);
                
            }
            formularioRaiz.IdSolicitud = solicitud.IdSolicitud;
            formularioRaiz.FechaAprobacion = DateTime.Now;
            _context.Add(formularioRaiz);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexAdmin");
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DenegarSolicitud(int id,FormularioRechazo formularioRechazo)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            solicitud.Estado = "Denegada";
            formularioRechazo.Motivo = "Motivos Personales ";
            formularioRechazo.FechaRechazo = DateTime.Now;
            formularioRechazo.IdSolicitud = solicitud.IdSolicitud;
            _context.Add(formularioRechazo);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexAdmin");
        }


        public async Task <IActionResult> ErrorSolicitud(Solicitud solicitud)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var solicitudVigente = await _context.Solicitudes.Where(x => x.TipoSolicitud == "Prestamo").Where(x => x.IdCliente == int.Parse(idclaim.Value)).FirstOrDefaultAsync();
            return View(solicitudVigente);
        }

        // POST: Solicituds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSolicitudPaquete([Bind("IdSolicitud,TipoSolicitud,FechaSolicitud,IdCliente")] Solicitud solicitud)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            var tieneSolicitudPaquete = _context.Solicitudes.Where(x => x.TipoSolicitud == "Paquete").Where(x => x.IdCliente == int.Parse(idclaim.Value)).FirstOrDefault();
            if (tieneSolicitudPaquete != null && tieneSolicitudPaquete.Estado=="Pendiente")
            {
                return View("ErrorSolicitud",tieneSolicitudPaquete);
            }
            else
            {
                var tienePaquete = _context.Paquete.FirstOrDefault(x => x.IdCliente == int.Parse(idclaim.Value));
                if (tienePaquete != null)
                {
                    return View("Error",tieneSolicitudPaquete.TipoSolicitud);
                }
                else
                {
                    solicitud.IdCliente = int.Parse(idclaim.Value);
                    solicitud.TipoSolicitud = "Paquete";
                    solicitud.FechaSolicitud = DateTime.Now;
                    solicitud.Estado = "Pendiente";
                    _context.Add(solicitud);
                    await _context.SaveChangesAsync();
                    return View("SolicitudEnviada");
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSolicitudPrestamo([Bind("IdSolicitud,TipoSolicitud,FechaSolicitud,IdCliente")] 
        Solicitud solicitud, bool EsPrendario,FormularioRaiz formularioRaiz,FormularioRechazo formularioRechazo)
        {
            var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
            
            
            //var idsolicitud = _context.Solicitudes.OrderByDescending(a => a.IdSolicitud).FirstOrDefault();
            var tieneSolicitudPrestamo = _context.Solicitudes.Where(x=>x.TipoSolicitud=="Prestamo").Where(x=>x.IdCliente==int.Parse(idclaim.Value)).FirstOrDefault();
            if (tieneSolicitudPrestamo!=null && tieneSolicitudPrestamo.Estado =="Pendiente")
            {
                return RedirectToAction("ErrorSolicitud",tieneSolicitudPrestamo);
            }
            else
            {
                var tienePrestamo = _context.Prestamo.FirstOrDefault(x => x.IdCliente == int.Parse(idclaim.Value));
                if (tienePrestamo != null)
                {
                    return View("Error", tieneSolicitudPrestamo.TipoSolicitud);
                }
                else
                {
                    solicitud.IdCliente = int.Parse(idclaim.Value);
                    solicitud.TipoSolicitud = "Prestamo";
                    solicitud.FechaSolicitud = DateTime.Now;
                    solicitud.Estado = "Pendiente";
                    _context.Add(solicitud);
                    await _context.SaveChangesAsync();
                    return View("SolicitudEnviada");
                }
            }
            
        }

        private bool SolicitudExists(int id)
        {
          return _context.Solicitudes.Any(e => e.IdSolicitud == id);
        }
    }
}
