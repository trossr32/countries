using CountriesParser.Core;
using CountriesParser.Core.Enums;
using CountriesParser.Core.Extensions;
using CountriesParser.Core.Models.CommandModels;
using CountriesParser.Core.Models.Countries;
using CountriesParser.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace CountriesParser.Tests;

public class GithubServiceTests
{
    private GithubService _githubSvc;
    private RunModel _options;
    private readonly List<CountryModel> _countries = [];

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _githubSvc = new GithubService(new NullLogger<GithubService>());

        _options = new RunModel
        {
            OutputDirectory = Directory.GetCurrentDirectory()
        };
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Directory.Delete(_options.RunDirectory, true);
    }

    [Order(10)]
    [TestCase(GithubRepo.Countries)]
    [TestCase(GithubRepo.CountryFlags)]
    public async Task DownloadRepo_ShouldDownloadAndExtract(GithubRepo repo)
    {
        await _githubSvc.DownloadGithubRepo(_options, repo, CancellationToken.None);

        var outPath = Path.Combine(_options.RunDirectory, repo.RepoOutputPath());

        Assert.That(Directory.Exists(outPath), Is.True);
    }

    [Test, Order(20)]
    public async Task CountriesJsonFiles_ShouldDeserialize()
    {
        var countriesRepo = Path.Combine(_options.RunDirectory, GithubRepo.Countries.RepoDataPath());

        var files = Directory.EnumerateFiles(countriesRepo, "*.json");

        foreach (var file in files)
        {
            var countryData = await _githubSvc.DeserializeCountryModel(file, CancellationToken.None);

            Assert.That(countryData, Is.Not.Null);
            Assert.That(countryData.Country.Name, Is.Not.Empty);

            _countries.Add(countryData);
        }
    }

    [Test, Order(30)]
    public void CountryFlags_ShouldExistForAllCountries()
    {
        var countryFlagsRepo = Path.Combine(_options.RunDirectory, GithubRepo.CountryFlags.RepoDataPath());

        var imagePaths = _countries
            .SelectMany(country => Constants.FileSystem.CountryFlagsImageDirectories
                .Select(imagePath => Path.Combine(countryFlagsRepo, imagePath.dir, $"{country.Country.Alpha2.ToLower()}.{imagePath.fileExtension}")));

        foreach (var imagePath in imagePaths)
        {
            Assert.That(File.Exists(imagePath), Is.True);
        }
    }
}