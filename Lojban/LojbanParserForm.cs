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
        public ParseMode Mode { get; set; }
        public object Result { get; set; }
        public string DocumentText { get; set; }
        public LojbanParserForm(ParseMode mode)
        {
            var mode_str = string.Empty;
            if (mode.HasFlag(ParseMode.Indented)) mode_str += "I";
            if (mode.HasFlag(ParseMode.KeepMorphology)) mode_str += "M";
            if (mode.HasFlag(ParseMode.ShowSpaces)) mode_str += "S";
            if (mode.HasFlag(ParseMode.ShowTerminators)) mode_str += "T";
            if (mode.HasFlag(ParseMode.ShowWordClasses)) mode_str += "C";
            if (mode.HasFlag(ParseMode.RawOutput)) mode_str += "R";
            if (mode.HasFlag(ParseMode.ShowMainNodeLabels)) mode_str += "N";
            WebBrowser.Dock = DockStyle.Fill;
            Controls.Add(WebBrowser);
            Load += (s, e) =>
            {
                WebBrowser.ObjectForScripting = this;
                WebBrowser.Navigate("about:blank");
                WebBrowser.Document.OpenNew(true);
                WebBrowser.DocumentText = DocumentText;
                Application.DoEvents();
                WebBrowser.Document.InvokeScript("cs_func", new[] { Text, mode_str });
                DialogResult = DialogResult.OK;
            };
        }
    }
}
