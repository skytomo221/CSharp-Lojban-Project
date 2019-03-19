# CSharp Lojban Project

![MIT License](https://img.shields.io/github/license/skytomo221/CSharp-Lojban-Project.svg)
![nuget v1.0.1](https://img.shields.io/nuget/v/Lojban.svg)

CSharp Lojban Project is library for handling Lojban with C#  
Unfortunately, it is still in development.

## Other Language

If you can not read English, please refer to the following document:
[Lojban🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/README.md)
[English🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.en.md)
[Japanese🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ja.md)
[Korean🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ko.md)
[Chinese🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.zh.md)

The original text of this document is Japanese, and in the case of a contradiction, Japanese takes precedence.
If there is a mistranslation, please issue an issue.

However, the license is prioritized in English.

## Function

Show below the functions that can be done and the functions that cannot be done in CSharp Lojban Project.

```diff
+ The result of analysis of Lojban statement can be obtained as a string (>= 1.0)
+ Analysis result of Lojban sentence can be obtained by Json formatted (>= 1.1)
+ Can get analysis results in the mode in ilmentufa (>= 1.1)
+ Can read XML output from Jbovlaste (>= 1.1)
- The result of the analysis of Lojban statement can be obtained as objects
- Throws an exception when parsing a Lojban statement fails
```

## Library

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NET is a popular high-performance JSON framework for .NET  
Mainly used when converting Lojban analysis results to JSON.

[ilmentufa](http://www.lojban.github.io/ilmentufa)
[(Source repository)](https://github.com/lojban/ilmentufa)  
Ilmentufa is a collection of formal grammars and syntactical parsers for the Lojban language, as well as related tools and interfaces. Made by Masato Hagiwara.

[camxes.js](http://www.masatohagiwara.net/camxes.js/)
[(Source repository)](https://github.com/mhagiwara/camxe.js)  
Camxes.js is a Lojban parser written in JavaScript. It is based on the camxes PEG. Made by Masato Hagiwara.

## License

This source is subject to the MIT license.
Please see [this](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/LICENSE) for more information.

## Version

### Version 0.0

- Parser using Pegasus

### Version 1.0

- Parser using WebBrowser
- Parser using ilmentufa
- Parser using camxes.js

#### Version 1.1

- Became possible to read the XML output from Jbovlaste
- Fix perspective mode
