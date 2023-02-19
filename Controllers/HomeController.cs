using ELCAPITAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ELCAPITAL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ELCAPITALContext _context;
        public HomeController(ILogger<HomeController> logger,ELCAPITALContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            string nombre;
            if (User.IsInRole("Cliente"))
            {
                var idclaim = User.Claims.FirstOrDefault(x => x.Type == "Id");
                var cliente = _context.Cliente.FirstOrDefault(b => b.IdCliente == int.Parse(idclaim.Value));
                if (cliente is PersonaFisica personaFisica)
                {
                    nombre = personaFisica.Nombre;
                    ViewData["Nombre"] = nombre;
                    return View(cliente);
                }
                else if (cliente is PersonaJuridica personaJuridica)
                {
                    nombre = personaJuridica.NombreEmpresa;
                    ViewData["Nombre"] = nombre;
                    return View(cliente);
                }
            }
            else
            {
                nombre = "Administrador";
                ViewData["Nombre"] = nombre;
                return View();
            }
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}