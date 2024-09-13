using System.CommandLine;

namespace CountriesParser.Console.ParameterConfig;

internal static class RunOptions
{
    internal static readonly Option<string> OutputDirectory = new(aliases: ["-o", "--output-dir"], () => @"C:\Temp\countries-parser")
    {
        Description = "The output directory",
        IsRequired = false
    };
}