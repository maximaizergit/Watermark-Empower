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

namespace EffectDialog
{
    public partial class Form2 : Form
    {
        public string SelectedEffect { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void customButtons1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void customButtons2_Click(object sender, EventArgs e)
        {
            SelectedEffect = comboBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case ("None"):
                    NonePreview();
                        break;
                case ("Glitch"):
                    GlitchEffectPreview(false);
                    break;
                case ("HardGlitch"):
                    GlitchEffectPreview(true);
                    break;
                case ("WhiteBox"):
                    WhiteBoxPreview();
                    break;
                case ("TransparentWhiteBox"):
                    TransparentWhiteBoxPreview();
                    break ;
                case ("RoundedWhiteBox"):
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
            Color[] gltchColors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue), };
            // Create a Graphics object from the image
            using (Graphics g = Graphics.FromImage(image))
            {
                // Set the text color and font
                Brush brush = new SolidBrush(Color.FromArgb(128,Color.White));
                Font font = new Font("Arial", 20);
                points.X = points.X - 6;
                points.Y = points.Y - 6;
                foreach(Color c in gltchColors)
                {
                    Brush gltchbrush = new SolidBrush(c);
                    g.DrawString("Watermark", font, gltchbrush, points);
                    points.X = points.X + 2;
                    points.Y = points.Y + 2;
                }
                // Draw the text at the specified position
                g.DrawString("Watermark", font, brush, points);
                if (ishard)
                {
                    
                    foreach (Color c in gltchColors)
                    {
                        Brush gltchbrush = new SolidBrush(c);
                        g.DrawString("Watermark", font, gltchbrush, points);
                        points.X = points.X + 2;
                        points.Y = points.Y + 2;
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
    }
        
    }

