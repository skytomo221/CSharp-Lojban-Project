# CSharp Lojban Project

![MIT License](https://img.shields.io/github/license/skytomo221/CSharp-Lojban-Project.svg)
![nuget v1.1.1](https://img.shields.io/nuget/v/Lojban.svg)
![download](https://img.shields.io/nuget/dt/Lojban.svg)

CSharp Lojban Projectは、C#でLojbanを処理するために作られたライブラリです。  

## Other Language

もし日本語が読めないときは次の文書を見てください。  
[ロジバン🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/README.md)
[英語🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.en.md)
[日本語🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ja.md)
[韓国語🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ko.md)
[中国語🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.zh.md)

この文書の原文は日本語であり、矛盾が生じた場合は日本語が優先されます。  
また誤訳が存在する場合は、issuesを立ててください。

ただし、ライセンスは英語が優先されます。

## Function

CSharp Lojban Projectでできる機能とできない機能を示します。

```diff
+ ロジバン文の解析結果を文字列で取得できる (>= 1.0)
+ ロジバン文の解析結果を整形されたJsonで取得できる (>= 1.1)
+ ilmentufaにあるモードで解析結果を取得できる (>= 1.1)
+ Jbovlasteから出力されたXMLを読み込むことができる (>= 1.1)
- ロジバン文の解析結果をオブジェクトで取得できる
- ロジバン文の解析が失敗したときに例外を投げてくれる
```

## Library

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NETは、.NET用の人気の高い高性能JSONフレームワークです。  
主に、ロジバンの解析結果をJSONに変換するときに使用されます。

[ilmentufa](http://www.lojban.github.io/ilmentufa)
[(Source repository)](https://github.com/lojban/ilmentufa)  
Ilmentufaは萩原雅人さんによって作られた、ロジバン語の正式な文法と構文解析のパーサー、ならびに関連ツールとインターフェースのコレクションです。

[camxes.js](http://www.masatohagiwara.net/camxes.js/)
[(Source repository)](https://github.com/mhagiwara/camxe.js)  
Camxes.jsは萩原雅人さんによって作られた、JavaScriptで書かれたロジバンのパーサーです。camxes PEGを基に作られています。

## License

ライセンスはMITライセンスです。
詳細は[こちら](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/LICENSE)から。

## Version

### Version 0.0

- Pegasusを用いたパーサ

### Version 1.0

- WebBrowserを用いたパーサ
- ilmentufaを用いたパーサ
- camxes.jsを用いたパーサ

#### Version 1.1

- Jbovlasteから出力されたXMLを読み込むことができるようになった
- パースのモードを改善
