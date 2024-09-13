using System.IO.Compression;
using System.Text.Json;
using CountriesParser.Core.Enums;
using CountriesParser.Core.Extensions;
using CountriesParser.Core.Models.CommandModels;
using CountriesParser.Core.Models.Countries;
using Flurl.Http;
using Microsoft.Extensions.Logging;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CountriesParser.Tests")]
namespace CountriesParser.Services;

public class GithubService(ILogger<GithubService> logger)
{
    /// <summary>
    /// Get countries and save flags from Github repositories
    /// </summary>
    /// <param name="options"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<Country>> GetCountries(RunModel options, CancellationToken token)
    {
        foreach (var githubRepo in Enum.GetValues<GithubRepo>())
        {
            await DownloadGithubRepo(options, githubRepo, token);
        }

        var countriesRepo = Path.Combine(options.RunDirectory, GithubRepo.Countries.RepoDataPath());

        var translations = await File.ReadAllTextAsync(Path.Combine(options.RunDirectory, GithubRepo.Countries.RepoDataPath("translations"), "countries-en.json"), token);

        var countryNames = JsonSerializer.Deserialize<Dictionary<string, string>>(translations)!;

        var files = Directory.EnumerateFiles(countriesRepo, "*.json");

        var countries = new List<Country>();

        foreach (var file in files)
        {
            var country = await DeserializeCountryModel(file, token);

            if (country is not null)
            {
                country.Country.Name = countryNames[country.Country.Alpha2];

                countries.Add(country.Country);

                continue;
            }

            logger.LogError("Failed to deserialize {File}", file);

            throw new Exception($"Failed to deserialize {file}");
        }

        return countries;
    }

    /// <summary>
    /// Deserialize a country model from a json file
    /// </summary>
    /// <param name="file"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    internal async Task<CountryModel?> DeserializeCountryModel(string file, CancellationToken token)
    {
        var lines = await File.ReadAllLinesAsync(file, token);

        // rename root object to "country" to unify all countries because as standard the root object is the country ISO2 code
        lines[1] = "  \"country\": {";

        // convert string array to string
        var json = string.Join("\n", lines);

        return JsonSerializer.Deserialize<CountryModel>(json);
    }

    /// <summary>
    /// Download Github repository
    /// </summary>
    /// <param name="options"></param>
    /// <param name="repo"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal async Task DownloadGithubRepo(RunModel options, GithubRepo repo, CancellationToken token)
    {
        // download repo
        var zipStream = await repo.GetRepoUrlDownload().GetStreamAsync(cancellationToken: token);

        var outPath = Path.Combine(options.RunDirectory, repo.RepoOutputPath());

        // extract zip
        logger.LogInformation("Extracting {Repo} repository to {Path}", repo, outPath);

        ZipFile.ExtractToDirectory(zipStream, outPath);
    }
}