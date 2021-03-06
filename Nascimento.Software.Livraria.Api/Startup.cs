using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Nascimento.Software.Livraria.Business.CompraService;
using Nascimento.Software.Livraria.Business.EmprestimoService;
using Nascimento.Software.Livraria.Business.LivroServices;
using Nascimento.Software.Livraria.Dominio.Dominios;
using Nascimento.Software.Livraria.Infraestrutura;
using Nascimento.Software.Livraria.Infraestrutura.Compra;
using Nascimento.Software.Livraria.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DevolucaoService>();
            services.AddScoped<AluguelService>();
            services.AddScoped<LivroServices>();

            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<processo_compra>();

            services.AddScoped<IRepositorio<Autor>, AutorRepositorio>();
            services.AddScoped<IRepositorio<Categoria>, CategoriaRepositorio>();


            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nascimento.Software.Livraria.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(option => option.AllowAnyOrigin()); ;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nascimento.Software.Livraria.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
