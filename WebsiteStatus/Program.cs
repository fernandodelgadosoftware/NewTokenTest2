using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Extensions.Hosting;

namespace WebsiteStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            //    .Enrich.FromLogContext()
            //    .WriteTo.File(@"C:\Users\admin\source\repos\WebStatusAppLogs\LogFile.txt")                
            //    .CreateLogger();

            CreateHostBuilder(args).Build().Run();

           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });//.UseSerilog();
    }
}
