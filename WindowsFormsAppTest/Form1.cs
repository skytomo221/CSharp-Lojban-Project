using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //webBrowser1.ObjectForScripting = new object();
            MainForm_Load();
        }

        public void MainForm_Load()
        {
            var indexPath = Environment.CurrentDirectory + @"/../../sample.html";
            var encoding = Encoding.UTF8;
            webBrowser1.Navigate(indexPath);
            webBrowser1.ObjectForScripting = new ObjectForScripting();
        }


        [ComVisible(true)]
        public class ObjectForScripting
        {
            string LogText { get; set; } = string.Empty;
            public ObjectForScripting() { }
            public void Log(string s)
            {
                LogText += s;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("test", new string[] { "mi coi", "Raw output" });
        }
    }
}
