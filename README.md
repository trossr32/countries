# Countries parser

## Description
Parses country data from various sources for database and CDN creation.

Uses the following sources for data:

- https://github.com/countries/countries - main data source
- https://github.com/hampusborgos/country-flags - flag images
- https://www.worldatlas.com/countries - for non-ISO country name

Currently only contains currency codes, but currency symbols could potentially be retrieved from https://github.com/RubyMoney/money (although this may almost be too comprehensive).

## Usage
```powershell
cd \countries\CountriesParser\CountriesParser.Console

dotnet publish "CountriesParser.Console.csproj" -c Release -r win-x64 -o 'C:/Temp/countries-parser/publish' -p:PublishSingleFile=True

cd 'C:/Temp/countries-parser/publish'

.\CountriesParser.Console.exe run -o 'C:\Temp\countries-parser'
```