namespace MiniER
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MiniER.Model.DataSchema dataSchema1 = new MiniER.Model.DataSchema();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_addTable = new System.Windows.Forms.ToolStripButton();
            this.btn_open = new System.Windows.Forms.ToolStripButton();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.btn_reverse = new System.Windows.Forms.ToolStripButton();
            this.btn_settings = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dialog_open = new System.Windows.Forms.OpenFileDialog();
            this.dialog_save = new System.Windows.Forms.SaveFileDialog();
            this.editPanel1 = new MiniER.Controls.EditPanel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_addTable,
            this.btn_open,
            this.btn_save,
            this.btn_reverse,
            this.btn_settings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(691, 29);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_addTable
            // 
            this.btn_addTable.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_addTable.Image = ((System.Drawing.Image)(resources.GetObject("btn_addTable.Image")));
            this.btn_addTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_addTable.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btn_addTable.Name = "btn_addTable";
            this.btn_addTable.Size = new System.Drawing.Size(85, 26);
            this.btn_addTable.Text = "Add Table";
            this.btn_addTable.Click += new System.EventHandler(this.btn_addTable_Click);
            // 
            // btn_open
            // 
            this.btn_open.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_open.Image = ((System.Drawing.Image)(resources.GetObject("btn_open.Image")));
            this.btn_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_open.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(62, 26);
            this.btn_open.Text = "Open";
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_save.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(57, 26);
            this.btn_save.Text = "Save";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_reverse
            // 
            this.btn_reverse.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_reverse.Image = ((System.Drawing.Image)(resources.GetObject("btn_reverse.Image")));
            this.btn_reverse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_reverse.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btn_reverse.Name = "btn_reverse";
            this.btn_reverse.Size = new System.Drawing.Size(120, 26);
            this.btn_reverse.Text = "Reverse from DB";
            this.btn_reverse.Click += new System.EventHandler(this.btn_reverse_Click);
            // 
            // btn_settings
            // 
            this.btn_settings.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_settings.Image = ((System.Drawing.Image)(resources.GetObject("btn_settings.Image")));
            this.btn_settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_settings.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(75, 26);
            this.btn_settings.Text = "Settings";
            this.btn_settings.Click += new System.EventHandler(this.btn_settings_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(691, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // editPanel1
            // 
            this.editPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editPanel1.Location = new System.Drawing.Point(0, 29);
            this.editPanel1.Name = "editPanel1";
            this.editPanel1.Schema = dataSchema1;
            this.editPanel1.Size = new System.Drawing.Size(691, 391);
            this.editPanel1.TabIndex = 2;
            this.editPanel1.OnEditTable += new MiniER.Controls.EditPanel.EditTable(this.editPanel1_OnEditTable);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 442);
            this.Controls.Add(this.editPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "Mini ER";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.EditPanel editPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_addTable;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton btn_open;
        private System.Windows.Forms.ToolStripButton btn_save;
        private System.Windows.Forms.ToolStripButton btn_reverse;
        private System.Windows.Forms.OpenFileDialog dialog_open;
        private System.Windows.Forms.SaveFileDialog dialog_save;
        private System.Windows.Forms.ToolStripButton btn_settings;
    }
}

