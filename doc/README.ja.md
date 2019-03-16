# CSharp Lojban Project

CSharp Lojban Projectは、C#でLojbanを処理するために作られたライブラリです。  
残念ながら、まだ開発中です。

## Other Language

もし日本語が読めないときは次の文書を見てください。  
[ロジバン🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/README.md)
[英語🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.en.md)
[日本語🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ja.md)
[韓国語🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.ko.md)
[中国語🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/README.zh.md)

この文書の原文は日本語であり、矛盾が生じた場合は日本語が優先されます。  
また誤訳が存在する場合は、issuesを立ててください。

ただし、ライセンスは英語が優先されます。

## Library

[Pegasus](http://otac0n.com/Pegasus/)
[(Source repository)](https://github.com/otac0n/Pegasus)  
Pegasusは、MSBuildおよびVisual Studioと統合する、.NET用のPEG（Parsing Expression Grammar）パーサージェネレータです。  
ロジバンの文法はPEGで書かれているため、そのパーサを生成するときに使用されます。

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NETは、.NET用の人気の高い高性能JSONフレームワークです。  
主に、ロジバンの解析結果をJSONに変換するときに使用されます。

[LojbanGrammer.peg](https://gist.github.com/otac0n/63d8fae45c551c4e8d41c83c53afc17e#file-lojbangrammar-peg)  
Pegasusの製作者である、[John Gietzen](https://gist.github.com/otac0n)さんが作成したpegファイルです。  
現在はこれを使用しています。  
ただし、別のロジバンのパーサと比較すると、違う結果が出てしまうので、修正が必要になります。

## License

ライセンスはMITライセンスです。
詳細は[こちら](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/LICENSE)から。
