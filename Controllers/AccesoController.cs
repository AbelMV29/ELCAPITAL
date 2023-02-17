using ELCAPITAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ELCAPITAL.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ELCAPITALContext _context;

        public AccesoController(ELCAPITALContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Cliente cliente)
        {
            var existeCliente = await _context.Cliente.FirstOrDefaultAsync
                (c => c.NumeroDocumento==cliente.NumeroDocumento);
            if (existeCliente !=null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Id",existeCliente.IdCliente.ToString())
                };
                var claimsIndetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndetity));

                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Acceso");

        }
    }
}
