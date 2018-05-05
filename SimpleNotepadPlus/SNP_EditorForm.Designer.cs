namespace TextEditor
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
            this.te_rtb_editor = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // te_rtb_editor
            // 
            this.te_rtb_editor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.te_rtb_editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.te_rtb_editor.Enabled = false;
            this.te_rtb_editor.Location = new System.Drawing.Point(0, 0);
            this.te_rtb_editor.Name = "te_rtb_editor";
            this.te_rtb_editor.Size = new System.Drawing.Size(617, 536);
            this.te_rtb_editor.TabIndex = 4;
            this.te_rtb_editor.Text = "";
            // 
            // SNP_EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 536);
            this.Controls.Add(this.te_rtb_editor);
            this.Name = "SNP_EditorForm";
            this.Text = "SNP_EditorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox te_rtb_editor;
    }
}