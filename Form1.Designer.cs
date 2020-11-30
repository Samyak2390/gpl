namespace gpl
{
    partial class Form1
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
            this.editor = new System.Windows.Forms.RichTextBox();
            this.cli = new System.Windows.Forms.RichTextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // editor
            // 
            this.editor.AcceptsTab = true;
            this.editor.BackColor = System.Drawing.SystemColors.InfoText;
            this.editor.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.editor.Location = new System.Drawing.Point(701, 0);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(512, 574);
            this.editor.TabIndex = 0;
            this.editor.Text = "";
            // 
            // cli
            // 
            this.cli.AcceptsTab = true;
            this.cli.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cli.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cli.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cli.Location = new System.Drawing.Point(-2, 485);
            this.cli.Name = "cli";
            this.cli.Size = new System.Drawing.Size(704, 89);
            this.cli.TabIndex = 1;
            this.cli.Text = "";
            this.cli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cli_KeyPress);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel.Controls.Add(this.canvas);
            this.panel.Location = new System.Drawing.Point(-2, 31);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(704, 459);
            this.panel.TabIndex = 2;
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(0, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(701, 453);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1214, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1214, 574);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.cli);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox editor;
        private System.Windows.Forms.RichTextBox cli;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
    }
}

