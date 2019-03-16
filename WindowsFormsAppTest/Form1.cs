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

            webBrowser1.ObjectForScripting = new TestClass();
            MainForm_Load();
        }

        public void MainForm_Load()
        {
            var indexPath = @"D:/skytomo/Documents/Language/Lojban/CSharp Lojban Project/WindowsFormsAppTest/sample.html";
            var encoding = Encoding.UTF8;
            webBrowser1.Navigate(indexPath);
        }

        [ComVisible(true)]
        public class TestClass
        {
            public void TestFunc(string s)
            {
                MessageBox.Show(s, "戻り値", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("sample", new string[] { "あいうえお", "さしすせそ" });
        }
    }
}
