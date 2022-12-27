namespace EffectDialog
{
    partial class Form2
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.EffectSettingsControl = new System.Windows.Forms.TabControl();
            this.None = new System.Windows.Forms.TabPage();
            this.scrollableControl3 = new System.Windows.Forms.ScrollableControl();
            this.Glitch = new System.Windows.Forms.TabPage();
            this.scrollableControl1 = new System.Windows.Forms.ScrollableControl();
            this.ColorDisplay = new System.Windows.Forms.PictureBox();
            this.ColorMenu = new System.Windows.Forms.PictureBox();
            this.Xoffset = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EffectOpacity = new System.Windows.Forms.TextBox();
            this.Yoffset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.customButtons2 = new Watermark_Empower.CustomButtons();
            this.customButtons1 = new Watermark_Empower.CustomButtons();
            this.Box = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.EffectSettingsControl.SuspendLayout();
            this.None.SuspendLayout();
            this.Glitch.SuspendLayout();
            this.scrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "None",
            "Glitch",
            "HardGlitch",
            "WhiteBox",
            "TransparentWhiteBox",
            "RoundedWhiteBox"});
            this.comboBox1.Location = new System.Drawing.Point(29, 112);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 31);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pictureBox1.Location = new System.Drawing.Point(-3, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(697, 43);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label1.Location = new System.Drawing.Point(60, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Effects list";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label2.Location = new System.Drawing.Point(398, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Preview";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label3.Location = new System.Drawing.Point(273, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "Effects dialog";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pictureBox2.Location = new System.Drawing.Point(244, 157);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(430, 230);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(260, 172);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(400, 200);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // EffectSettingsControl
            // 
            this.EffectSettingsControl.Controls.Add(this.None);
            this.EffectSettingsControl.Controls.Add(this.Glitch);
            this.EffectSettingsControl.Controls.Add(this.Box);
            this.EffectSettingsControl.Location = new System.Drawing.Point(29, 149);
            this.EffectSettingsControl.Name = "EffectSettingsControl";
            this.EffectSettingsControl.SelectedIndex = 0;
            this.EffectSettingsControl.Size = new System.Drawing.Size(199, 330);
            this.EffectSettingsControl.TabIndex = 6;
            // 
            // None
            // 
            this.None.BackColor = System.Drawing.Color.Black;
            this.None.Controls.Add(this.scrollableControl3);
            this.None.Location = new System.Drawing.Point(4, 25);
            this.None.Name = "None";
            this.None.Padding = new System.Windows.Forms.Padding(3);
            this.None.Size = new System.Drawing.Size(191, 301);
            this.None.TabIndex = 0;
            this.None.Text = "None";
            // 
            // scrollableControl3
            // 
            this.scrollableControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.scrollableControl3.Location = new System.Drawing.Point(-4, 0);
            this.scrollableControl3.Name = "scrollableControl3";
            this.scrollableControl3.Size = new System.Drawing.Size(195, 305);
            this.scrollableControl3.TabIndex = 3;
            this.scrollableControl3.Text = "scrollableControl3";
            // 
            // Glitch
            // 
            this.Glitch.BackColor = System.Drawing.Color.Black;
            this.Glitch.Controls.Add(this.scrollableControl1);
            this.Glitch.Location = new System.Drawing.Point(4, 25);
            this.Glitch.Name = "Glitch";
            this.Glitch.Padding = new System.Windows.Forms.Padding(3);
            this.Glitch.Size = new System.Drawing.Size(191, 301);
            this.Glitch.TabIndex = 1;
            this.Glitch.Text = "Glitch";
            // 
            // scrollableControl1
            // 
            this.scrollableControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.scrollableControl1.Controls.Add(this.ColorDisplay);
            this.scrollableControl1.Controls.Add(this.ColorMenu);
            this.scrollableControl1.Controls.Add(this.Xoffset);
            this.scrollableControl1.Controls.Add(this.label5);
            this.scrollableControl1.Controls.Add(this.EffectOpacity);
            this.scrollableControl1.Controls.Add(this.Yoffset);
            this.scrollableControl1.Controls.Add(this.label6);
            this.scrollableControl1.Controls.Add(this.label4);
            this.scrollableControl1.Location = new System.Drawing.Point(-2, 1);
            this.scrollableControl1.Name = "scrollableControl1";
            this.scrollableControl1.Size = new System.Drawing.Size(195, 299);
            this.scrollableControl1.TabIndex = 4;
            this.scrollableControl1.Text = "scrollableControl1";
            // 
            // ColorDisplay
            // 
            this.ColorDisplay.BackColor = System.Drawing.Color.White;
            this.ColorDisplay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ColorDisplay.Location = new System.Drawing.Point(85, 157);
            this.ColorDisplay.Name = "ColorDisplay";
            this.ColorDisplay.Size = new System.Drawing.Size(100, 50);
            this.ColorDisplay.TabIndex = 21;
            this.ColorDisplay.TabStop = false;
            // 
            // ColorMenu
            // 
            this.ColorMenu.Image = global::Watermark_Empower.Properties.Resources.colour;
            this.ColorMenu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ColorMenu.InitialImage = global::Watermark_Empower.Properties.Resources.colour;
            this.ColorMenu.Location = new System.Drawing.Point(13, 147);
            this.ColorMenu.Name = "ColorMenu";
            this.ColorMenu.Size = new System.Drawing.Size(67, 67);
            this.ColorMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ColorMenu.TabIndex = 20;
            this.ColorMenu.TabStop = false;
            this.ColorMenu.Click += new System.EventHandler(this.ColorMenu_Click);
            // 
            // Xoffset
            // 
            this.Xoffset.Font = new System.Drawing.Font("Source Code Pro", 10.2F);
            this.Xoffset.Location = new System.Drawing.Point(109, 14);
            this.Xoffset.Name = "Xoffset";
            this.Xoffset.Size = new System.Drawing.Size(78, 29);
            this.Xoffset.TabIndex = 14;
            this.Xoffset.Text = "2";
            this.Xoffset.TextChanged += new System.EventHandler(this.Xoffset_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 28);
            this.label5.TabIndex = 13;
            this.label5.Text = "X offset";
            // 
            // EffectOpacity
            // 
            this.EffectOpacity.Font = new System.Drawing.Font("Source Code Pro", 10.2F);
            this.EffectOpacity.Location = new System.Drawing.Point(110, 101);
            this.EffectOpacity.Name = "EffectOpacity";
            this.EffectOpacity.Size = new System.Drawing.Size(78, 29);
            this.EffectOpacity.TabIndex = 12;
            this.EffectOpacity.Text = "50";
            this.EffectOpacity.TextChanged += new System.EventHandler(this.Opacity_TextChanged);
            // 
            // Yoffset
            // 
            this.Yoffset.Font = new System.Drawing.Font("Source Code Pro", 10.2F);
            this.Yoffset.Location = new System.Drawing.Point(109, 55);
            this.Yoffset.Name = "Yoffset";
            this.Yoffset.Size = new System.Drawing.Size(78, 29);
            this.Yoffset.TabIndex = 12;
            this.Yoffset.Text = "2";
            this.Yoffset.TextChanged += new System.EventHandler(this.Yoffset_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(10, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 28);
            this.label6.TabIndex = 11;
            this.label6.Text = "Opacity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "Y offset";
            // 
            // customButtons2
            // 
            this.customButtons2.BackColor = System.Drawing.Color.LimeGreen;
            this.customButtons2.BackgroundColor = System.Drawing.Color.LimeGreen;
            this.customButtons2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButtons2.BorderRadius = 20;
            this.customButtons2.BorderSize = 0;
            this.customButtons2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.customButtons2.FlatAppearance.BorderSize = 0;
            this.customButtons2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtons2.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtons2.ForeColor = System.Drawing.Color.White;
            this.customButtons2.Location = new System.Drawing.Point(545, 451);
            this.customButtons2.Name = "customButtons2";
            this.customButtons2.Size = new System.Drawing.Size(115, 40);
            this.customButtons2.TabIndex = 5;
            this.customButtons2.Text = "Confirm";
            this.customButtons2.TextColor = System.Drawing.Color.White;
            this.customButtons2.UseVisualStyleBackColor = false;
            this.customButtons2.Click += new System.EventHandler(this.customButtons2_Click);
            // 
            // customButtons1
            // 
            this.customButtons1.BackColor = System.Drawing.Color.Crimson;
            this.customButtons1.BackgroundColor = System.Drawing.Color.Crimson;
            this.customButtons1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButtons1.BorderRadius = 20;
            this.customButtons1.BorderSize = 0;
            this.customButtons1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customButtons1.FlatAppearance.BorderSize = 0;
            this.customButtons1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtons1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtons1.ForeColor = System.Drawing.Color.White;
            this.customButtons1.Location = new System.Drawing.Point(260, 451);
            this.customButtons1.Name = "customButtons1";
            this.customButtons1.Size = new System.Drawing.Size(115, 40);
            this.customButtons1.TabIndex = 5;
            this.customButtons1.Text = "Cancel";
            this.customButtons1.TextColor = System.Drawing.Color.White;
            this.customButtons1.UseVisualStyleBackColor = false;
            this.customButtons1.Click += new System.EventHandler(this.customButtons1_Click);
            // 
            // Box
            // 
            this.Box.Location = new System.Drawing.Point(4, 25);
            this.Box.Name = "Box";
            this.Box.Padding = new System.Windows.Forms.Padding(3);
            this.Box.Size = new System.Drawing.Size(191, 301);
            this.Box.TabIndex = 2;
            this.Box.Text = "Box";
            this.Box.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(690, 503);
            this.Controls.Add(this.customButtons2);
            this.Controls.Add(this.customButtons1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.EffectSettingsControl);
            this.MaximumSize = new System.Drawing.Size(708, 550);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.EffectSettingsControl.ResumeLayout(false);
            this.None.ResumeLayout(false);
            this.Glitch.ResumeLayout(false);
            this.scrollableControl1.ResumeLayout(false);
            this.scrollableControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Watermark_Empower.CustomButtons customButtons1;
        private Watermark_Empower.CustomButtons customButtons2;
        private System.Windows.Forms.TabControl EffectSettingsControl;
        private System.Windows.Forms.TabPage None;
        private System.Windows.Forms.TabPage Glitch;
        private System.Windows.Forms.ScrollableControl scrollableControl3;
        private System.Windows.Forms.ScrollableControl scrollableControl1;
        private System.Windows.Forms.TextBox Xoffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Yoffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox ColorMenu;
        private System.Windows.Forms.PictureBox ColorDisplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox EffectOpacity;
        private System.Windows.Forms.TabPage Box;
    }
}