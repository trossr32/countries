namespace CountriesParser.Core.CustomAttributes;

[AttributeUsage(AttributeTargets.Field)]
public class RepoUrlDownloadAttribute : Attribute
{
    public string RepoUrlDownload { get; set; }

    public RepoUrlDownloadAttribute(string repoUrlDownload) => RepoUrlDownload = repoUrlDownload;
}