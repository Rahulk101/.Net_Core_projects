using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel;
using System.IO;

namespace Venkat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder=>
                    {
                        //webBuilder.UseUrls("http://localhost:5200");
                        webBuilder.UseStartup<Startup>();
                    });

        // public static IHostBuilder CreateHostBuilder(string[] args)
        // {
        //     var config = new ConfigurationBuilder()  
        //         .SetBasePath(Directory.GetCurrentDirectory())  
        //         .AddJsonFile("appsettings.json", optional: false)  
        //         .Build();
        //     var WebHost=Host.CreateDefaultBuilder(args)
        //                 .ConfigureWebHostDefaults(webBuilder =>
        //                 {
        //                     webBuilder.UseUrls($"http://localhost:{config.GetValue<int>("Host:Port")}");
        //                     webBuilder.UseStartup<Startup>();
        //                 });
        //     return WebHost;
        // }
            
        
    }
}
