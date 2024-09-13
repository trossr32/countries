namespace CountriesParser.Core;

public static class Constants
{
    public static class Github
    {
        public const string CountriesRepoUrl = "https://github.com/countries/countries-data-json";
        public const string CountriesRepoDownloadUrl = "https://github.com/countries/countries-data-json/archive/refs/heads/master.zip";

        public const string CountryFlagsRepoUrl = "https://github.com/hampusborgos/country-flags";
        public const string CountryFlagsRepoDownloadUrl = "https://github.com/hampusborgos/country-flags/archive/refs/heads/main.zip";
    }

    public static class FileSystem
    {
        public static readonly List<(string dir, string fileExtension)> CountryFlagsImageDirectories =
        [
            //new("png1000px", "png"),
            //new("png100px", "png"),
            new("png250px", "png"),
            new("svg", "svg")
        ];
    }

    public static class WorldAtlas
    {
        public const string Url = "https://www.worldatlas.com/countries";
    }
}