using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nascimento.Software.Livraria_WebApp.Data;

[assembly: HostingStartup(typeof(Nascimento.Software.Livraria_WebApp.Areas.Identity.IdentityHostingStartup))]
namespace Nascimento.Software.Livraria_WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}