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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cli = new System.Windows.Forms.RichTextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.Location = new System.Drawing.Point(701, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(512, 574);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
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
            this.panel.Location = new System.Drawing.Point(-2, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(704, 488);
            this.panel.TabIndex = 2;
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(3, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(701, 488);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
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
            this.Controls.Add(this.richTextBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox cli;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox canvas;
    }
}

