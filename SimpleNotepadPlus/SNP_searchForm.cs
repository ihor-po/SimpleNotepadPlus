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
    public partial class SNP_searchForm : Form
    {
        public string SearchText { get; set; }
        public string ReplaceText { get; set; }

        private bool isReplace;

        public SNP_searchForm(bool _isReplase)
        {
            isReplace = _isReplase;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (isReplace)
            {
                this.Size = new Size(this.Width, 121);
                label2.Visible = true;
                sf_replaseText.Visible = true;
                sf_button.Location =new Point(452, 41);
                sf_button.Text = "Заменить";
            }
            else
            {
                this.Size = new Size(this.Width, 82);
                label2.Visible = false;
                sf_replaseText.Visible = false;
                sf_button.Location = new Point(452, 8);
                sf_button.Text = "Найти";
            }

            sf_button.Click += Sf_button_Click;
        }

        private void Sf_button_Click(object sender, EventArgs e)
        {
            if (isReplace)
            {
                ReplaceText = sf_replaseText.Text;
            }

            SearchText = sf_searchText.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}
