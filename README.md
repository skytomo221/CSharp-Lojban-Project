# CSharp Lojban Project

CSharp Lojban Project is library for handling Lojban with C#  
Unfortunately, it is still in development.

## lo bangu ku drata lo banjubu'o ku

**HELP!**
Do you speak Lojban?
Can you help translate to Lojban?
We are glad to welcome it.

If you can not read banjubu'o, please refer to the following document:
[banjubu'o🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/README.md)
[bangenugu🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.en.md)
[banjupunu🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ja.md)
[bangrxangu🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ko.md)
[banzuxe'o🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.zh.md)

The original text of this document is Japanese, and in the case of a contradiction, Japanese takes precedence.
If there is a mistranslation, please issue an issue.

However, the license is prioritized in English.

## ckusro

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

## curmi pilno

This source is subject to the MIT license.
Please see [this](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/LICENSE) for more information.
