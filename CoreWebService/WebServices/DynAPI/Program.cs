using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DynAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
              config.SetBasePath(System.IO.Directory.GetCurrentDirectory());
              config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
              config.AddCommandLine(args);
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder
              .UseIISIntegration()
              .UseStartup<Startup>();

          });
    }
}
