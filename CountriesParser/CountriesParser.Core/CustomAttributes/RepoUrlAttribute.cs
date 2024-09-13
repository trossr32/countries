namespace CountriesParser.Core.CustomAttributes;

[AttributeUsage(AttributeTargets.Field)]
public class RepoUrlAttribute : Attribute
{
    public string RepoUrl { get; set; }

    public RepoUrlAttribute(string repoUrl) => RepoUrl = repoUrl;
}