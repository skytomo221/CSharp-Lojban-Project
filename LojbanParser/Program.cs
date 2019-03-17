using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace LojbanParser
{

    public class LojbanParser
    {
        public string Text { get; set; }
        public object Result { get; private set; }
        public string DocumentText => @"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>title</title>
</head>
<body>
    <script type=""text/javascript"" src=""" + Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes.js""></script>
    <script type=""text/javascript"" src=""" + Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes_postproc.js""></script>
    <script>
        function cs_func(text, mode) {
            var result = camxes.parse(text);
            var result_str = camxes_postprocessing(result, mode);
            external.Result = result_str;
            return result_str;
        }
    </script>
    <h1>Hello!</h1>
</body>
</html>
";
        public WebBrowser WebBrowser { get; } = new WebBrowser();
        public LojbanParser() { Initialize(); }
        public LojbanParser(string text) { Initialize(); Text = text; }
        public void Initialize() { WebBrowser.DocumentCompleted += WebBrowser_DocumentCompleted; }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine("done.");
        }
        public string Parse()
        {
            var parse = new ParseResult();
            WebBrowser.Navigate("about:blank");
            WebBrowser.Document.OpenNew(true);
            WebBrowser.Document.Write(DocumentText);
            WebBrowser.ObjectForScripting = parse;
            WebBrowser.Document.InvokeScript("cs_func", new string[] { Text, "Raw output" });
            Console.WriteLine();
            return parse.Result;
        }
        public object Parse(string text)
        {
            Text = text;
            return Parse();
        }

        [ComVisible(true)]
        public class ParseResult
        {
            public string Result { get; set; }
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //var parser = new LojbanParser();
            //var result = parser.Parse("mi coi");
            //Console.WriteLine(result ?? "●●●\t残念ながら、\t●●●");
            var result = ParserForm.Parse("coi");
            Console.WriteLine(result ?? "(null)");
            Console.ReadKey();
        }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public class ParserForm : Form
    {
        public static string Parse(string message)
        {
            var region = new Region(new GraphicsPath());
            var form = new ParserForm
            {
                Visible = false,
                Text = message,
                FormBorderStyle = FormBorderStyle.None,
                Region = region
            };
            form.ShowDialog();
            return form.Result?.ToString();
        }

        private WebBrowser webBrowser1 = new WebBrowser();
        public new string Text { get; set; }
        public object Result { get; set; }

        public ParserForm()
        {
            webBrowser1.Dock = DockStyle.Fill;
            Controls.Add(webBrowser1);
            Load += (s, e) =>
            {
                webBrowser1.ObjectForScripting = this;
                webBrowser1.Navigate("about:blank");
                webBrowser1.Document.OpenNew(true);
                webBrowser1.DocumentText = @"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>title</title>
</head>
<body>
    <script type=""text/javascript"" src=""" + Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes.js""></script>
    <script type=""text/javascript"" src=""" + Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes_postproc.js""></script>
    <script>
        function cs_func(text, mode) {
            var result = camxes.parse(text);
            var result_str = camxes_postprocessing(result, mode);
            external.Result = result_str;
        }
    </script>
    <h1>Hello!</h1>
</body>
</html>
";
                Application.DoEvents();
                webBrowser1.Document.InvokeScript("cs_func", new[] { Text, "Raw output" });
                DialogResult = DialogResult.OK;
            };
        }
    }
}
