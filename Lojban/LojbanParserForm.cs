using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Lojban
{
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
}
