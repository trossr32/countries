using System.ComponentModel;
using System.Globalization;
using CountriesParser.Core.CustomAttributes;
using CountriesParser.Core.Enums;

namespace CountriesParser.Core.Extensions;

public static class EnumExtensions
{
    public const string Github = "github";

    /// <summary>
    /// Get the value of a Description attribute on an enum member
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetDescription<T>(this T e) where T : IConvertible
    {
        if (e is not Enum)
            return string.Empty;

        var type = e.GetType();
        var values = Enum.GetValues(type);

        foreach (int val in values)
        {
            if (val != e.ToInt32(CultureInfo.InvariantCulture))
                continue;

            var memInfo = type.GetMember(type.GetEnumName(val)!);

            if (memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                return descriptionAttribute.Description;
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the value of a RepoUrl attribute on an enum member
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetRepoUrl<T>(this T e) where T : IConvertible
    {
        if (e is not Enum)
            return string.Empty;

        var type = e.GetType();
        var values = Enum.GetValues(type);

        foreach (int val in values)
        {
            if (val != e.ToInt32(CultureInfo.InvariantCulture))
                continue;

            var memInfo = type.GetMember(type.GetEnumName(val)!);

            if (memInfo[0]
                    .GetCustomAttributes(typeof(RepoUrlAttribute), false)
                    .FirstOrDefault() is RepoUrlAttribute repoUrlAttribute)
                return repoUrlAttribute.RepoUrl;
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the value of a RepoUrlDownload attribute on an enum member
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetRepoUrlDownload<T>(this T e) where T : IConvertible
    {
        if (e is not Enum)
            return string.Empty;

        var type = e.GetType();
        var values = Enum.GetValues(type);

        foreach (int val in values)
        {
            if (val != e.ToInt32(CultureInfo.InvariantCulture))
                continue;

            var memInfo = type.GetMember(type.GetEnumName(val)!);

            if (memInfo[0]
                    .GetCustomAttributes(typeof(RepoUrlDownloadAttribute), false)
                    .FirstOrDefault() is RepoUrlDownloadAttribute repoUrlDownloadAttribute)
                return repoUrlDownloadAttribute.RepoUrlDownload;
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the output path for a GithubRepo enum member
    /// </summary>
    /// <param name="repo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string RepoOutputPath(this GithubRepo repo) =>
        repo switch
        {
            GithubRepo.Countries => Path.Combine(Github, "countries"),
            GithubRepo.CountryFlags => Path.Combine(Github, "country-flags"),
            _ => throw new ArgumentOutOfRangeException(nameof(repo), repo, null)
        };

    /// <summary>
    /// Get the data path for a GithubRepo enum member
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="dataSubDirectory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string RepoDataPath(this GithubRepo repo, string? dataSubDirectory = null) =>
        repo switch
        {
            GithubRepo.Countries => Path.Combine(repo.RepoOutputPath(), "countries-data-json-master", "data", dataSubDirectory ?? "countries"),
            GithubRepo.CountryFlags => Path.Combine(repo.RepoOutputPath(), "country-flags-main"),
            _ => throw new ArgumentOutOfRangeException(nameof(repo), repo, null)
        };
}