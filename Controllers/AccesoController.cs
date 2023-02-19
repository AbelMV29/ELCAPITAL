using ELCAPITAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ELCAPITAL.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ELCAPITALContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccesoController(ELCAPITALContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Cliente cliente)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (cliente.NumeroDocumento!=37)
            {
                
                var existeCliente = await _context.Cliente.FirstOrDefaultAsync
                    (c => c.NumeroDocumento == cliente.NumeroDocumento);
                if (existeCliente != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim("Id",existeCliente.IdCliente.ToString()),
                    new Claim(ClaimTypes.Role,"Cliente")
                    };
                    var claimsIndetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndetity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var claimsIndetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndetity));

                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Acceso");

        }
    }
}
