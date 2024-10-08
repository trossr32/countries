# Countries parser

## Description
Parses country data from various sources for database and CDN creation.

Uses the following sources for data:

- https://github.com/countries/countries - main data source
- https://github.com/hampusborgos/country-flags - flag images

Currently only contains currency codes, but currency symbols could potentially be retrieved from https://github.com/RubyMoney/money (although this may almost be too comprehensive).

## Usage

### PowerShell

```powershell
$repoLocation = "[Repo location]"
$publishLocation = "C:/Temp/countries-parser/publish"
$jobOutputLocation = "C:/Temp/countries-parser"
$project = "$repoLocation/countries/CountriesParser/CountriesParser.Console/CountriesParser.Console.csproj"
$exe = "$publishLocation/CountriesParser.Console.exe"

dotnet publish $project -c Release -r win-x64 -o $publishLocation -p:PublishSingleFile=True

Invoke-Expression "$exe run -o $jobOutputLocation"

Invoke-Item $jobOutputLocation
```

### Bash

```bash
repoLocation="/[Repo location]"
publishLocation="/tmp/countries-parser/publish"
jobOutputLocation="/tmp/countries-parser"
project="$repoLocation/countries/CountriesParser/CountriesParser.Console/CountriesParser.Console.csproj"
exe="$publishLocation/CountriesParser.Console"

dotnet publish $project -c Release -r linux-x64 -o $publishLocation -p:PublishSingleFile=True

$exe run -o $jobOutputLocation
```