using System.ComponentModel;
using System.Windows.Forms;

namespace SourceFolderCleanup.Controls
{
    public enum ProgressLinkLabelMode
    {
        Text,
        Progress
    }

    public partial class ProgressLinkLabel : UserControl
    {
        public ProgressLinkLabel()
        {
            InitializeComponent();
        }

        public event LinkLabelLinkClickedEventHandler LinkClicked;

        public new string Text
        {
            get { return linkLabel1.Text; }
            set { linkLabel1.Text = value; }
        }

        private ProgressLinkLabelMode _mode;

        [Browsable(false)]
        public ProgressLinkLabelMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                progressBar1.Visible = _mode == ProgressLinkLabelMode.Progress;
                linkLabel1.Visible = _mode == ProgressLinkLabelMode.Text;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked?.Invoke(sender, e);
        }
    }        
}
