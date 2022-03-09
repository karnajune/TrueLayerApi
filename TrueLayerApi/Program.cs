using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace TrueLayerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                   .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

            string format = configuration["Logging:Details:Format"];
            string path = configuration["Logging:Details:Path"];

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Warning()
                    .Enrich.FromLogContext()
                    .WriteTo.Async(log => log.Console(theme: AnsiConsoleTheme.Literate))
                    .WriteTo.Async(log => log.File(path, rollingInterval: RollingInterval.Day, outputTemplate: format))
                    .CreateLogger();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
