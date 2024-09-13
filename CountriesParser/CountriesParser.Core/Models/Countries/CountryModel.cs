using System.Text.Json.Serialization;

namespace CountriesParser.Core.Models.Countries;

public class CountryModel
{
    [JsonPropertyName("country")]
    public Country Country { get; set; } = null!;
}

public class Country
{
    /// <summary>
    /// Populate from WorldAtlas scrape
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("alpha2")]
    public string Alpha2 { get; set; } = null!;

    [JsonPropertyName("alpha3")]
    public string Alpha3 { get; set; } = null!;

    [JsonPropertyName("continent")]
    public string Continent { get; set; } = null!;

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = null!;

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; } = null!;

    [JsonPropertyName("distance_unit")]
    public string DistanceUnit { get; set; } = null!;

    [JsonPropertyName("gec")]
    public string Gec { get; set; } = null!;

    [JsonPropertyName("geo")]
    public GeoData GeoData { get; set; } = null!;

    [JsonPropertyName("international_prefix")]
    public string InternationalPrefix { get; set; } = null!;

    [JsonPropertyName("ioc")]
    public string Ioc { get; set; } = null!;

    [JsonPropertyName("iso_long_name")]
    public string IsoLongName { get; set; } = null!;

    [JsonPropertyName("iso_short_name")]
    public string IsoShortName { get; set; } = null!;

    [JsonPropertyName("languages_official")]
    public string[] LanguagesOfficial { get; set; } = null!;

    [JsonPropertyName("languages_spoken")]
    public string[] LanguagesSpoken { get; set; } = null!;

    [JsonPropertyName("national_destination_code_lengths")]
    public int?[] NationalDestinationCodeLengths { get; set; } = null!;

    [JsonPropertyName("national_number_lengths")]
    public int?[] NationalNumberLengths { get; set; } = null!;

    [JsonPropertyName("national_prefix")]
    public string NationalPrefix { get; set; } = null!;

    [JsonPropertyName("nationality")]
    public string Nationality { get; set; } = null!;

    [JsonPropertyName("number")]
    public string Number { get; set; } = null!;

    [JsonPropertyName("postal_code")]
    public bool PostalCode { get; set; }

    [JsonPropertyName("postal_code_format")]
    public string PostalCodeFormat { get; set; } = null!;

    [JsonPropertyName("region")]
    public string Region { get; set; } = null!;

    [JsonPropertyName("start_of_week")]
    public string StartOfWeek { get; set; } = null!;

    [JsonPropertyName("subregion")]
    public string SubRegion { get; set; } = null!;

    [JsonPropertyName("un_locode")]
    public string UnLocode { get; set; } = null!;

    [JsonPropertyName("unofficial_names")]
    public string[] UnofficialNames { get; set; } = null!;

    [JsonPropertyName("world_region")]
    public string WorldRegion { get; set; } = null!;

    [JsonPropertyName("address_format")]
    public string AddressFormat { get; set; } = null!;

    [JsonPropertyName("vat_rates")]
    public VatRates VatRates { get; set; } = null!;

    [JsonPropertyName("nanp_prefix")]
    public string NanpPrefix { get; set; } = null!;

    [JsonPropertyName("g20_member")]
    public bool G20Member { get; set; }

    [JsonPropertyName("eea_member")]
    public bool EeaMember { get; set; }

    [JsonPropertyName("eu_member")]
    public bool EuMember { get; set; }

    [JsonPropertyName("euvat_member")]
    public bool EuVatMember { get; set; }

    [JsonPropertyName("g7_member")]
    public bool G7Member { get; set; }

    [JsonPropertyName("esm_member")]
    public bool EsmMember { get; set; }

    [JsonPropertyName("alt_currency")]
    public string AltCurrency { get; set; } = null!;
}

public class GeoData
{
    [JsonPropertyName("latitude")]
    public float Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public float Longitude { get; set; }

    [JsonPropertyName("max_latitude")]
    public float MaxLatitude { get; set; }

    [JsonPropertyName("max_longitude")]
    public float MaxLongitude { get; set; }

    [JsonPropertyName("min_latitude")]
    public float MinLatitude { get; set; }

    [JsonPropertyName("min_longitude")]
    public float MinLongitude { get; set; }

    [JsonPropertyName("bounds")]
    public Bounds Bounds { get; set; } = null!;
}

public class Bounds
{
    [JsonPropertyName("northeast")]
    public Northeast NorthEast { get; set; } = null!;

    [JsonPropertyName("southwest")]
    public Southwest SouthWest { get; set; } = null!;
}

public class Northeast
{
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }

    [JsonPropertyName("lng")]
    public float Longitude { get; set; }
}

public class Southwest
{
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }

    [JsonPropertyName("lng")]
    public float Longitude { get; set; }
}

public class VatRates
{
    [JsonPropertyName("standard")]
    public float Standard { get; set; }

    [JsonPropertyName("reduced")]
    public float?[] Reduced { get; set; } = null!;

    [JsonPropertyName("super_reduced")]
    public float? SuperReduced { get; set; }

    [JsonPropertyName("parking")]
    public float? Parking { get; set; }
}
