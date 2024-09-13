using CountriesParser.Core.CustomAttributes;

namespace CountriesParser.Core.Enums;

public enum GithubRepo
{
    [RepoUrl(Constants.Github.CountriesRepoUrl)]
    [RepoUrlDownload(Constants.Github.CountriesRepoDownloadUrl)]
    Countries,

    [RepoUrl(Constants.Github.CountryFlagsRepoUrl)]
    [RepoUrlDownload(Constants.Github.CountryFlagsRepoDownloadUrl)]
    CountryFlags
}