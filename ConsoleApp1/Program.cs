using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ConsoleApp1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parser = new MyClass();
            var result = parser.Parse();
            Console.WriteLine(result ?? "(null)");
        }
    }

    public class MyClass
    {
        public string Text { get; set; }
        public object Result { get; private set; }
        public WebBrowser WebBrowser { get; } = new WebBrowser();
        public ParseResult ParseResultObject { get; set; } = new ParseResult();
        public string Parse()
        {
            WebBrowser.DocumentCompleted += delegate { Console.WriteLine("Are you OK?"); };
            WebBrowser.Navigate("about:blank");
            WebBrowser.Document.OpenNew(true);
            WebBrowser.Document.Write(@"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>title</title>
</head>
<body>
    <script>
        function cs_func(text) {
            external.Result = text;
        }
    </script>
    <h1>Hello!</h1>
</body>
</html>
");
            WebBrowser.ObjectForScripting = ParseResultObject;
            WebBrowser.Document.InvokeScript("cs_func", new string[] { "Hello, world!" });
            return ParseResultObject.Result;
        }

        [ComVisible(true)]
        public class ParseResult
        {
            public string Result { get; set; }
        }
    }
}
