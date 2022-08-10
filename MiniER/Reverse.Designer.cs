namespace MiniER
{
    partial class Reverse
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
            this.ddl_engine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pn_db = new System.Windows.Forms.Panel();
            this.btn_Next = new System.Windows.Forms.Button();
            this.pn_connection = new System.Windows.Forms.Panel();
            this.btn_OK = new System.Windows.Forms.Button();
            this.pn_connection.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddl_engine
            // 
            this.ddl_engine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_engine.FormattingEnabled = true;
            this.ddl_engine.Location = new System.Drawing.Point(94, 16);
            this.ddl_engine.Name = "ddl_engine";
            this.ddl_engine.Size = new System.Drawing.Size(170, 21);
            this.ddl_engine.TabIndex = 0;
            this.ddl_engine.SelectedIndexChanged += new System.EventHandler(this.ddl_engine_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select engine:";
            // 
            // pn_db
            // 
            this.pn_db.Location = new System.Drawing.Point(15, 43);
            this.pn_db.Name = "pn_db";
            this.pn_db.Size = new System.Drawing.Size(381, 238);
            this.pn_db.TabIndex = 2;
            // 
            // btn_Next
            // 
            this.btn_Next.Location = new System.Drawing.Point(17, 310);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(75, 23);
            this.btn_Next.TabIndex = 3;
            this.btn_Next.Text = "Next >>";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // pn_connection
            // 
            this.pn_connection.Controls.Add(this.pn_db);
            this.pn_connection.Controls.Add(this.ddl_engine);
            this.pn_connection.Controls.Add(this.label1);
            this.pn_connection.Location = new System.Drawing.Point(2, 3);
            this.pn_connection.Name = "pn_connection";
            this.pn_connection.Size = new System.Drawing.Size(405, 301);
            this.pn_connection.TabIndex = 4;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(98, 310);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 5;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Visible = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // Reverse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 343);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.pn_connection);
            this.Controls.Add(this.btn_Next);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reverse";
            this.Text = "Reverse";
            this.pn_connection.ResumeLayout(false);
            this.pn_connection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ddl_engine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pn_db;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Panel pn_connection;
        private System.Windows.Forms.Button btn_OK;
    }
}