namespace CountriesParser.Core.Extensions;

public static class SqlExtensions
{
    /// <summary>
    /// Converts a string to a SQL string <br />
    /// A null value will be converted to 'null' <br />
    /// A string value will be enclosed in single quotes and any single quotes in the string will be escaped
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToSql(this string? value) => 
        $"{(value is null ? "null" : $"'{value.Replace("'", "''")}'")}";

    /// <summary>
    /// Converts a boolean value to a SQL string <br />
    /// True will be converted to '1' <br />
    /// False will be converted to '0'
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToSql(this bool value) => value ? "1" : "0";
}