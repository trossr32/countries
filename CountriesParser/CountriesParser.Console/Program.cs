using System.CommandLine;
using CountriesParser.Console.Extensions;
using CountriesParser.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CountriesParser.Console;

class Program
{
    private static readonly IServiceProvider ServiceProvider;

    static Program()
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();

        builder.Build();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}") // log to console
            .CreateLogger();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions();

        // logging
        services.AddLogging(configure => configure.AddSerilog());

        // services
        services.AddSingleton<ProcessService>();
        services.AddSingleton<GithubService>();
        services.AddSingleton<WorldAtlasService>();
    }

    /// <summary>
    /// App entry point. Use System.CommandLine to configure any command line arguments.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static async Task Main(string[] args)
    {
        var rootCommand = new RootCommand("app");

        rootCommand
            .AddRunCommand(ServiceProvider);

        await rootCommand.InvokeAsync(args);
    }
}