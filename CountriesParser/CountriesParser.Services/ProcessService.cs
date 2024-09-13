using System.Text;
using System.Text.Json;
using CountriesParser.Core;
using CountriesParser.Core.Enums;
using CountriesParser.Core.Extensions;
using CountriesParser.Core.Models.CommandModels;
using CountriesParser.Core.Models.Countries;
using Microsoft.Extensions.Logging;

namespace CountriesParser.Services;

public class ProcessService(ILogger<ProcessService> logger, GithubService githubSvc, WorldAtlasService worldAtlasSvc)
{
    /// <summary>
    /// Main process
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task Process(RunModel options, CancellationToken token)
    {
        // Download countries
        var countries = await githubSvc.GetCountries(options, token);

        if (countries.Count == 0)
        {
            logger.LogError("No countries found");

            return;
        }

        // Add country names
        countries = await worldAtlasSvc.AddCountryNameToCountries(countries, token);

        logger.LogInformation("Found {Count} countries", countries.Count);
        logger.LogInformation("Run directory: {RunDir}", options.RunDirectory);

        // Write countries to json file
        await WriteCountriesJsonFile(options, token, countries);

        // Write flags to directory
        WriteFlagsDirectory(options);

        // Generate SQL file
        await GenerateSqlFile(options, token, countries);
    }

    /// <summary>
    /// Write country flags to directory
    /// </summary>
    /// <param name="options"></param>
    private void WriteFlagsDirectory(RunModel options)
    {
        var flagsDir = Path.Combine(options.RunDirectory, "country-flag");

        if (!Directory.Exists(flagsDir))
            Directory.CreateDirectory(flagsDir);

        var countryFlagsRepo = Path.Combine(options.RunDirectory, GithubRepo.CountryFlags.RepoDataPath());
        
        foreach (var (dir, fileExtension) in Constants.FileSystem.CountryFlagsImageDirectories)
        {
            var sourceDir = Path.Combine(Path.Combine(countryFlagsRepo, dir));

            var files = Directory.EnumerateFiles(sourceDir, $"*.{fileExtension}");

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);

                var destFile = Path.Combine(flagsDir, fileName);

                File.Copy(file, destFile, true);
            }
        }

        logger.LogInformation("Country flags written to {FlagsDir}", flagsDir);
    }

    /// <summary>
    /// Write countries to json file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <param name="countries"></param>
    /// <returns></returns>
    private async Task WriteCountriesJsonFile(RunModel options, CancellationToken token, List<Country> countries)
    {
        var outFile = Path.Combine(options.RunDirectory, "countries.json");

        await File.WriteAllTextAsync(outFile, JsonSerializer.Serialize(countries), token);

        logger.LogInformation("Deserialized countries written to {OutFile}", outFile);
    }

    /// <summary>
    /// Generate SQL file
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <param name="countries"></param>
    /// <returns></returns>
    private async Task GenerateSqlFile(RunModel options, CancellationToken token, List<Country> countries)
    {
        var sql = new StringBuilder();

        sql.AppendLine("""
                       -- Country schema
                       DROP TABLE IF EXISTS `Country`;
                       CREATE TABLE IF NOT EXISTS `Country`(
                           `Id` int(11) NOT NULL AUTO_INCREMENT,
                           `Name` varchar(200) NOT NULL,
                           `IsoShortName` varchar(200) NOT NULL,
                           `IsoLongName` varchar(200) NOT NULL,
                           `IsoAlpha2` varchar(2) NOT NULL,
                           `IsoAlpha3` varchar(3) NOT NULL,
                           `UnCode` varchar(3) NOT NULL,
                           `CreatedOn` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
                           `ModifiedOn` datetime NULL ON UPDATE CURRENT_TIMESTAMP,
                           PRIMARY KEY (`Id`)
                       ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

                       -- Insert countries
                       """);

        foreach (var country in countries)
        {
            sql.AppendLine($"""
                            INSERT INTO `Country` (`Name`, `IsoShortName`, `IsoLongName`, `IsoAlpha2`, `IsoAlpha3`, `IsoNumeric`, `IsdCode`, `CurrencyCode`, `UsesPostcode`, `PostcodeFormat`, `ImagePngCdnPath`, `ImageSvgCdnPath`) 
                            VALUES ('{country.Name}', '{country.IsoShortName}', '{country.IsoLongName}', '{country.Alpha2}', '{country.Alpha3}', '{country.Number}', '{country.CountryCode}', '{country.CurrencyCode}', '{country.PostalCode}', '{country.PostalCodeFormat}', 'country-flag/{country.Alpha2.ToLower()}.png', 'country-flag/{country.Alpha2.ToLower()}.svg');
                            
                            """);
        }

        var sqlFile = Path.Combine(options.RunDirectory, "countries.sql");

        await File.WriteAllTextAsync(sqlFile, sql.ToString(), token);

        logger.LogInformation("SQL file generated at {OutFile}", sqlFile);
    }
}