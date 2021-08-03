using eintech.api.Extensions;
using eintech.api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                  .MigrateDbContext<PersonDbContext>((context, services) =>
                  {
                      var env = ServiceProviderServiceExtensions.GetService<IWebHostEnvironment>(services);
                      var logger = ServiceProviderServiceExtensions.GetService<ILogger<PersonDbSeedContext>>(services);
                      new PersonDbSeedContext()
                      .SeedAsync(context, logger)
                      .Wait();
                  })
                .Run();

        }

        public static IHost CreateHostBuilder(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            return builder.Build();
        }
        
    }
}  
