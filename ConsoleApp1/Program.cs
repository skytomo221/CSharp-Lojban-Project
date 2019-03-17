using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace ConsoleApp1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var result = ParserForm.Parse("Hello World");
            Console.WriteLine(result ?? "(null)");
            //Console.ReadKey();
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
                message = message,
                FormBorderStyle = FormBorderStyle.None,
                Region = region
            };
            form.ShowDialog();
            return form.Result?.ToString();
        }

        private WebBrowser webBrowser1 = new WebBrowser();
        private string message;
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
                webBrowser1.Document.Write(@"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>title</title>
</head>
<body>
    <script>
        function cs_func(text) {
            window.external.Result = text;
        }
    </script>
    <h1>Hello!</h1>
</body>
</html>
");
                Application.DoEvents();
                webBrowser1.Document.InvokeScript("cs_func", new[] { message });
                DialogResult = DialogResult.OK;
            };
        }
    }
}
