using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using RestWithASPNETUdemy.Business.Implementation;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementation;
using System;

namespace RestWithASPNETUdemy
{
    public class Startup
    {

        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get;  }
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            this.Configuration = configuration;
            this._logger = logger;
            this._environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration["MySqlConnetion:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_environment.IsDevelopment())
            {
                try
                {
                    //var evolveConnection = new  MySql.Data.MySqlClient.MySqlConnection(connectionString);

                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Dependcy Injection
            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            //Versão de API
            services.AddApiVersioning();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
