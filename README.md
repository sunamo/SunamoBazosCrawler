# SunamoBazosCrawler

## Short description

Knihovna pro parsování inzerátů z webů bazos.cz a bazos.sk. Je součástí sady balíčků Sunamo.


Parsing of advertisements from bazos.cz/.sk

## Overview

SunamoBazosCrawler is part of the Sunamo package ecosystem, providing modular, platform-independent utilities for .NET development. It crawls and parses dating advertisements from the Bazos website.

## Main Components

### Key Classes

- **BazosCrawlerHelper** - Helper class for crawling and parsing dating advertisements from Bazos website
- **DatingAd** - Represents a dating advertisement with Title, Description, Price, and Location

### Key Methods

- `BazosCrawlerHelper.ParseFromOnline(url, downloadContentFunc)` - Parses dating advertisements from the specified URL

## Installation

```bash
dotnet add package SunamoBazosCrawler
```

## Dependencies

- **HtmlAgilityPack** (v1.12.4)
- **Microsoft.Extensions.Logging.Abstractions** (v10.0.2)

## Target Frameworks

- net10.0
- net9.0
- net8.0

## Package Information

- **Package Name**: SunamoBazosCrawler
- **License**: MIT
- **Authors**: www.sunamo.cz

## Related Packages

This package is part of the Sunamo package ecosystem. For more information about related packages, visit the main repository.

## License

See the repository root for license information.

<!-- sunamo:test-coverage:start -->
## Test coverage

| Metric | Value |
|---|---|
| Method | static |
| Test projects | 1 |
| Public types referenced by tests | 0/2 (0.0 %) |
| Public methods referenced by tests | 0/1 (0.0 %) |
| Line coverage | n/a |
| Branch coverage | n/a |
| Generated (UTC) | 2026-11-22 21:24 |

<!-- sunamo:test-coverage:data {"package":"SunamoBazosCrawler","container":"pinp","testProjects":1,"publicTypes":2,"typesReferencedByTests":0,"publicMethods":1,"methodsReferencedByTests":0,"lineRate":null,"branchRate":null,"method":"static","generatedUtc":"2026-11-22 21:24"} -->
<!-- sunamo:test-coverage:end -->
