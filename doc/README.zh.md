# CSharp Lojban Project

CSharp Lojban Project是一个用于在C＃中处理Lojban的库。  
不幸的是，它仍处于开发阶段。

## Other Language

如果您不懂中文，请查看以下文件。  
[逻辑语🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/README.md)
[英语🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.en.md)
[日语🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ja.md)
[朝鲜语🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ko.md)
[中文🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.zh.md)

本文档的原始文本是日文，在矛盾的情况下，日文优先。  
如果有误译，请发出问题。

但是，许可证的优先顺序是英语。

## Library

[Pegasus](http://otac0n.com/Pegasus/)
[(Source repository)](https://github.com/otac0n/Pegasus)  
Pegasus是用于.NET的PEG（解析表达式语法）解析器生成器，它与MS Build和Visual Studio集成。  
Lojban的语法是用PEG编写的，因此在生成解析器时使用它。

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NET是一种流行的.NET高性能JSON框架。
它主要用于将Lojban分析结果转换为JSON时。  

[LojbanGrammer.peg](https://gist.github.com/otac0n/63d8fae45c551c4e8d41c83c53afc17e#file-lojbangrammar-peg)  
这是由Pegasus制造商[John Gietzen](https://gist.github.com/otac0n)创建的peg文件。  
目前我用这个。  
但是，与另一个Lojban解析器相比，结果不同，因此需要进行更正。

## License

许可证是MIT许可证。  
来自[here](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/LICENSE)的详细信息。
