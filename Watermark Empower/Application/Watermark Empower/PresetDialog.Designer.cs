namespace Watermark_Empower
{
    partial class PresetDialog
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
            this.label3 = new System.Windows.Forms.Label();
            this.customButtons2 = new Watermark_Empower.CustomButtons();
            this.customButtons1 = new Watermark_Empower.CustomButtons();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RewritePreset = new MyCheckBox();
            this.PresetNameTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.customButtons3 = new Watermark_Empower.CustomButtons();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label3.Location = new System.Drawing.Point(163, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Preset dialog";
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
            this.customButtons2.Location = new System.Drawing.Point(285, 43);
            this.customButtons2.Name = "customButtons2";
            this.customButtons2.Size = new System.Drawing.Size(115, 40);
            this.customButtons2.TabIndex = 6;
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
            this.customButtons1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.customButtons1.FlatAppearance.BorderSize = 0;
            this.customButtons1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtons1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtons1.ForeColor = System.Drawing.Color.White;
            this.customButtons1.Location = new System.Drawing.Point(169, 545);
            this.customButtons1.Name = "customButtons1";
            this.customButtons1.Size = new System.Drawing.Size(115, 40);
            this.customButtons1.TabIndex = 7;
            this.customButtons1.Text = "Close";
            this.customButtons1.TextColor = System.Drawing.Color.White;
            this.customButtons1.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(484, 43);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RewritePreset);
            this.groupBox3.Controls.Add(this.PresetNameTxtBox);
            this.groupBox3.Controls.Add(this.customButtons2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(120)))), ((int)(((byte)(249)))));
            this.groupBox3.Location = new System.Drawing.Point(24, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(421, 128);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Autosave and import preset";
            // 
            // RewritePreset
            // 
            this.RewritePreset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RewritePreset.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RewritePreset.Location = new System.Drawing.Point(30, 82);
            this.RewritePreset.Name = "RewritePreset";
            this.RewritePreset.Size = new System.Drawing.Size(263, 31);
            this.RewritePreset.TabIndex = 23;
            this.RewritePreset.Text = "Rewrite existing preset";
            this.RewritePreset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RewritePreset.UseVisualStyleBackColor = true;
            // 
            // PresetNameTxtBox
            // 
            this.PresetNameTxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.PresetNameTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PresetNameTxtBox.Font = new System.Drawing.Font("Nirmala UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PresetNameTxtBox.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.PresetNameTxtBox.Location = new System.Drawing.Point(95, 46);
            this.PresetNameTxtBox.Name = "PresetNameTxtBox";
            this.PresetNameTxtBox.Size = new System.Drawing.Size(139, 30);
            this.PresetNameTxtBox.TabIndex = 11;
            this.PresetNameTxtBox.Text = "MyPreset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.label1.Location = new System.Drawing.Point(21, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.customButtons3);
            this.groupBox1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(120)))), ((int)(((byte)(249)))));
            this.groupBox1.Location = new System.Drawing.Point(24, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 99);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Delete imported preset";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Nirmala UI", 10.2F);
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(26, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(253, 31);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // customButtons3
            // 
            this.customButtons3.BackColor = System.Drawing.Color.Orange;
            this.customButtons3.BackgroundColor = System.Drawing.Color.Orange;
            this.customButtons3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButtons3.BorderRadius = 20;
            this.customButtons3.BorderSize = 0;
            this.customButtons3.FlatAppearance.BorderSize = 0;
            this.customButtons3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtons3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtons3.ForeColor = System.Drawing.Color.White;
            this.customButtons3.Location = new System.Drawing.Point(285, 40);
            this.customButtons3.Name = "customButtons3";
            this.customButtons3.Size = new System.Drawing.Size(115, 40);
            this.customButtons3.TabIndex = 6;
            this.customButtons3.Text = "Delete";
            this.customButtons3.TextColor = System.Drawing.Color.White;
            this.customButtons3.UseVisualStyleBackColor = false;
            this.customButtons3.Click += new System.EventHandler(this.customButtons3_Click);
            // 
            // PresetDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(467, 597);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.customButtons1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PresetDialog";
            this.Load += new System.EventHandler(this.PresetDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private CustomButtons customButtons2;
        private CustomButtons customButtons1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox PresetNameTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButtons customButtons3;
        private System.Windows.Forms.ComboBox comboBox1;
        private MyCheckBox RewritePreset;
    }
}