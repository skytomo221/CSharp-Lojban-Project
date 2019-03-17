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
        public string Text { get; set; } = "coi";
        public string Mode { get; set; } = "Raw output";
        public object Result { get; private set; }
        public string ParserFile { get; set; } = "camxes";
        public LojbanParserForm LojbanParserForm { get; }
        public string DocumentText => @"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>title</title>
</head>
<body>
    <script type=""text/javascript"" src=""" + Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/" + ParserFile + @".js""></script>
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
        public LojbanParser() { }
        public LojbanParser(string text) { Text = text; }
        public string Parse()
        {
            var LojbanParserForm = new LojbanParserForm
            {
                Visible = false,
                Text = Text,
                Mode = Mode,
                FormBorderStyle = FormBorderStyle.None,
                Region = new Region(new GraphicsPath()),
                DocumentText = DocumentText
            };
            LojbanParserForm.ShowDialog();
            return LojbanParserForm.Result?.ToString();
        }
        public object Parse(string text)
        {
            Text = text;
            return Parse();
        }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public class LojbanParserForm : Form
    {
        private WebBrowser WebBrowser = new WebBrowser();
        public new string Text { get; set; }
        public string Mode { get; set; }
        public object Result { get; set; }
        public string DocumentText { get; set; }
        public LojbanParserForm()
        {
            WebBrowser.Dock = DockStyle.Fill;
            Controls.Add(WebBrowser);
            Load += (s, e) =>
            {
                WebBrowser.ObjectForScripting = this;
                WebBrowser.Navigate("about:blank");
                WebBrowser.Document.OpenNew(true);
                WebBrowser.DocumentText = DocumentText;
                Application.DoEvents();
                WebBrowser.Document.InvokeScript("cs_func", new[] { Text, Mode });
                DialogResult = DialogResult.OK;
            };
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parser = new LojbanParser();
            var result = parser.Parse("mi coi");
            Console.WriteLine(result ?? "(null)");
        }
    }
}
