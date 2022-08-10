namespace MiniER
{
    partial class SettingsForm
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
            this.cb_show_schema = new System.Windows.Forms.CheckBox();
            this.cb_chow_datatype = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_show_schema
            // 
            this.cb_show_schema.AutoSize = true;
            this.cb_show_schema.Location = new System.Drawing.Point(12, 12);
            this.cb_show_schema.Name = "cb_show_schema";
            this.cb_show_schema.Size = new System.Drawing.Size(93, 17);
            this.cb_show_schema.TabIndex = 0;
            this.cb_show_schema.Text = "Show schema";
            this.cb_show_schema.UseVisualStyleBackColor = true;
            // 
            // cb_chow_datatype
            // 
            this.cb_chow_datatype.AutoSize = true;
            this.cb_chow_datatype.Location = new System.Drawing.Point(12, 35);
            this.cb_chow_datatype.Name = "cb_chow_datatype";
            this.cb_chow_datatype.Size = new System.Drawing.Size(97, 17);
            this.cb_chow_datatype.TabIndex = 1;
            this.cb_chow_datatype.Text = "Show datatype";
            this.cb_chow_datatype.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(138, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 130);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_chow_datatype);
            this.Controls.Add(this.cb_show_schema);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cb_show_schema;
        private System.Windows.Forms.CheckBox cb_chow_datatype;
        private System.Windows.Forms.Button button1;
    }
}