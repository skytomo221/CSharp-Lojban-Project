# Experienced Error

エラーが発生しましたか？  
大丈夫です。それはよくあることです。  
だから、エラーが発生しても、ショックを受けないでください。
私と一緒に原因を探しましょう。

## Other Language

もし日本語が読めないときは次の文書を見てください。  
[ロジバン🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.md)
[英語🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.en.md)
[日本語🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.ja.md)

## Get an exception

次のような例外が発生しましたか？

```md
System.Threading.ThreadStateException
  HResult=0x80131520
  Message=現在のスレッドはシングル スレッド アパートメントでないため、ActiveX コントロール '8856f961-xxxx-xxxx-xxxx-00c04fd705a2' をインスタンス化できません。
  Source=System.Windows.Forms
  スタック トレース:
   場所 System.Windows.Forms.WebBrowserBase..ctor(String clsidString)
   場所 System.Windows.Forms.WebBrowser..ctor()
   場所 Lojban.LojbanParser..ctor() (C:\UserName\Documents\Language\Lojban\CSharp Lojban Project\Lojban\LojbanParser.cs):行 38
   場所 LojbanTest.Program.Main(String[] args) (C:\UserName\Documents\Language\Lojban\CSharp Lojban Project\LojbanTest\Program.cs):行 12
```

おそらく、あなたのソースコードは次のようになっているでしょう。

```cs
using Lojban;
using System;

namespace LojbanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "la.alis.co'a tatpi lo nu zutse lo rirxe korbi re'o lo mensi gi'e zukte fi no da";
            var parser = new LojbanParser();
            var result = parser.Parse(text);
            Console.WriteLine(result);
        }
    }
}
```

Main関数の直前に`[STAThread]`をつけてください。

```diff
using Lojban;
using System;

namespace LojbanTest
{
    class Program
    {
+       [STAThread]
        static void Main(string[] args)
        {
            var text = "la.alis.co'a tatpi lo nu zutse lo rirxe korbi re'o lo mensi gi'e zukte fi no da";
            var parser = new LojbanParser();
            var result = parser.Parse(text);
            Console.WriteLine(result);
        }
    }
}
```

動くようになりましたか？

## Get an Script Error

次のようなスクリプトエラーが発生しましたか？

```txt
⚠️このページのスクリプトでエラーが発生しました。  

ライン: 13
文字:   13
エラー: 'camxes'は定義されていません。
コード: 0
URL:   about:blank

このページのスクリプトを実行し続けますか？
[はい(&Y)][いいえ(&N)]
```

プロジェクトのディレクトリを開いてください。  
あなたがDebugモードでビルドをするのなら、  
`./bin/Debug`に`ilmentufa`のフォルダをセットしてください。  
あなたがReleaseモードでビルドをするのなら、  
`./bin/Release`に`ilmentufa`のフォルダをセットしてください。

動くようになりましたか？

## Unkown Error

問題が解決しませんか？  
それはもしかしたらバグかもしれません。  
issuesへ行ってissueを立てましょう。
