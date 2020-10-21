using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITPE3200_1_20H_nor_way.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ITPE3200_1_20H_nor_way
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<Nor_Way_Context>(options =>
                            options.UseSqlite("Data Source=Nor_Way_db.db").UseLazyLoadingProxies());
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IOrderTicketRepository, OrderTicketRepository>();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutter
                options.Cookie.IsEssential = true;
            });
            // Denne må også være med:
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddFile("Logs/ErrorLog.txt");
                DBInit.Initialize(app); // denne m? fjernes dersom vi vil beholde dataene i databasen og ikke initialisere 
            }

            app.UseRouting();
            app.UseSession();

            //app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}

