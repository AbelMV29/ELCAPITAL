using ELCAPITAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using NuGet.ProjectModel;
using System.Security.Claims;

namespace ELCAPITAL
{
    public class HostedService // IHostedService
    {
        /*private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ELCAPITALContext _context;
        public HostedService(IHttpContextAccessor httpContextAccessor,ELCAPITALContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        private Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(AplicarBaja, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var user = httpContext.User;
            return Task.CompletedTask;
            
        }

        public void AplicarBaja(object state)
        {
            var user = (ClaimsPrincipal)state;
            var idclaim=user.Claims.FirstOrDefault(x =>x.Type=="Id");
            Restriccion restriccion = new Restriccion();
            restriccion.IdCliente = int.Parse(idclaim.Value);
            restriccion.Motivo = "Prueba";
            restriccion.FechaAlta = DateTime.Now;
            _context.Add(restriccion);
            //httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }*/
    }
}
