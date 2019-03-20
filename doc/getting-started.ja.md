# Getting started

さあ、はじめよう。

## NuGet

Nuget からダウンロードすることができます。

1. ソリューション エクスプローラーで、ダウンロードしたいプロジェクトの「参照」を右クリックし、「NuGet パッケージの管理」をクリックします。
2. [パッケージ ソース] として "nuget.org" を選択し、「参照」タブを選択し、「Lojban」を検索し、一覧からそのパッケージを選択し、「インストール」を選択します。
3. ラインセンス プロンプトに同意します。
4. (Visual Studio 2017) パッケージ管理形式を選択するように求められたら、「プロジェクト ファイルの PackageReference」を選択します。
5. 変更の確認を求められた場合、「OK」を選択します。

## How to parse

では、まずロジバンの解析から始めてみましょう。

1. la ilmentufa を[ここ](https://github.com/lojban/ilmentufa)からダウンロードしてください。
2. ダウンロードしたフォルダを"./bin/Debug"または"./bin/Release"に置いてください。
3. 次のコードを書きます。

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
