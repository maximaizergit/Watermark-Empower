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

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer,true);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            this.AutoSize = false;
            this.Width = 1410;
            this.Height = 775;
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

        private void NavMove(int height, int top, int left)//animation for nav menu
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
        //DESCRIBING VARIABLES
        string fileName = "tempinput.jpg";
        string filePath;
        int operation = 0;
        Options options = new Options();
        Generator gen = new Generator();
        bool isimported = false;
        //MAIN FORM FUNCTIONAL
        private void Import_Image_Click(object sender, EventArgs e)//import image and preparing for work with images
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Open the selected image file
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    Image image = Image.FromStream(fileStream);
                    // Load the image into the PictureBox control
                    image.Save(filePath);
                    MainDisplay.Image = Image.FromStream(fileStream);
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
                NavMove(FullfillSelector.Height, FullfillSelector.Top, FullfillSelector.Left);
                MainDisplay.Image.Dispose();
                if (!FullfillGradientcheckbox.Checked)
                {
                    if (operation == 0)
                    {
                        gen.GenPatternFullfill(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween,
                            options.Heightbetween, options.Fontstyle, options.Color, 0, options.Effect);
                        
                        MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                    }
                    else
                    {
                        gen.GenPatternFullfill(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween,
                            options.Heightbetween, options.Fontstyle, options.Color, 1, options.Effect);
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
                NavMove(ChessSelector.Height, ChessSelector.Top, ChessSelector.Left);
                
                MainDisplay.Image.Dispose();
                if (operation == 0)
                {
                    gen.GenPatternChess(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween,
                           options.Heightbetween, options.Fontstyle, options.Color, 0, options.Effect);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChess(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween,
                            options.Heightbetween, options.Fontstyle, options.Color, 1, options.Effect);
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
            NavMove(button1.Height, button1.Top, button1.Left);
            button1.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NavMove(button2.Height, button2.Top, button2.Left);
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


        //CLASSES

        //class with options for creating new images
        public class Options
        {
            string text = "Watermark";
            string fontname = "Arial";
            int fontsize = 48;
            int transparancy = 128;
            int angle = 25;
            int widthbetween = 0;
            int heightbetween = 0;
            FontStyle fontstyle = FontStyle.Regular;
            Color color = Color.White;
            Color color2 = Color.White;
            Color color3 = Color.White;
            int colorstart = 0;
            int colorend = 300;
            int colorangle = 50;
            string effect = "None";

            public string Fontname { get => fontname; set => fontname = value; }
            public int Fontsize { get => fontsize; set => fontsize = value; }
            public int Transparancy
            {
                get => transparancy;
                set
                {
                    double alpha = (double)value / 100 * 255;
                    transparancy = Convert.ToInt32(alpha);
                }
            }
            public int Angle { get => angle; set => angle = value; }
            public int Widthbetween { get => widthbetween; set => widthbetween = value; }
            public int Heightbetween { get => heightbetween; set => heightbetween = value; }
            public FontStyle Fontstyle { get => fontstyle; set => fontstyle = value; }
            public string Text { get => text; set => text = value; }
            public Color Color { get => color; set => color = value; }
            public Color Color2 { get => color2; set => color2 = value; }
            public Color Color3 { get => color3; set => color3 = value; }
            public int Colorstart { get => colorstart; set => colorstart = value; }
            public int Colorend { get => colorend; set => colorend = value; }
            public int Colorangle { get => colorangle; set => colorangle = value; }
            public string Effect { get => effect; set => effect = value; }
        }
        //WORK WITH FULLFILL PATTERN
        //Updating unused image. Disposing used image. Opening updated image 
        private void FullfillUpdate(string text, string fontname, int fontsize, int alpha, int angle, int widthbetween, int heightbetween, FontStyle fontstyle, Color color, Color color2, Color color3, int colorstart, int colorend, int colorangle, string effect)
        {
            MainDisplay.Image.Dispose();
            if (!FullfillGradientcheckbox.Checked)
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation(),effect);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation(),effect);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }
            else
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation(), color, color2, color3, colorstart, colorend, colorangle);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation(), color, color2, color3, colorstart, colorend, colorangle);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }

        }
              
        //Update image on fields change for fullfill 
        private void FullfillFont_TextChanged(object sender, EventArgs e)
        {
            options.Fontname = FullfillFont.Text;
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle, options.Effect);

        }
        
        private void FullfillText_TextChanged(object sender, EventArgs e)
        {
            options.Text = FullfillText.Text;
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle,options.Effect);
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
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle, options.Effect);
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
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, 
                    options.Colorangle, options.Effect);

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
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, 
                    options.Colorangle, options.Effect);
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
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                    options.Colorangle, options.Effect);
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
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, 
                    options.Colorangle, options.Effect);
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
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                   options.Colorangle, options.Effect);
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
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, 
                   options.Colorangle, options.Effect);
        }
        private void FullfillGradientUpd(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            options.Color = color;
            options.Color2 = color1;
            options.Color3 = color2;
            options.Colorstart = colorstart;
            options.Colorend = colorend;
            options.Colorangle = colorangle;
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, 
                   options.Colorangle, options.Effect);
        }



        private void FullfillGradientcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (FullfillGradientcheckbox.Checked)
            {
                options.Color = Color.Red;
                options.Color2 = Color.Blue;
                options.Color3 = Color.Green;
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                       , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                       options.Colorangle, options.Effect);
            }
            else
            {
                options.Color = Color.White;
                FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                      , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                      options.Colorangle, options.Effect);
            }
        }

        private void FullfillGradientStart_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillGradientStart.Text == "" || FullfillGradientStart.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(FullfillGradientStart.Text);
                    FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                          , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                          options.Colorangle, options.Effect);
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
        private void ChessUpdate(string text, string fontname, int fontsize, int alpha, int angle, int widthbetween, int heightbetween, FontStyle fontstyle, Color color, Color color2, Color color3, int colorstart, int colorend, int colorangle, string effect)
        {
            MainDisplay.Image.Dispose();
            if (!ChessGradientCheckBox.Checked)
            {
                if (operation == 0)
                {
                    gen.GenPatternChess(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation(), effect);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChess(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation(), effect);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }
            else
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation(), color, color2, color3, colorstart, colorend, colorangle);
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation(), color, color2, color3, colorstart, colorend, colorangle);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
            }

        }
        private void ChessText_TextChanged(object sender, EventArgs e)
        {
            options.Text = ChessText.Text;
            ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle, options.Effect);

        }

        private void ChessFont_TextChanged(object sender, EventArgs e)
        {
            options.Fontname = ChessFont.Text;
            ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle, options.Effect);

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
                ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend, options.Colorangle, options.Effect);
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
                ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                    options.Colorangle, options.Effect);

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
                ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                    options.Colorangle, options.Effect);
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
               ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                    options.Colorangle, options.Effect);
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
                ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                    , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                    options.Colorangle, options.Effect);
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
            ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                   options.Colorangle, options.Effect);
        }
        private void ChessGradientUpd(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            options.Color = color;
            options.Color2 = color1;
            options.Color3 = color2;
            options.Colorstart = colorstart;
            options.Colorend = colorend;
            options.Colorangle = colorangle;
            FullfillUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                   options.Colorangle, options.Effect);
        }
        private void ChessColorUpd(Color color)
        {

            options.Color = color;
            ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                   , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                   options.Colorangle, options.Effect);
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
               ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                       , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                       options.Colorangle, options.Effect);
            }
            else
            {
                options.Color = Color.White;
                ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                      , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                      options.Colorangle, options.Effect);
            }
        }

        private void ChessGradientStart_TextChanged(object sender, EventArgs e)
        {
            if (!(ChessGradientStart.Text == "" || ChessGradientStart.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(ChessGradientStart.Text);
                    ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                  , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                  options.Colorangle, options.Effect);
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
                    ChessUpdate(options.Text, options.Fontname, options.Fontsize, options.Transparancy, options.Angle, options.Widthbetween
                          , options.Heightbetween, options.Fontstyle, options.Color, options.Color2, options.Color3, options.Colorstart, options.Colorend,
                          options.Colorangle, options.Effect);
                }
            }
        }
    }



}
