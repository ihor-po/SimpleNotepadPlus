namespace SimpleNotepadPlus
{
    partial class SNP_searchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNP_searchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.sf_searchText = new System.Windows.Forms.TextBox();
            this.sf_replaseText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sf_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Искомый текст";
            // 
            // sf_searchText
            // 
            this.sf_searchText.Location = new System.Drawing.Point(148, 9);
            this.sf_searchText.Name = "sf_searchText";
            this.sf_searchText.Size = new System.Drawing.Size(295, 27);
            this.sf_searchText.TabIndex = 1;
            // 
            // sf_replaseText
            // 
            this.sf_replaseText.Location = new System.Drawing.Point(148, 42);
            this.sf_replaseText.Name = "sf_replaseText";
            this.sf_replaseText.Size = new System.Drawing.Size(295, 27);
            this.sf_replaseText.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Текст для замены";
            // 
            // sf_button
            // 
            this.sf_button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sf_button.Location = new System.Drawing.Point(452, 8);
            this.sf_button.Name = "sf_button";
            this.sf_button.Size = new System.Drawing.Size(122, 29);
            this.sf_button.TabIndex = 4;
            this.sf_button.Text = "button1";
            this.sf_button.UseVisualStyleBackColor = true;
            // 
            // SNP_searchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 82);
            this.Controls.Add(this.sf_button);
            this.Controls.Add(this.sf_replaseText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sf_searchText);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SNP_searchForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SNP_searchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sf_searchText;
        private System.Windows.Forms.TextBox sf_replaseText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sf_button;
    }
}