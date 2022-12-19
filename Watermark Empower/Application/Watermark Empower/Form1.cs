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

using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Watermark_Empower
{
    public partial class Form1 : Form
    {
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
        string fileName = "tempinput.jpg";
        string filePath;
        int operation = 0;
        FullfillOptions fullfillopt = new FullfillOptions();
        Generator gen = new Generator();
        private bool _isDragging = false;
        private Point _lastCursorPosition;
        bool isimported = false;

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer,
                        true);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            this.AutoSize = false;
            this.Width = 1410;
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
            int hgt = tabControl1.Height;
            tabControl1.Height = this.Height + 60;
            panel1.Height = tabControl1.Height;

            if (hgt < tabControl1.Height)
            {
                scrollableControl1.Height = tabControl1.Height - 20;

            }
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
        private void NavMove(int height, int top, int left)
        {

            Nav.Height = height;
            Nav.Left = left;
            int topmove = (Nav.Top - top) / 10;
            for (int i = 0; i <= 10; i++)
            {

                Nav.Top = Nav.Top - topmove;

                Thread.Sleep(10);
            }
            Nav.Top = top;



        }
        //opening Fullfill options menu and creating image with defoult options

        private void FullfillSelector_Click(object sender, EventArgs e)
        {
            if (isimported)
            {
                NavMove(FullfillSelector.Height, FullfillSelector.Top, FullfillSelector.Left);
                FullfillSelector.BackColor = Color.FromArgb(46, 51, 73);
                tabControl1.SelectTab("fullfill");

                MainDisplay.Image.Dispose();
                if (!myCheckBox1.Checked)
                {
                    if (operation == 0)
                    {
                        gen.GenPatternFullfill("Watermark", newoperation());
                        MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                    }
                    else
                    {
                        gen.GenPatternFullfill("Watermark", newoperation());
                        MainDisplay.Image = Image.FromFile("tempinput.jpg");
                    }

                }
                else
                {


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
                NavMove(ChessSelector.Height, ChessSelector.Top, ChessSelector.Left);
                ChessSelector.BackColor = Color.FromArgb(46, 51, 73);
                tabControl1.SelectTab("chess");
                MainDisplay.Image.Dispose();
                if (operation == 0)
                {
                    gen.GenPatternChess("Watermark", newoperation());
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternChess("Watermark", newoperation());
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
        private void FullfillSelector_Leave(object sender, EventArgs e)
        {
            FullfillSelector.BackColor = Color.FromArgb(24, 30, 53);
        }

        private void ChessSelector_Leave(object sender, EventArgs e)
        {
            ChessSelector.BackColor = Color.FromArgb(24, 30, 53);
        }
        //Updating unused image. Disposing used image. Opening updated image 
        private void FullfillUpdate(string text, string fontname, int fontsize, int alpha, int angle, int widthbetween, int heightbetween, FontStyle fontstyle, Color color, Color color2, Color color3, int colorstart, int colorend, int colorangle)
        {
            MainDisplay.Image.Dispose();
            if (!myCheckBox1.Checked)
            {
                if (operation == 0)
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation());
                    MainDisplay.Image = Image.FromFile("tempinput2.jpg");
                }
                else
                {
                    gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, color, newoperation());
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
        //switch for selecting unused
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
        //opening chess options menu and creating image with defoult options


        //making menu height always equal to form height
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int hgt = tabControl1.Height;
            tabControl1.Height = this.Height - 29;

            if (hgt < tabControl1.Height)
            {
                scrollableControl1.Height = tabControl1.Height - 20;

            }
        }

        //class with options for creating new images
        public class FullfillOptions
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
        }
        //Update image on fields change for fullfill BEGIN
        private void FullfillFont_TextChanged(object sender, EventArgs e)
        {

            fullfillopt.Fontname = FullfillFont.Text;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);

        }

        private void FullfillText_TextChanged(object sender, EventArgs e)
        {
            fullfillopt.Text = FullfillText.Text;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
        }

        private void FullfillFontSize_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillFontSize.Text == ""))
            {
                try
                {
                    if (Convert.ToInt32(FullfillFontSize.Text) < 0 || Convert.ToInt32(FullfillFontSize.Text) > 10000)
                    {
                        throw new Exception("Размер шрифта вышел за диапазон");
                    }
                    fullfillopt.Fontsize = Convert.ToInt32(FullfillFontSize.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
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
                    fullfillopt.Transparancy = Convert.ToInt32(FullfillTransparancy.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);

            }

        }

        private void FullfillAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillAngle.Text == "" || FullfillAngle.Text == "-"))
            {
                try
                {

                    fullfillopt.Angle = Convert.ToInt32(FullfillAngle.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
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
                        Font font = new Font(fullfillopt.Fontname, fullfillopt.Fontsize);
                        SizeF size = g.MeasureString(fullfillopt.Text, font);
                        float width = size.Width - 5;



                        if (Convert.ToInt32(Fullfillwbtw.Text) < -width)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(width, 0) + " недопустимы для данного размера текста");
                        }
                        fullfillopt.Widthbetween = Convert.ToInt32(Fullfillwbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
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
                        Font font = new Font(fullfillopt.Fontname, fullfillopt.Fontsize);
                        SizeF size = g.MeasureString(fullfillopt.Text, font);
                        float height = size.Height - 5;



                        if (Convert.ToInt32(Fullfillhbtw.Text) < -height)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(height, 0) + " недопустимы для данного размера текста");
                        }
                        fullfillopt.Heightbetween = Convert.ToInt32(Fullfillhbtw.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
            }
        }

        private void FullfillStyle_TextChanged(object sender, EventArgs e)
        {

            switch (FullfillStyle.Text.ToLower())
            {

                case "regular":
                    fullfillopt.Fontstyle = FontStyle.Regular;
                    break;

                case "bold":
                    fullfillopt.Fontstyle = FontStyle.Bold;
                    break;

                case "italic":
                    fullfillopt.Fontstyle = FontStyle.Italic;
                    break;

                case "underline":
                    fullfillopt.Fontstyle = FontStyle.Underline;
                    break;
                default:

                    break;
            }
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                   , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
        }
        //Update image on fields change for fullfill END
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

        private void FullfillColorMenu_Click(object sender, EventArgs e)
        {
            if (!myCheckBox1.Checked)
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
                FullfillGradientUpd(color1, color2, color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
            }

        }

        private void FullfillColorDisplay_Click(object sender, EventArgs e)
        {
            FullfillColorDisplay.BackColor = colorupd();
            FullfillColorUpd(FullfillColorDisplay.BackColor);
        }
        private void FullfillColorUpd(Color color)
        {

            fullfillopt.Color = color;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                   , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
        }
        private void FullfillGradientUpd(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            fullfillopt.Color = color;
            fullfillopt.Color2 = color1;
            fullfillopt.Color3 = color2;
            fullfillopt.Colorstart = colorstart;
            fullfillopt.Colorend = colorend;
            fullfillopt.Colorangle = colorangle;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                   , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
        }



        private void myCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            fullfillopt.Color = Color.Red;
            fullfillopt.Color2 = Color.Blue;
            fullfillopt.Color3 = Color.Green;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                   , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!(textBox9.Text == "" || textBox9.Text == "-"))
            {
                try
                {
                    fullfillopt.Colorstart = Convert.ToInt32(textBox9.Text);
                    FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                  , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (!(textBox10.Text == "" || textBox10.Text == "-"))
            {
                try
                {
                    fullfillopt.Colorend = Convert.ToInt32(textBox10.Text);
                    FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                  , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (!(textBox11.Text == "" || textBox11.Text == "-"))
            {
                try
                {
                    fullfillopt.Colorstart = Convert.ToInt32(textBox11.Text);
                    FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                  , fullfillopt.Heightbetween, fullfillopt.Fontstyle, fullfillopt.Color, fullfillopt.Color2, fullfillopt.Color3, fullfillopt.Colorstart, fullfillopt.Colorend, fullfillopt.Colorangle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void customButtons1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



}
