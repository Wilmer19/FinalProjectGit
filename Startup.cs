using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Final.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Final
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
            services.AddControllers();
            services.AddSwaggerDocument();

            services.AddDbContext<StudentClassContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("StudentClassContext")));

            services.AddScoped<IStudentClassContextDAO, StudentClassContextDAO>();

            services.AddDbContext<FoodContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("FoodContext")));
            
            services.AddScoped<IFoodContextDAO, FoodContextDAO>();

            services.AddDbContext<SportsContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("SportsContext")));

            services.AddScoped<ISportsContextDAO, SportsContextDAO>();

            services.AddDbContext<MusicContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("MusicContext")));

            services.AddScoped<IMusicContextDAO, MusicContextDAO>();


                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentClassContext context1, FoodContext context2, SportsContext context3, MusicContext context4)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            context1.Database.Migrate();
            context2.Database.Migrate();
            context3.Database.Migrate();
            context4.Database.Migrate();
            
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
