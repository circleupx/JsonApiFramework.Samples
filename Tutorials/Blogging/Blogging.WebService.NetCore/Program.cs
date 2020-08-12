using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Blogging.WebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Host builder error");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureLogging((hostBuilderContext, loggingBuilder) =>
            {
                loggingBuilder.AddSerilog();
            });

            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.AddJsonFile("appsettings.json", false, true);
                configurationBuilder.AddEnvironmentVariables();
                configurationBuilder.AddCommandLine(args);
            });

            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseIIS();
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSerilog();
            });

            return hostBuilder;
        }
    }
}