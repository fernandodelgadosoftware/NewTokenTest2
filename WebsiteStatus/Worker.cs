using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteStatus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //var file = new FileStream(@"C:\Users\admin\source\repos\WebStatusAppLogs\LogFilePhaseTwo.txt", FileMode.OpenOrCreate);

            Console.WriteLine("Hello world from the console!");
            //Create a StreamWriter
            //var file = new FileStream(@"C:\Users\admin\source\repos\WebStatusAppLogs\LogFilePhaseTwo.txt", FileMode.OpenOrCreate);
            //var writer = new StreamWriter(file);
            //Console.SetOut(writer);
            while (!stoppingToken.IsCancellationRequested)
            {

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var result = await client.GetAsync("https://www.iamtimcorey.com");
                if (result.IsSuccessStatusCode)
                {

                    _logger.LogInformation("The website is up and running. Status code {StatusCode}", result.StatusCode);


                    //Console.Write("The website is up and running.");
                    File.AppendAllText(@"C:\Users\admin\source\repos\WebStatusAppLogs\LogFilePhaseTwo.txt", "The website is up and running. Status code {StatusCode}\r\n");



                }

                else
                {
                    _logger.LogError("The website is down. Status code {StatusCode}", result.StatusCode);
                }
                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}

