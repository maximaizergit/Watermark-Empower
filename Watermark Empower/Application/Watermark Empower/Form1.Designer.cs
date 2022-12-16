namespace Watermark_Empower
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fullfill = new System.Windows.Forms.TabPage();
            this.scrollableControl1 = new System.Windows.Forms.ScrollableControl();
            this.button1 = new System.Windows.Forms.Button();
            this.chess = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.FullfillFont = new System.Windows.Forms.TextBox();
            this.FullfillFontSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FullfillTransparancy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FullfillAngle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FullfillText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Import_Image = new Watermark_Empower.CustomButtons();
            this.ChessSelector = new Watermark_Empower.CustomButtons();
            this.FullfillSelector = new Watermark_Empower.CustomButtons();
            this.Fullfillhbtw = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Fullfillwbtw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FullfillStyle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.fullfill.SuspendLayout();
            this.scrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(84, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1280, 720);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.fullfill);
            this.tabControl1.Controls.Add(this.chess);
            this.tabControl1.ItemSize = new System.Drawing.Size(1, 2);
            this.tabControl1.Location = new System.Drawing.Point(1550, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(350, 863);
            this.tabControl1.TabIndex = 1;
            // 
            // fullfill
            // 
            this.fullfill.AutoScroll = true;
            this.fullfill.Controls.Add(this.scrollableControl1);
            this.fullfill.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fullfill.Location = new System.Drawing.Point(4, 4);
            this.fullfill.Name = "fullfill";
            this.fullfill.Padding = new System.Windows.Forms.Padding(3);
            this.fullfill.Size = new System.Drawing.Size(340, 855);
            this.fullfill.TabIndex = 0;
            this.fullfill.Text = "fullfill";
            this.fullfill.UseVisualStyleBackColor = true;
            // 
            // scrollableControl1
            // 
            this.scrollableControl1.Controls.Add(this.FullfillStyle);
            this.scrollableControl1.Controls.Add(this.label8);
            this.scrollableControl1.Controls.Add(this.Fullfillhbtw);
            this.scrollableControl1.Controls.Add(this.label6);
            this.scrollableControl1.Controls.Add(this.Fullfillwbtw);
            this.scrollableControl1.Controls.Add(this.label7);
            this.scrollableControl1.Controls.Add(this.FullfillText);
            this.scrollableControl1.Controls.Add(this.label5);
            this.scrollableControl1.Controls.Add(this.FullfillAngle);
            this.scrollableControl1.Controls.Add(this.label4);
            this.scrollableControl1.Controls.Add(this.FullfillTransparancy);
            this.scrollableControl1.Controls.Add(this.label3);
            this.scrollableControl1.Controls.Add(this.FullfillFontSize);
            this.scrollableControl1.Controls.Add(this.label2);
            this.scrollableControl1.Controls.Add(this.FullfillFont);
            this.scrollableControl1.Controls.Add(this.label1);
            this.scrollableControl1.Controls.Add(this.button1);
            this.scrollableControl1.Location = new System.Drawing.Point(3, 3);
            this.scrollableControl1.Name = "scrollableControl1";
            this.scrollableControl1.Size = new System.Drawing.Size(326, 841);
            this.scrollableControl1.TabIndex = 0;
            this.scrollableControl1.Text = "scrollableControl1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(40, 566);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chess
            // 
            this.chess.Location = new System.Drawing.Point(4, 4);
            this.chess.Name = "chess";
            this.chess.Padding = new System.Windows.Forms.Padding(3);
            this.chess.Size = new System.Drawing.Size(340, 855);
            this.chess.TabIndex = 1;
            this.chess.Text = "chess";
            this.chess.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Шрифт";
            // 
            // FullfillFont
            // 
            this.FullfillFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillFont.Location = new System.Drawing.Point(113, 60);
            this.FullfillFont.Name = "FullfillFont";
            this.FullfillFont.Size = new System.Drawing.Size(179, 27);
            this.FullfillFont.TabIndex = 2;
            this.FullfillFont.Text = "Arial";
            this.FullfillFont.TextChanged += new System.EventHandler(this.FullfillFont_TextChanged);
            // 
            // FullfillFontSize
            // 
            this.FullfillFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillFontSize.Location = new System.Drawing.Point(113, 110);
            this.FullfillFontSize.Name = "FullfillFontSize";
            this.FullfillFontSize.Size = new System.Drawing.Size(179, 27);
            this.FullfillFontSize.TabIndex = 4;
            this.FullfillFontSize.Text = "48";
            this.FullfillFontSize.TextChanged += new System.EventHandler(this.FullfillFontSize_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Размер";
            // 
            // FullfillTransparancy
            // 
            this.FullfillTransparancy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillTransparancy.Location = new System.Drawing.Point(210, 159);
            this.FullfillTransparancy.Name = "FullfillTransparancy";
            this.FullfillTransparancy.Size = new System.Drawing.Size(82, 27);
            this.FullfillTransparancy.TabIndex = 6;
            this.FullfillTransparancy.Text = "50";
            this.FullfillTransparancy.TextChanged += new System.EventHandler(this.FullfillTransparancy_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Прозрачность %";
            // 
            // FullfillAngle
            // 
            this.FullfillAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillAngle.Location = new System.Drawing.Point(77, 211);
            this.FullfillAngle.Name = "FullfillAngle";
            this.FullfillAngle.Size = new System.Drawing.Size(215, 27);
            this.FullfillAngle.TabIndex = 8;
            this.FullfillAngle.Text = "25";
            this.FullfillAngle.TextChanged += new System.EventHandler(this.FullfillAngle_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Угол";
            // 
            // FullfillText
            // 
            this.FullfillText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillText.Location = new System.Drawing.Point(113, 19);
            this.FullfillText.Name = "FullfillText";
            this.FullfillText.Size = new System.Drawing.Size(179, 27);
            this.FullfillText.TabIndex = 10;
            this.FullfillText.Text = "Watermark";
            this.FullfillText.TextChanged += new System.EventHandler(this.FullfillText_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Текст";
            // 
            // Import_Image
            // 
            this.Import_Image.BackColor = System.Drawing.Color.Orange;
            this.Import_Image.BackgroundColor = System.Drawing.Color.Orange;
            this.Import_Image.BorderColor = System.Drawing.Color.Red;
            this.Import_Image.BorderRadius = 20;
            this.Import_Image.BorderSize = 2;
            this.Import_Image.FlatAppearance.BorderSize = 0;
            this.Import_Image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Import_Image.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Import_Image.ForeColor = System.Drawing.Color.Black;
            this.Import_Image.Location = new System.Drawing.Point(1428, 26);
            this.Import_Image.Name = "Import_Image";
            this.Import_Image.Size = new System.Drawing.Size(93, 50);
            this.Import_Image.TabIndex = 5;
            this.Import_Image.Text = "Import";
            this.Import_Image.TextColor = System.Drawing.Color.Black;
            this.Import_Image.UseVisualStyleBackColor = false;
            this.Import_Image.Click += new System.EventHandler(this.Import_Image_Click);
            // 
            // ChessSelector
            // 
            this.ChessSelector.BackColor = System.Drawing.Color.Turquoise;
            this.ChessSelector.BackgroundColor = System.Drawing.Color.Turquoise;
            this.ChessSelector.BorderColor = System.Drawing.Color.Blue;
            this.ChessSelector.BorderRadius = 20;
            this.ChessSelector.BorderSize = 2;
            this.ChessSelector.FlatAppearance.BorderSize = 0;
            this.ChessSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChessSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChessSelector.ForeColor = System.Drawing.Color.Black;
            this.ChessSelector.Location = new System.Drawing.Point(1428, 176);
            this.ChessSelector.Name = "ChessSelector";
            this.ChessSelector.Size = new System.Drawing.Size(93, 50);
            this.ChessSelector.TabIndex = 4;
            this.ChessSelector.Text = "Chess";
            this.ChessSelector.TextColor = System.Drawing.Color.Black;
            this.ChessSelector.UseVisualStyleBackColor = false;
            this.ChessSelector.Click += new System.EventHandler(this.ChessSelector_Click);
            // 
            // FullfillSelector
            // 
            this.FullfillSelector.BackColor = System.Drawing.Color.Turquoise;
            this.FullfillSelector.BackgroundColor = System.Drawing.Color.Turquoise;
            this.FullfillSelector.BorderColor = System.Drawing.Color.Blue;
            this.FullfillSelector.BorderRadius = 20;
            this.FullfillSelector.BorderSize = 2;
            this.FullfillSelector.FlatAppearance.BorderSize = 0;
            this.FullfillSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FullfillSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillSelector.ForeColor = System.Drawing.Color.Black;
            this.FullfillSelector.Location = new System.Drawing.Point(1428, 101);
            this.FullfillSelector.Name = "FullfillSelector";
            this.FullfillSelector.Size = new System.Drawing.Size(93, 50);
            this.FullfillSelector.TabIndex = 3;
            this.FullfillSelector.Text = "FullFill";
            this.FullfillSelector.TextColor = System.Drawing.Color.Black;
            this.FullfillSelector.UseVisualStyleBackColor = false;
            this.FullfillSelector.Click += new System.EventHandler(this.FullfillSelector_Click);
            // 
            // Fullfillhbtw
            // 
            this.Fullfillhbtw.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Fullfillhbtw.Location = new System.Drawing.Point(17, 397);
            this.Fullfillhbtw.Name = "Fullfillhbtw";
            this.Fullfillhbtw.Size = new System.Drawing.Size(275, 27);
            this.Fullfillhbtw.TabIndex = 14;
            this.Fullfillhbtw.Text = "0";
            this.Fullfillhbtw.TextChanged += new System.EventHandler(this.Fullfillhbtw_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(260, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Высота между текстом";
            // 
            // Fullfillwbtw
            // 
            this.Fullfillwbtw.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Fullfillwbtw.Location = new System.Drawing.Point(17, 312);
            this.Fullfillwbtw.Name = "Fullfillwbtw";
            this.Fullfillwbtw.Size = new System.Drawing.Size(275, 27);
            this.Fullfillwbtw.TabIndex = 12;
            this.Fullfillwbtw.Text = "0";
            this.Fullfillwbtw.TextChanged += new System.EventHandler(this.Fontfillwbtw_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(12, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(261, 25);
            this.label7.TabIndex = 11;
            this.label7.Text = "Ширина между текстом";
            // 
            // FullfillStyle
            // 
            this.FullfillStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullfillStyle.Location = new System.Drawing.Point(17, 490);
            this.FullfillStyle.Name = "FullfillStyle";
            this.FullfillStyle.Size = new System.Drawing.Size(275, 27);
            this.FullfillStyle.TabIndex = 16;
            this.FullfillStyle.Text = "Regular";
            this.FullfillStyle.TextChanged += new System.EventHandler(this.FullfillStyle_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(12, 449);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(152, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "Стиль текста";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1902, 861);
            this.Controls.Add(this.Import_Image);
            this.Controls.Add(this.ChessSelector);
            this.Controls.Add(this.FullfillSelector);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.fullfill.ResumeLayout(false);
            this.scrollableControl1.ResumeLayout(false);
            this.scrollableControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage fullfill;
        private System.Windows.Forms.TabPage chess;
        private CustomButtons FullfillSelector;
        private CustomButtons ChessSelector;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private CustomButtons Import_Image;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ScrollableControl scrollableControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox FullfillFontSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FullfillFont;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FullfillAngle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FullfillTransparancy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FullfillText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Fullfillhbtw;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Fullfillwbtw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox FullfillStyle;
        private System.Windows.Forms.Label label8;
    }
}

