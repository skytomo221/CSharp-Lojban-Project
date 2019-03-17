using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public MyClass Parser;

        public Form1()
        {
            InitializeComponent();
            Parser = new MyClass();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var result = Parser.Parse();
            Console.WriteLine(result ?? "(null)");
        }
    }

    public class MyClass
    {
        public string Text { get; set; }
        public object Result { get; private set; }
        public WebBrowser WebBrowser { get; set; } = new WebBrowser();
        public ParseResult ParseResultObject { get; set; } = new ParseResult();
        public string Parse()
        {
            WebBrowser.DocumentText = @"
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
";
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
