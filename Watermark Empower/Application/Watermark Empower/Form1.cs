using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WatermarkGenerator;
using System.Drawing.Imaging;
using System.Reflection;
using System.Threading;
using EffectDialog;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Watermark_Empower
{
    public partial class Form1 : Form
    {
        //VISUAL AND FORM SETTINGS
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        private bool _isDragging = false;
        private Point _lastCursorPosition;

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           //Preparing visual for form
            DoubleBuffered = true;
            foreach (string temp in tempfiles){
                if (File.Exists(temp))
                {

                    File.Delete(temp);

                }
            }
            
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            this.AutoSize = false;
            this.Width = 1410;
            this.Height = 820;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox1.Region = new Region(gp);

            int hgt = Tools.Height;
            Tools.Height = this.Height + 60;
            panel1.Height = Tools.Height;
            if (hgt < Tools.Height)
            {
                scrollableControl1.Height = Tools.Height - 20;
                scrollableControl3.Height = Tools.Height - 20;

            }
            Tools.SelectTab("None");
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start dragging the form
                _isDragging = true;
                _lastCursorPosition = Cursor.Position;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Stop dragging the form
                _isDragging = false;
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the difference between the current and last cursor positions
                Point delta = new Point(Cursor.Position.X - _lastCursorPosition.X, Cursor.Position.Y - _lastCursorPosition.Y);

                // Update the position of the form
                this.Location = new Point(this.Location.X + delta.X, this.Location.Y + delta.Y);

                // Update the last cursor position
                _lastCursorPosition = Cursor.Position;
            }
        }
        //making menu height always equal to form height
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int hgt = Tools.Height;
            Tools.Height = this.Height - 29;
            navpanelborder.Height = this.Height;
            if (hgt < Tools.Height)
            {
                scrollableControl1.Height = Tools.Height - 20;

            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MainDisplay.Image != null) { MainDisplay.Image.Dispose(); MainDisplay.Image = null; }
           
           
            foreach (string temp in tempfiles)
            {
                if (File.Exists(temp))
                {

                    File.Delete(temp);

                }
            }
        }

        private void NavMove( int top, int left)//animation for nav menu
        {

            //Nav.Height = height;
            Nav.Left = left;
            int topmove = (Nav.Top - top) / 10;
            for (int i = 0; i < 10; i++)
            {

                Nav.Top = Nav.Top - topmove;
                Nav.Refresh();
                Thread.Sleep(10);
            }
            Nav.Top = top;

        }
        private void SizingNavMove( int left)//animation for nav menu
        {

           
            
            int leftmove = (sizingnav.Left - left) / 10;
            for (int i = 0; i < 10; i++)
            {

                sizingnav.Left = sizingnav.Left - leftmove;
                Nav.Refresh();
                Thread.Sleep(10);
            }
            sizingnav.Left = left;

        }
        //DESCRIBING VARIABLES
        string fileName = "tempinput.jpg";
        List<string> tempfiles = new List<string>() { "input.jpg","tempinput.jpg","tempinput2.jpg"};
        string filePath;
        int operation = 0;
        Generator.Options options = new Generator.Options();
        Generator.ProjectSettings settings = new Generator.ProjectSettings();
        Generator gen = new Generator();
        bool isimported = false;
        //MAIN FORM FUNCTIONAL
        private void Import_Image_Click(object sender, EventArgs e)//import image and preparing for work with images
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
           
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(fileName))
                {
                    MainDisplay.Image.Dispose();

                    MainDisplay.Image = null;
                    File.Delete(fileName);

                }
                // Open the selected image file
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    Image image = Image.FromStream(fileStream);
                    // Load the image into the PictureBox control
                    NavMove( MenuButton.Top, 0);
                    Tools.SelectTab("none");
                    MainDisplay.Image = image;
                    string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    // Save the image to the executable directory with the specified name
                   
                    MainDisplay.Image.Save(Path.Combine(executableDirectory, "tempinput.jpg"), ImageFormat.Jpeg);
                    MainDisplay.Image.Save(Path.Combine(executableDirectory, "input.jpg"), ImageFormat.Jpeg);
                    isimported = true;

                }
            }
        }
        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //nav buttons
        private void FullfillSelector_Click(object sender, EventArgs e)//opening Fullfill options menu and creating image with defoult options
        {
            if (isimported)
            {
                
                Tools.SelectTab("fullfill");
                FullfillText.Text = options.Text;
                FullfillFont.Text = options.Fontname;
                FullfillFontSize.Text = options.Fontsize.ToString();
                FullfillAngle.Text = options.Angle.ToString();
                double transparancy = (Convert.ToDouble(options.Transparancy) / 255*100 );
                
                FullfillTransparancy.Text = Math.Round(transparancy,0).ToString();
                Fullfillwbtw.Text = options.Widthbetween.ToString();
                Fullfillhbtw.Text = options.Heightbetween.ToString();
                FullfillStyle.Text = options.Fontstyle.ToString();
                FullfillColorDisplay.BackColor = options.Color;
                FullfillGradientStart.Text= options.Colorstart.ToString();
                FullfillGradientEnd.Text = options.Colorend.ToString();
                FullfillGradientAngle.Text = options.Angle.ToString();
                FullfillEffectTextBox.Text = options.Effect.ToString();
                FullfillGradientcheckbox.Checked = false;
                NavMove( FullfillSelector.Top, FullfillSelector.Left);
                MainDisplay.Image.Dispose();
                if (!FullfillGradientcheckbox.Checked)
                {
                    if (operation == 0)
                    {
                        gen.GenPatternFullfill( 0, options, settings);
                        
                        MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                    }
                    else
                    {
                        gen.GenPatternFullfill( 1, options, settings);
                        MainDisplay.Image = Image.FromFile("tempinput.jpg");
                    }

                }
                else
                {
                    //TODO defoult for gradient

                }

            }
            else
            {
                MessageBox.Show("Сначала импортируйте изображение!");
            }

        }

        private void ChessSelector_Click(object sender, EventArgs e)
        {

            if (isimported)
            {
                
                Tools.SelectTab("chess");
                ChessText.Text = options.Text;
                ChessFont.Text = options.Fontname;
                ChessTextSize.Text = options.Fontsize.ToString();
                ChessAngle.Text = options.Angle.ToString();
                double transparancy = (Convert.ToDouble(options.Transparancy) / 255 * 100);

                ChessTransparancy.Text = Math.Round(transparancy, 0).ToString();
                ChessWbtw.Text = options.Widthbetween.ToString();
                ChessHbtw.Text = options.Heightbetween.ToString();
                ChessTextStyle.Text = options.Fontstyle.ToString();
                ChessColorDisplay.BackColor = options.Color;
                ChessGradientStart.Text = options.Colorstart.ToString();
                ChessGradientEnd.Text = options.Colorend.ToString();
                ChessGradientAngle.Text = options.Angle.ToString();
                ChessEffect.Text = options.Effect.ToString();
                ChessGradientCheckBox.Checked = false;
                NavMove( ChessSelector.Top, ChessSelector.Left);
                
                MainDisplay.Image.Dispose();
                if (operation == 0)
                {
                    gen.GenPatternChess( 0, options);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChess(1, options);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }
            else
            {
                MessageBox.Show("Сначала импортируйте изображение!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            NavMove( button1.Top, button1.Left);
            button1.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavMove( button2.Top, button2.Left);
            button2.BackColor = Color.FromArgb(46, 51, 73);
        }
        //TODO remove color change
        private void FullfillSelector_Leave(object sender, EventArgs e)
        {
            FullfillSelector.BackColor = Color.FromArgb(24, 30, 53);
        }

        private void ChessSelector_Leave(object sender, EventArgs e)
        {
            ChessSelector.BackColor = Color.FromArgb(24, 30, 53);
        }
        //switch for selecting unused image
        private int newoperation()
        {
            if (operation == 0)
            {
                int res = 0;
                operation++;
                return res;
            }
            else
            {
                int res = 1;
                operation--;
                return res;
            }
        }
        private Color colorupd()
        {
            ColorDialog colorDialog = new ColorDialog();

            // Allow the user to enter custom colors
            colorDialog.AllowFullOpen = true;

            // Set the initially selected color to red

            Color selectedColor = colorDialog.Color;

            // Only allow the user to select solid colors
            colorDialog.SolidColorOnly = true;

            // Set the custom colors that the user can select from
            colorDialog.CustomColors = new int[] { ColorTranslator.ToOle(Color.AliceBlue), ColorTranslator.ToOle(Color.AntiqueWhite) };

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // The user selected a color and clicked OK
                selectedColor = colorDialog.Color;

            }
            return selectedColor;
        }


        
        //WORK WITH FULLFILL PATTERN
        //Updating unused image. Disposing used image. Opening updated image 
        private void FullfillUpdate()
        {
            MainDisplay.Image.Dispose();
            if (!FullfillGradientcheckbox.Checked)
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfill( newoperation(),options,settings);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfill(newoperation(),options,settings);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }
            else
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfillGradient( newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfillGradient(newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }

        }
              
        //Update image on fields change for fullfill 
        private void FullfillFont_TextChanged(object sender, EventArgs e)
        {
            options.Fontname = FullfillFont.Text;
            FullfillUpdate();

        }
        
        private void FullfillText_TextChanged(object sender, EventArgs e)
        {
            options.Text = FullfillText.Text;
            FullfillUpdate();
        }

        private void FullfillFontSize_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillFontSize.Text == ""|| Convert.ToInt32(FullfillFontSize.Text)<=7))
            {
                try
                {
                    if (Convert.ToInt32(FullfillFontSize.Text) < 0 || Convert.ToInt32(FullfillFontSize.Text) > 10000)
                    {
                        throw new Exception("Размер шрифта вышел за диапазон");
                    }
                    options.Fontsize = Convert.ToInt32(FullfillFontSize.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate();
            }

        }

        private void FullfillTransparancy_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillTransparancy.Text == ""))
            {
                try
                {
                    if (Convert.ToInt32(FullfillTransparancy.Text) < 0 || Convert.ToInt32(FullfillTransparancy.Text) > 100)
                    {
                        throw new Exception("Прозрачность вышла за пределы диапазона");
                    }
                    options.Transparancy = Convert.ToInt32(FullfillTransparancy.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                FullfillUpdate();

            }

        }

        private void FullfillAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillAngle.Text == "" || FullfillAngle.Text == "-"))
            {
                try
                {

                    options.Angle = Convert.ToInt32(FullfillAngle.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate();
            }

        }

        private void Fontfillwbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(Fullfillwbtw.Text == "" || Fullfillwbtw.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float width = size.Width - 5;



                        if (Convert.ToInt32(Fullfillwbtw.Text) < -width)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(width, 0) + " недопустимы для данного размера текста");
                        }
                        options.Widthbetween = Convert.ToInt32(Fullfillwbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate();
            }
        }

        private void Fullfillhbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(Fullfillhbtw.Text == "" || Fullfillhbtw.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float height = size.Height - 5;



                        if (Convert.ToInt32(Fullfillhbtw.Text) < -height)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(height, 0) + " недопустимы для данного размера текста");
                        }
                        options.Heightbetween = Convert.ToInt32(Fullfillhbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate();
            }
        }

        private void FullfillStyle_TextChanged(object sender, EventArgs e)
        {

            switch (FullfillStyle.Text.ToLower())
            {

                case "regular":
                    options.Fontstyle = FontStyle.Regular;
                    break;

                case "bold":
                    options.Fontstyle = FontStyle.Bold;
                    break;

                case "italic":
                    options.Fontstyle = FontStyle.Italic;
                    break;

                case "underline":
                    options.Fontstyle = FontStyle.Underline;
                    break;
                default:

                    break;
            }
            FullfillUpdate();
        }
            

        private void FullfillColorMenu_Click(object sender, EventArgs e)
        {
            if (!FullfillGradientcheckbox.Checked)
            {
                FullfillColorDisplay.BackColor = colorupd();
                FullfillColorUpd(FullfillColorDisplay.BackColor);
            }
            else
            {
                Color color1 = colorupd();
                Color color2 = colorupd();
                Color color3 = colorupd();
                ColorBlend blend = new ColorBlend();

                blend.Positions = new float[] { 0, 0.5f, 1 };
                blend.Colors = new Color[] { color1, color2, color3 };
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(40, 67), Color.Red, Color.Blue);
                gradientBrush.InterpolationColors = blend;

                FullfillColorDisplay.CreateGraphics().FillRectangle(gradientBrush, pictureBox1.ClientRectangle);
                FullfillGradientUpd(color1, color2, color3, options.Colorstart, options.Colorend, options.Colorangle);
            }

        }

        private void FullfillColorDisplay_Click(object sender, EventArgs e)
        {
            if (!FullfillGradientcheckbox.Checked)
            {
                FullfillColorDisplay.BackColor = colorupd();
                FullfillColorUpd(FullfillColorDisplay.BackColor);
            }
            else
            {
                Color color1 = colorupd();
                Color color2 = colorupd();
                Color color3 = colorupd();
                ColorBlend blend = new ColorBlend();




                blend.Positions = new float[] { 0, 0.5f, 1 };
                blend.Colors = new Color[] { color1, color2, color3 };
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(40, 67), Color.Red, Color.Blue);
                gradientBrush.InterpolationColors = blend;

                FullfillColorDisplay.CreateGraphics().FillRectangle(gradientBrush, pictureBox1.ClientRectangle);
                FullfillGradientUpd(color1, color2, color3, options.Colorstart, options.Colorend, options.Colorangle);
            }
        }
        private void FullfillColorUpd(Color color)
        {

            options.Color = color;
            FullfillUpdate();
        }
        private void FullfillGradientUpd(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            options.Color = color;
            options.Color2 = color1;
            options.Color3 = color2;
            options.Colorstart = colorstart;
            options.Colorend = colorend;
            options.Colorangle = colorangle;
            FullfillUpdate();
        }



        private void FullfillGradientcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (FullfillGradientcheckbox.Checked)
            {
                options.Color = Color.Red;
                options.Color2 = Color.Blue;
                options.Color3 = Color.Green;
                FullfillUpdate();
            }
            else
            {
                options.Color = Color.White;
                FullfillUpdate();
            }
        }

        private void FullfillGradientStart_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillGradientStart.Text == "" || FullfillGradientStart.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(FullfillGradientStart.Text);
                    FullfillUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FullfillGradientEnd_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillGradientEnd.Text == "" || FullfillGradientEnd.Text == "-"))
            {
                try
                {
                    options.Colorend = Convert.ToInt32(FullfillGradientEnd.Text);
                    FullfillUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void FullfillGradientAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillGradientAngle.Text == "" || FullfillGradientAngle.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(FullfillGradientAngle.Text);
                    FullfillUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        
        private void EffectDialogButton_Click(object sender, EventArgs e)
        {
            using (Form2 effectdialog = new Form2())
            {
                if (effectdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FullfillEffectTextBox.Text = effectdialog.SelectedEffect;
                    options.Effect = effectdialog.SelectedEffect;
                    FullfillUpdate();
                }
            }
        }

       private void FullfillFontSelectBtn_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            // Set the font selection options
            fontDialog.AllowScriptChange = true;
            fontDialog.ShowEffects = true;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the font of the selected control
                FullfillFont.Text = fontDialog.Font.Name.ToString();
                options.Fontname = fontDialog.Font.Name.ToString();
            }
        }

        //WORK WITH CHESS PATTERN
        private void ChessUpdate()
        {
            MainDisplay.Image.Dispose();
            if (!ChessGradientCheckBox.Checked)
            {
                if (operation == 0)
                {
                    gen.GenPatternChess( newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChess( newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }
            else
            {
                if (operation == 0)
                {
                    gen.GenPatternChessGradient( newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChessGradient(newoperation(), options);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }

        }
        private void ChessText_TextChanged(object sender, EventArgs e)
        {
            options.Text = ChessText.Text;
            ChessUpdate();

        }

        private void ChessFont_TextChanged(object sender, EventArgs e)
        {
            options.Fontname = ChessFont.Text;
            ChessUpdate();

        }

        private void ChessFontBtn_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            // Set the font selection options
            fontDialog.AllowScriptChange = true;
            fontDialog.ShowEffects = true;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the font of the selected control
                ChessFont.Text = fontDialog.Font.Name.ToString();
                options.Fontname = fontDialog.Font.Name.ToString();
            }
        }

        private void ChessTextSize_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessTextSize.Text == "" || Convert.ToInt32(ChessTextSize.Text) <= 7))
            {
                try
                {
                    if (Convert.ToInt32(ChessTextSize.Text) < 0 || Convert.ToInt32(ChessTextSize.Text) > 10000)
                    {
                        throw new Exception("Размер шрифта вышел за диапазон");
                    }
                    options.Fontsize = Convert.ToInt32(ChessTextSize.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ChessUpdate();
            }
        }

        private void ChessTransparancy_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessTransparancy.Text == ""))
            {
                try
                {
                    if (Convert.ToInt32(ChessTransparancy.Text) < 0 || Convert.ToInt32(ChessTransparancy.Text) > 100)
                    {
                        throw new Exception("Прозрачность вышла за пределы диапазона");
                    }
                    options.Transparancy = Convert.ToInt32(ChessTransparancy.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                ChessUpdate();

            }
        }

        private void ChessAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessAngle.Text == "" || ChessAngle.Text == "-"))
            {
                try
                {

                    options.Angle = Convert.ToInt32(ChessAngle.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ChessUpdate();
            }
        }

        private void ChessWbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessWbtw.Text == "" || ChessWbtw.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float width = size.Width - 5;



                        if (Convert.ToInt32(ChessWbtw.Text) < -width)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(width, 0) + " недопустимы для данного размера текста");
                        }
                        options.Widthbetween = Convert.ToInt32(ChessWbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               ChessUpdate();
            }
        }

        private void ChessHbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessHbtw.Text == "" || ChessHbtw.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float height = size.Height - 5;



                        if (Convert.ToInt32(ChessHbtw.Text) < -height)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(height, 0) + " недопустимы для данного размера текста");
                        }
                        options.Heightbetween = Convert.ToInt32(ChessHbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ChessUpdate();
            }
        }

        private void ChessTextStyle_TextChanged(object sender, EventArgs e)
        {
            switch (ChessTextStyle.Text.ToLower())
            {

                case "regular":
                    options.Fontstyle = FontStyle.Regular;
                    break;

                case "bold":
                    options.Fontstyle = FontStyle.Bold;
                    break;

                case "italic":
                    options.Fontstyle = FontStyle.Italic;
                    break;

                case "underline":
                    options.Fontstyle = FontStyle.Underline;
                    break;
                default:

                    break;
            }
            ChessUpdate();
        }
        private void ChessGradientUpd(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            options.Color = color;
            options.Color2 = color1;
            options.Color3 = color2;
            options.Colorstart = colorstart;
            options.Colorend = colorend;
            options.Colorangle = colorangle;
            FullfillUpdate();
        }
        private void ChessColorUpd(Color color)
        {

            options.Color = color;
            ChessUpdate();
        }
        private void ChessColorIcon_Click(object sender, EventArgs e)
        {
            if (!ChessGradientCheckBox.Checked)
            {
                ChessColorDisplay.BackColor = colorupd();
                ChessColorUpd(ChessColorDisplay.BackColor);
            }
            else
            {
                Color color1 = colorupd();
                Color color2 = colorupd();
                Color color3 = colorupd();
                ColorBlend blend = new ColorBlend();




                blend.Positions = new float[] { 0, 0.5f, 1 };
                blend.Colors = new Color[] { color1, color2, color3 };
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(40, 67), Color.Red, Color.Blue);
                gradientBrush.InterpolationColors = blend;

                ChessColorDisplay.CreateGraphics().FillRectangle(gradientBrush, pictureBox1.ClientRectangle);
                ChessGradientUpd(color1, color2, color3, options.Colorstart, options.Colorend, options.Colorangle);
            }
        }

        private void ChessColorDisplay_Click(object sender, EventArgs e)
        {
            if (!ChessGradientCheckBox.Checked)
            {
                ChessColorDisplay.BackColor = colorupd();
                ChessColorUpd(ChessColorDisplay.BackColor);
            }
            else
            {
                Color color1 = colorupd();
                Color color2 = colorupd();
                Color color3 = colorupd();
                ColorBlend blend = new ColorBlend();




                blend.Positions = new float[] { 0, 0.5f, 1 };
                blend.Colors = new Color[] { color1, color2, color3 };
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(40, 67), Color.Red, Color.Blue);
                gradientBrush.InterpolationColors = blend;

                ChessColorDisplay.CreateGraphics().FillRectangle(gradientBrush, pictureBox1.ClientRectangle);
                ChessGradientUpd(color1, color2, color3, options.Colorstart, options.Colorend, options.Colorangle);
            }
        }

        private void ChessGradientCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ChessGradientCheckBox.Checked)
            {
                options.Color = Color.Red;
                options.Color2 = Color.Blue;
                options.Color3 = Color.Green;
               ChessUpdate();
            }
            else
            {
                options.Color = Color.White;
                ChessUpdate();
            }
        }

        private void ChessGradientStart_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessGradientStart.Text == "" || ChessGradientStart.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(ChessGradientStart.Text);
                    ChessUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ChessGradientEnd_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessGradientEnd.Text == "" || ChessGradientEnd.Text == "-"))
            {
                try
                {
                    options.Colorend = Convert.ToInt32(ChessGradientEnd.Text);
                    ChessUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ChessGradientAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessGradientAngle.Text == "" || ChessGradientAngle.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(ChessGradientAngle.Text);
                    ChessUpdate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ChessEffectBtn_Click(object sender, EventArgs e)
        {
            using (Form2 effectdialog = new Form2())
            {
                if (effectdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ChessEffectBtn.Text = effectdialog.SelectedEffect;
                    options.Effect = effectdialog.SelectedEffect;
                    ChessUpdate();
                }
            }
        }

        private void StretchButton_Click(object sender, EventArgs e)
        {
            
            MainDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(StretchButton.Left);
        }

        private void NormalSizeButton_Click(object sender, EventArgs e)
        {
            
            MainDisplay.SizeMode = PictureBoxSizeMode.Normal;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(NormalSizeButton.Left);
        }

        private void AutosizeButton_Click(object sender, EventArgs e)
        {
            MainDisplay.SizeMode = PictureBoxSizeMode.AutoSize;
            SizingNavMove(AutosizeButton.Left);
            if (MainDisplay.Width >960 || MainDisplay.Height > 585)
            {
                
                MainDisplay.SizeMode = PictureBoxSizeMode.Normal;
                MainDisplay.Width = 960;
                MainDisplay.Height = 585;
                MessageBox.Show("Размер изображения превосходит возможный для работы размер, выберите другой способ размежения картинки");
                SizingNavMove(NormalSizeButton.Left);
            }
          
        }

        private void CenterImageButton_Click(object sender, EventArgs e)
        {
            
            MainDisplay.SizeMode = PictureBoxSizeMode.CenterImage;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(CenterImageButton.Left);
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            
            MainDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(ZoomButton.Left);
        }

        private void ProjectOptions_Click(object sender, EventArgs e)
        {
            using (ProjectSettingsDialog prjset = new ProjectSettingsDialog(settings.Rows,settings.Columns,settings.Xoffset, settings.Yoffset))
            {
                if (prjset.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    settings.Rows = prjset.rows;
                    settings.Columns = prjset.columns;
                    settings.Xoffset = prjset.xoffset;
                    settings.Yoffset = prjset.yoffset;
                   
                    FullfillUpdate();
                }
            }
        }
    }



}
