using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleAppProject;

public class Program
{
    public static async Task Main(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        var appSettingsFile = string.IsNullOrWhiteSpace(env)
            ? "appsettings.json"
            : $"appsettings.{env}.json";

        //TODO: Add logging

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                config.AddEnvironmentVariables();
                config.AddUserSecrets<Program>();
            })
            .ConfigureServices((context, services) =>
            {
                var itWorks = context.Configuration["Test:Setting1"];
                Console.WriteLine($"Configuration Test: ItWorks = {itWorks}");
                // Add other services here if needed
                services.AddTransient<Application>();
            }).Build();

        using var scope = host.Services.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<Application>();
        await app.DoWork();
    }
}