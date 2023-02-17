using ELCAPITAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using NuGet.ProjectModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ELCAPITAL
{
    public class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Timer _timer;

        public HostedService(IHttpContextAccessor httpContextAccessor, 
            IServiceScopeFactory scopeFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
         {
             _timer = new Timer(AplicarRestriccion, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
             return Task.CompletedTask;
         }
         public Task StopAsync(CancellationToken cancellationToken)
         {
             _timer?.Change(Timeout.Infinite, 0);
             return Task.CompletedTask;
         }

         private void AplicarRestriccion(object state)
         {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                Restriccion restriccion = new Restriccion();
                var idclaim = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
                restriccion.Motivo = "Prueba";
                restriccion.IdCliente = int.Parse(idclaim.Value);
                restriccion.FechaAlta = DateTime.Now;
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ELCAPITALContext>();
                    dbContext.Add(restriccion);
                    dbContext.SaveChangesAsync();
                }
            }



        }

     }
}
    
