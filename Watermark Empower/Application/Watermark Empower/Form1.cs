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

namespace Watermark_Empower
{
    public partial class Form1 : Form
    {
        string fileName = "tempinput.jpg";
        string filePath;
        int operation = 0;
        FullfillOptions fullfillopt = new FullfillOptions();
        Generator gen = new Generator();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        }

        private void customButtons1_Click(object sender, EventArgs e)
        {

        }

        private void Import_Image_Click(object sender, EventArgs e)
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
                    pictureBox1.Image = Image.FromStream(fileStream);
                    string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    // Save the image to the executable directory with the specified name
                    pictureBox1.Image.Save(Path.Combine(executableDirectory, "tempinput.jpg"), ImageFormat.Jpeg);
                    pictureBox1.Image.Save(Path.Combine(executableDirectory, "input.jpg"), ImageFormat.Jpeg);


                }
            }
        }

        private void FullfillSelector_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("fullfill");
            
            pictureBox1.Image.Dispose();
            if (operation == 0)
            {
                gen.GenPatternFullfill("Maxim", newoperation());
                pictureBox1.Image = Image.FromFile("tempinput2.jpg");
            }
            else
            {
                gen.GenPatternFullfill("Maxim", newoperation());
                pictureBox1.Image = Image.FromFile("tempinput.jpg");
            }

        }
        private void FullfillUpdate(string text, string fontname, int fontsize, int alpha, int angle, int widthbetween, int heightbetween, FontStyle fontstyle)
        {
            pictureBox1.Image.Dispose();
            if (operation == 0)
            {
                gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation());
                pictureBox1.Image = Image.FromFile("tempinput2.jpg");
            }
            else
            {
                gen.GenPatternFullfill(text, fontname, fontsize, alpha, angle, widthbetween, heightbetween, fontstyle, newoperation());
                pictureBox1.Image = Image.FromFile("tempinput.jpg");
            }
        }
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

        private void ChessSelector_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("chess");
            pictureBox1.Image.Dispose();
            if (operation == 0)
            {
                gen.GenPatternChess("Maxim", newoperation());
                pictureBox1.Image = Image.FromFile("tempinput2.jpg");
            }
            else
            {
                gen.GenPatternChess("Maxim", newoperation());
                pictureBox1.Image = Image.FromFile("tempinput.jpg");
            }
            
        }
        

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int hgt = tabControl1.Height;
            tabControl1.Height = this.Height-100;
            
            if (hgt < tabControl1.Height)
            {
                scrollableControl1.Height = tabControl1.Height;
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        public class FullfillOptions
        {
            string text = "Watermark";
            string fontname = "Arial";
            int fontsize = 48;
            int transparancy=128;
            int angle=25;
            int widthbetween = 0;
            int heightbetween = 0;
            FontStyle fontstyle = FontStyle.Regular;
            
            public string Fontname { get => fontname; set => fontname = value; }
            public int Fontsize { get => fontsize; set => fontsize = value; }
            public int Transparancy { get => transparancy;
                set {
                    double alpha = (double)value/100*255;
                    transparancy = Convert.ToInt32(alpha); 
                }
            }
            public int Angle { get => angle; set => angle = value; }
            public int Widthbetween { get => widthbetween; set => widthbetween = value; }
            public int Heightbetween { get => heightbetween; set => heightbetween = value; }
            public FontStyle Fontstyle { get => fontstyle; set => fontstyle = value; }
            public string Text { get => text; set => text = value; }
        }
        private void FullfillFont_TextChanged(object sender, EventArgs e)
        {
            
            fullfillopt.Fontname = FullfillFont.Text;
            FullfillUpdate(fullfillopt.Text,fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy,fullfillopt.Angle,fullfillopt.Widthbetween
                ,fullfillopt.Heightbetween,fullfillopt.Fontstyle);

        }

        private void FullfillText_TextChanged(object sender, EventArgs e)
        {
            fullfillopt.Text = FullfillText.Text;
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
        }

        private void FullfillFontSize_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillFontSize.Text == ""))
            {
                try
                {

                    fullfillopt.Fontsize = Convert.ToInt32(FullfillFontSize.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
            }
            
        }

        private void FullfillTransparancy_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillTransparancy.Text == ""))
            {
                try
                {

                    fullfillopt.Transparancy = Convert.ToInt32(FullfillTransparancy.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle);

            }
           
        }

        private void FullfillAngle_TextChanged(object sender, EventArgs e)
        {
            if (!(FullfillTransparancy.Text == ""))
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
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
            }
           
        }

        private void Fontfillwbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(Fullfillwbtw.Text == ""))
            {
                try
                {

                    fullfillopt.Widthbetween= Convert.ToInt32(Fullfillwbtw.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
            }
        }

        private void Fullfillhbtw_TextChanged(object sender, EventArgs e)
        {
            if (!(Fullfillhbtw.Text == ""))
            {
                try
                {

                    fullfillopt.Heightbetween = Convert.ToInt32(Fullfillhbtw.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                    , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
            }
        }

        private void FullfillStyle_TextChanged(object sender, EventArgs e)
        {
            
            switch (FullfillStyle.Text)
            {
                case "Regular":
                case "regular":
                    fullfillopt.Fontstyle = FontStyle.Regular;
                    break;
                case "Bold":
                case "bold":
                    fullfillopt.Fontstyle = FontStyle.Bold;
                    break;
                case "Italic":
                case "italic":
                    fullfillopt.Fontstyle = FontStyle.Italic;
                    break;
                case "Underline":
                case "underline":
                    fullfillopt.Fontstyle = FontStyle.Underline;
                    break;
                default:
                    
                    break;
            }
            FullfillUpdate(fullfillopt.Text, fullfillopt.Fontname, fullfillopt.Fontsize, fullfillopt.Transparancy, fullfillopt.Angle, fullfillopt.Widthbetween
                   , fullfillopt.Heightbetween, fullfillopt.Fontstyle);
        }
    }
    
      
    
}
