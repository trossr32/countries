namespace CountriesParser.Core.Models.CommandModels;

public class RunModel
{
    /// <summary>
    /// Output directory defined by the supplied command line argument
    /// </summary>
    public string OutputDirectory { get; set; } = null!;

    /// <summary>
    /// Run date and time, must be set when running the application
    /// </summary>
    public DateTime RunDate { get; } = DateTime.Now;

    /// <summary>
    /// Run directory based on th output directory and the run date and time
    /// </summary>
    public string RunDirectory => Path.Combine(OutputDirectory, $"{RunDate:yyyyMMdd_HHmmss}");
}