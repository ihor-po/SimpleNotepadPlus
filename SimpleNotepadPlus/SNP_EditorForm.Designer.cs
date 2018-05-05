namespace SimpleNotepadPlus
{
    partial class SNP_EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNP_EditorForm));
            this.snpe_rtb = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // snpe_rtb
            // 
            this.snpe_rtb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.snpe_rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snpe_rtb.Location = new System.Drawing.Point(0, 0);
            this.snpe_rtb.Name = "snpe_rtb";
            this.snpe_rtb.Size = new System.Drawing.Size(613, 457);
            this.snpe_rtb.TabIndex = 4;
            this.snpe_rtb.Text = "";
            // 
            // SNP_EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 457);
            this.Controls.Add(this.snpe_rtb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SNP_EditorForm";
            this.Text = "SNP_EditorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox snpe_rtb;
    }
}