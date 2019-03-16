# CSharp Lojban Project

CSharp Lojban Project is library for handling Lojban with C#  
Unfortunately, it is still in development.

## Other Language

If you can not read English, please refer to the following document:
[Lojban🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/README.md)
[English🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.en.md)
[Japanese🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ja.md)
[Korean🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ko.md)
[Chinese🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.zh.md)

The original text of this document is Japanese, and in the case of a contradiction, Japanese takes precedence.
If there is a mistranslation, please issue an issue.

However, the license is prioritized in English.

## Library

[Pegasus](http://otac0n.com/Pegasus/)
[(Source repository)](https://github.com/otac0n/Pegasus)  
Pegasus is a PEG (Parsing Expression Grammar) parser generator for .NET that integrates with MSBuild and Visual Studio.  
Lojban's grammar is written in PEG, so it is used when generating its parser.

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NET is a popular high-performance JSON framework for .NET  
Mainly used when converting Lojban analysis results to JSON.

[LojbanGrammer.peg](https://gist.github.com/otac0n/63d8fae45c551c4e8d41c83c53afc17e#file-lojbangrammar-peg)  
It is a peg file created by [John Gietzen](https://gist.github.com/otac0) who is the maker of Pegasus.  
Currently this is adopted.  
However, when compared with another Lojban parser, the result is different, so it needs to be corrected.

## License

This source is subject to the MIT license.
Please see [this](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/LICENSE) for more information.
