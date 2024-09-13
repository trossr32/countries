using System.CommandLine;
using System.CommandLine.Invocation;
using CountriesParser.Console.ParameterConfig;
using CountriesParser.Core.Models.CommandModels;
using CountriesParser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CountriesParser.Console.Extensions;

public static class RunRootCommand
{
    /// <summary>
    /// Add run root command
    /// </summary>
    /// <param name="rootCommand"></param>
    /// <param name="serviceProvider"></param>
    public static RootCommand AddRunCommand(this RootCommand rootCommand, IServiceProvider serviceProvider)
    {
        rootCommand.AddCommand(
            Build(
                async (context) =>
                {
                    await Run(new RunModel
                    {
                        OutputDirectory = context.ParseResult.GetValueForOption(RunOptions.OutputDirectory)!
                    }, serviceProvider, context.GetCancellationToken());
                }));

        return rootCommand;
    }

    /// <summary>
    /// Build run command
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    private static Command Build(Func<InvocationContext, Task> func)
    {
        var getGuidCommand = new Command("run", "Run process");

        getGuidCommand.AddOption(RunOptions.OutputDirectory);

        getGuidCommand.SetHandler(func);

        return getGuidCommand;
    }


    /// <summary>
    /// Run on 'run' verb, e.g. run
    /// </summary>
    /// <param name="options"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    private static async Task Run(RunModel options, IServiceProvider serviceProvider, CancellationToken token)
    {
        var guidSvc = serviceProvider.GetService<ProcessService>()!;
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>()!;
        var logger = loggerFactory.CreateLogger(nameof(RunRootCommand));

        try
        {
            await guidSvc.Process(options, token);
        }
        catch (Exception e)
        {
            logger?.LogError(e, "Failed to run process");

            throw;
        }
    }
}