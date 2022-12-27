using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatermarkGenerator;

namespace EffectDialog
{
    public partial class Form2 : Form
    {
        
        public Generator.EffectSettings EffectSettings = new Generator.EffectSettings();

        public Form2(Generator.EffectSettings importsettings)
        {
            EffectSettings = importsettings;
           
            InitializeComponent();
        }

        private void customButtons1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void customButtons2_Click(object sender, EventArgs e)
        {
            EffectSettings.SelectedEffect = comboBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case ("None"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    NonePreview();
                        break;
                case ("Glitch"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    GlitchEffectPreview(false);
                    break;
                case ("HardGlitch"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab("Glitch");
                    GlitchEffectPreview(true);
                    break;
                case ("WhiteBox"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    WhiteBoxPreview();
                    break;
                case ("TransparentWhiteBox"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    TransparentWhiteBoxPreview();
                    break ;
                case ("RoundedWhiteBox"):
                    EffectSettings.SelectedEffect = comboBox1.Text;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    RoundedWhiteBoxPreview();
                    break;
                default:
                    break;
            }
                
        }
        public void GlitchEffectPreview(bool ishard)
        {
            Bitmap image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            PointF points = new PointF(120, 75);
           
            Color[] gltchColors = new Color[] { Color.FromArgb(EffectSettings.Transparancy, EffectSettings.EffectColor1), 
                Color.FromArgb(EffectSettings.Transparancy, EffectSettings.EffectColor2), Color.FromArgb(EffectSettings.Transparancy, EffectSettings.EffectColor3), };
            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush brush = new SolidBrush(Color.FromArgb(128,Color.White));
                Font font = new Font("Arial", 20);
                points.X = points.X - EffectSettings.EffectXoffset *3;
                points.Y = points.Y - EffectSettings.EffectYoffset *3;
                foreach(Color c in gltchColors)
                {
                    Brush gltchbrush = new SolidBrush(c);
                    g.DrawString("Watermark", font, gltchbrush, points);
                    points.X = points.X + EffectSettings.EffectXoffset;
                    points.Y = points.Y + EffectSettings.EffectYoffset;
                }
                // Draw the text at the specified position
                g.DrawString("Watermark", font, brush, points);
                if (ishard)
                {
                    
                    foreach (Color c in gltchColors)
                    {
                        Brush gltchbrush = new SolidBrush(c);
                        g.DrawString("Watermark", font, gltchbrush, points);
                        points.X = points.X + EffectSettings.EffectXoffset;
                        points.Y = points.Y + EffectSettings.EffectYoffset;
                    }
                }
               
            }

            // Set the image of the PictureBox
            pictureBox3.Image = image;

            // Refresh the PictureBox to display the text
            pictureBox3.Refresh();
        }
        public void NonePreview()
        {
            Bitmap image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            PointF points = new PointF(120, 75);
            
            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush brush = new SolidBrush(Color.FromArgb(255, Color.White));
                Font font = new Font("Arial", 20);
               
               
                // Draw the text at the specified position
                g.DrawString("Watermark", font, brush, points);
                
                

            }

            // Set the image of the PictureBox
            pictureBox3.Image = image;

            // Refresh the PictureBox to display the text
            pictureBox3.Refresh();
        }
        public void WhiteBoxPreview()
        {
            Bitmap image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            PointF points = new PointF(120, 75);

            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush RectBrush = new SolidBrush(Color.FromArgb(255, Color.White));
                Brush brush = new SolidBrush(Color.FromArgb(255, Color.Black));
                Font font = new Font("Arial", 20);
                SizeF textSize = g.MeasureString("Watermark", font);
                
                float width = textSize.Width + 20;
                float height = textSize.Height + 20;
                // Draw the text at the specified position
                g.FillRectangle(RectBrush, points.X-10, points.Y-10, width, height);
                g.DrawString("Watermark", font, brush, points);



            }

            // Set the image of the PictureBox
            pictureBox3.Image = image;

            // Refresh the PictureBox to display the text
            pictureBox3.Refresh();
        }
        public void TransparentWhiteBoxPreview()
        {
            Bitmap image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            PointF points = new PointF(120, 75);

            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush RectBrush = new SolidBrush(Color.FromArgb(128, Color.White));
                Brush brush = new SolidBrush(Color.FromArgb(255, Color.Black));
                Font font = new Font("Arial", 20);
                SizeF textSize = g.MeasureString("Watermark", font);

                float width = textSize.Width + 20;
                float height = textSize.Height + 20;
                // Draw the text at the specified position
                g.FillRectangle(RectBrush, points.X - 10, points.Y - 10, width, height);
                g.DrawString("Watermark", font, brush, points);



            }

            // Set the image of the PictureBox
            pictureBox3.Image = image;

            // Refresh the PictureBox to display the text
            pictureBox3.Refresh();
        }
        public void RoundedWhiteBoxPreview()
        {
            Bitmap image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            PointF points = new PointF(120, 75);

            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush RectBrush = new SolidBrush(Color.FromArgb(255, Color.White));
                Brush brush = new SolidBrush(Color.FromArgb(255, Color.Black));
                Font font = new Font("Arial", 20);
                SizeF textSize = g.MeasureString("Watermark", font);
                GraphicsPath path = new GraphicsPath();
                float width = textSize.Width + 20;
                float height = textSize.Height + 20;
                int radius = 50;
                path.AddArc(points.X-10, points.Y - 10, radius, radius, 180, 90);
                path.AddArc(points.X-10 + width - radius, points.Y - 10, radius, radius, 270, 90);
                path.AddArc(points.X-10 + width - radius, points.Y - 10 + height - radius, radius, radius, 0, 90);
                path.AddArc(points.X-10, points.Y - 10 + height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                // Draw the rounded rectangle and the text
                g.FillPath(RectBrush, path);
                // Draw the text at the specified position
                
                g.DrawString("Watermark", font, brush, points);



            }

            // Set the image of the PictureBox
            pictureBox3.Image = image;

            // Refresh the PictureBox to display the text
            pictureBox3.Refresh();
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

        private void Xoffset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                EffectSettings.EffectXoffset = Convert.ToInt32(Xoffset.Text);
                comboBox1_SelectedIndexChanged(sender, e);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Yoffset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                EffectSettings.EffectYoffset = Convert.ToInt32(Yoffset.Text);
                comboBox1_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Opacity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(Convert.ToInt32(EffectOpacity.Text) >=0 || Convert.ToInt32(EffectOpacity.Text)<=100)
                {
                    EffectSettings.Transparancy = Convert.ToInt32(Convert.ToDouble(EffectOpacity.Text) / 100 * 255);
                    comboBox1_SelectedIndexChanged(sender, e);
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            Xoffset.Text = EffectSettings.EffectXoffset + "";
            Yoffset.Text = EffectSettings.EffectYoffset.ToString();
            double transparancy = (Convert.ToDouble(EffectSettings.Transparancy) / 255 * 100);

            EffectOpacity.Text = Math.Round(transparancy, 0).ToString();
           
            ColorMenu1Upd(EffectSettings.EffectColor1, EffectSettings.EffectColor2, EffectSettings.EffectColor3);
            switch (EffectSettings.SelectedEffect)
            {
                case ("None"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    NonePreview();
                    break;
                case ("Glitch"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    GlitchEffectPreview(false);
                    break;
                case ("HardGlitch"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab("Glitch");
                    GlitchEffectPreview(true);
                    break;
                case ("WhiteBox"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    WhiteBoxPreview();
                    break;
                case ("TransparentWhiteBox"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    TransparentWhiteBoxPreview();
                    break;
                case ("RoundedWhiteBox"):
                    comboBox1.Text = EffectSettings.SelectedEffect;
                    EffectSettingsControl.SelectTab(EffectSettings.SelectedEffect);
                    RoundedWhiteBoxPreview();
                    break;
                default:
                    break;
            }
        }

        private void ColorMenu_Click(object sender, EventArgs e)
        {
            Color color1 = colorupd();
            Color color2 = colorupd();
            Color color3 = colorupd();
            ColorMenu1Upd(color1,color2,color3);
            comboBox1_SelectedIndexChanged(sender, e);

        }
        private void ColorMenu1Upd(Color color1, Color color2, Color color3)
        {
            ColorDisplay.Image = new Bitmap(100, 50);
            Graphics graphics = Graphics.FromImage(ColorDisplay.Image);
            Brush brush = new SolidBrush(color1);
            graphics.FillRectangle(brush, 0, 0, 33, 50);
            brush = new SolidBrush(color2);
            graphics.FillRectangle(brush, 33, 0, 33, 50);
            brush = new SolidBrush(color3);
            graphics.FillRectangle(brush, 66, 0, 34, 50);
            ColorDisplay.Refresh();
            EffectSettings.EffectColor1 = color1;
            EffectSettings.EffectColor2 = color2;
            EffectSettings.EffectColor3 = color3;
            
        }
    }
        
    }

