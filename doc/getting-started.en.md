# Getting started

Well then, let's begin.

## NuGet

It can be downloaded from Nuget.

1. In Solution Explorer, right-click References for the project you want to download Lojban and choose Manage NuGet Packages.
2. Choose "nuget.org" as the Package source, select the Browse tab, search for **Lojban**, select that package in the list, and select Install.
3. Accept any license prompts.
4. (Visual Studio 2017) If prompted to select a package management format, select PackageReference in project file.
5. If prompted to review changes, select OK.

## How to parse

Let's start with Lojban analysis.

1. Please download la ilmentufa from [here](https://github.com/lojban/ilmentufa).
2. Set the downloaded folder to "./bin/Debug" or "./bin/Release".
3. Write code:

```cs
using Lojban;
using System;

namespace LojbanTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var text = "la.alis.co'a tatpi lo nu zutse lo rirxe korbi re'o lo mensi gi'e zukte fi no da";
            text = "coi";
            var parser = new LojbanParser();
            parser.ParserFile = "camxes";
            var result = parser.Parse(text);
            Console.WriteLine(result);
        }
    }
}
```
