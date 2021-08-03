using eintech.api.Models;
using eintech.api.Repositories;
using eintech.api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eintech.api", Version = "v1" });
            });

            services.AddDbContext<PersonDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureLocalServices(services);
        }

        protected void ConfigureLocalServices(IServiceCollection services)
        {
            services.AddTransient<IPersonDeleteService, PersonDeleteService>(provider =>
            {
                var dbContext = provider.GetService<PersonDbContext>();
                return new PersonDeleteService(new PersonRepository(dbContext));
            });

            services.AddTransient<IPersonReadService, PersonReadService>(provider =>
            {
                var dbContext = provider.GetService<PersonDbContext>();
                return new PersonReadService(new PersonRepository(dbContext));
            });

            services.AddTransient<IPersonUpdateService, PersonUpdateService>(provider =>
            {
                var dbContext = provider.GetService<PersonDbContext>();
                return new PersonUpdateService(new PersonRepository(dbContext));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eintech.api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
