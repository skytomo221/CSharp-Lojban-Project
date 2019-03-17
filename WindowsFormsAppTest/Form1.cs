using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAppTest
{
    public partial class Form1 : Form
    {
        public ParseResult parseResult;


        public Form1()
        {
            InitializeComponent();

            parseResult = new ParseResult();
            //webBrowser1.ObjectForScripting = new object();
            MainForm_Load();
        }

        public void MainForm_Load()
        {
            //var indexPath = Environment.CurrentDirectory + @"/../../base.html";
            //var encoding = Encoding.UTF8;
            //webBrowser1.Navigate(indexPath);
            webBrowser1.DocumentText = @"
<html>
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
    <title>タイトル</title>
</head>
<body>
    <script type=""text/javascript"" src="""+ Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes.js""></script>
    <script type=""text/javascript"" src="""+ Environment.CurrentDirectory.Replace("\\", "/") + @"/ilmentufa/camxes_postproc.js""></script>
    <script>
        function cs_func(text, mode) {
            var result = camxes.parse(text);
            var result_str = camxes_postprocessing(result, mode);
            window.external.Result = result_str;
            external.Result = ""Hello, world!"";
            alert(result_str);
        }
    </script>
    <h1>Hello!</h1>
</body>
</html>
";
        webBrowser1.ObjectForScripting = parseResult;
            Console.WriteLine("after " + webBrowser1.DocumentText);
        }

        [ComVisible(true)]
        public class ParseResult
        {
            public string Result { get; set; }
            public void SetResult(string s) { Result = s; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("cs_func", new string[] { "mi coi", "Raw output" });
            Console.WriteLine(parseResult.Result);
        }
    }
}
