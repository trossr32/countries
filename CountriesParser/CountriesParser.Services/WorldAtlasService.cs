using CountriesParser.Core;
using CountriesParser.Core.Models.Countries;
using Flurl.Http;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace CountriesParser.Services;

public class WorldAtlasService(ILogger<WorldAtlasService> logger)
{
    /// <summary>
    /// Add country name to countries from WorldAtlas
    /// </summary>
    /// <param name="countries"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [Obsolete("This data can be derived from the github countries repo, but leaving in case required later")]
    public async Task<List<Country>> AddCountryNameToCountries(List<Country> countries, CancellationToken token)
    {
        // Download the HTML content of the page
        var html = await Constants.WorldAtlas.Url.GetStringAsync(cancellationToken: token);

        // Load HTML content into a XmlDocument object for parsing
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        foreach (var node in doc.DocumentNode.SelectNodes("//li[contains(@class, 'country_landing_list_item')]")) {

            // Extract country details
            var countryName = node.SelectSingleNode(".//h3[@id='item_name']/a").InnerText.Trim();
            //var iso2 = node.SelectSingleNode(".//td[@id='item_iso2']").InnerText.Trim();
            var iso3 = node.SelectSingleNode(".//td[@id='item_iso3']").InnerText.Trim();
            //var dialingCode = node.SelectSingleNode(".//tr[th[text()='Dialing Code']]/td").InnerText.Trim();

            // Get the related country object by ISO3
            var country = countries.FirstOrDefault(c => c.Alpha3.Equals(iso3, StringComparison.OrdinalIgnoreCase));

            if (country is null)
            {
                logger.LogError("Country not found in WorldAtlas data for ISO3 {Iso3}", iso3);

                continue;
            }

            // Update the country name
            country.Name = countryName;
        }
        
        return countries;
    }
}