# Experienced Error

Did you get an error?  
It's okay. That is often the case.  
So don't be shocked if you get an error.  
Let's find the cause with me.

## Other Language

If you can not read English, please refer to the following document:
[Lojban🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.md)
[English🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.en.md)
[Japanese🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/develop/doc/experienced-error.ja.md)

## Get an exception

Did you get the following exception?

```md
System.Threading.ThreadStateException
  HResult=0x80131520
  Message=ActiveX control '8856f961-xxxx-xxxx-xxxx-00c04fd705a2' cannot be instantiated becuase the current thread is not in a single-thread apartment.
  Source=System.Windows.Forms
  Stack trace:
   at System.Windows.Forms.WebBrowserBase..ctor(String clsidString)
   at System.Windows.Forms.WebBrowser..ctor()
   at Lojban.LojbanParser..ctor() (C:\UserName\Documents\Language\Lojban\CSharp Lojban Project\Lojban\LojbanParser.cs):Line 38
   at LojbanTest.Program.Main(String[] args) (C:\UserName\Documents\Language\Lojban\CSharp Lojban Project\LojbanTest\Program.cs):Line 12
```

Perhaps your source code looks like this:

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

Add `[STAThread]` attribute to your main method.

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

Dit it work?

## Get an Script Error

Did you get the following error?

```txt
⚠️ An error has occurred in the script on this page.

Line:  13
Char:  13
Error: 'camxes' is undefined.
Code:  0
URL:   about:blank

Do you want to continue running scripts on this page?
[Yes][No]
```

Open the project directory.  
Set `ilmentufa` to `./bin/Debug` if you build in debug mode.  
Set `ilmentufa` to `./bin/Release` if you build in release mode.

Dit it work?

## Unkown Error

Isn't the problem solved?  
That may be a bug.  
Post your issue.
