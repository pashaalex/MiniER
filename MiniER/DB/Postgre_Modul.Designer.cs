namespace MiniER.DB
{
    partial class Postgre_Modul
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_server = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_database = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tb_server
            // 
            this.tb_server.Location = new System.Drawing.Point(16, 21);
            this.tb_server.Name = "tb_server";
            this.tb_server.Size = new System.Drawing.Size(237, 20);
            this.tb_server.TabIndex = 0;
            this.tb_server.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server";
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(259, 21);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(106, 20);
            this.tb_port.TabIndex = 2;
            this.tb_port.Text = "5432";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Login:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password:";
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(73, 55);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(180, 20);
            this.tb_login.TabIndex = 6;
            this.tb_login.Text = "postgres";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(73, 89);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(180, 20);
            this.tb_password.TabIndex = 7;
            this.tb_password.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Database:";
            // 
            // cb_database
            // 
            this.cb_database.FormattingEnabled = true;
            this.cb_database.Location = new System.Drawing.Point(73, 127);
            this.cb_database.Name = "cb_database";
            this.cb_database.Size = new System.Drawing.Size(180, 21);
            this.cb_database.TabIndex = 9;
            this.cb_database.DropDown += new System.EventHandler(this.cb_database_DropDown);
            // 
            // Postgre_Modul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_database);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_login);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_port);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_server);
            this.Name = "Postgre_Modul";
            this.Size = new System.Drawing.Size(380, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_database;
    }
}
