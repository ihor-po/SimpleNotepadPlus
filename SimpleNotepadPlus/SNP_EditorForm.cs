using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNotepadPlus
{
    public partial class SNP_EditorForm : Form
    {
        public RichTextBox editorRtb;

        public string fileName { get; set; } = "";

        public string filePath { get; set; } = "";

        public bool isCode;

        public SNP_EditorForm()
        {
            InitializeComponent();
            editorRtb = snpe_rtb;
        }

        protected override void OnLoad(EventArgs e)
        {
            editorRtb = snpe_rtb;
        }
    }
}
