using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;


namespace WatermarkGenerator
{
    public class Generator
    {
        
        static void Main(string[] args)
        {

            

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
            Color color2 = Color.Green;
            Color color3 = Color.Blue;
            int colorstart = 0;
            int colorend = 300;
            int colorangle = 50;
            string effect = "None";
            string operation = "Fullfill";

            public string Fontname { get => fontname; set => fontname = value; }
            public int Fontsize { get => fontsize; set => fontsize = value; }
            public int Transparancy
            {
                get => transparancy;
                set =>transparancy = value;
               
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
            public string Operation { get => operation; set => operation = value; }
        }
        public class ProjectSettings
        {
            int rows = 10;
            int columns = 10;
            int xoffset =-200;
            int yoffset = -200;
           
            bool isgradienton = false;
     

            public int Rows { get => rows; set => rows = value; }
            public int Columns { get => columns; set => columns = value; }
            public int Xoffset { get => xoffset; set => xoffset = value; }
            public int Yoffset { get => yoffset; set => yoffset = value; }
            
            public bool Isgradienton { get => isgradienton; set => isgradienton = value; }
        }
        public class EffectSettings
        {
            Color effectcolor1 = Color.Red;
            Color effectcolor2 = Color.Green;
            Color effectcolor3 = Color.Blue;
            string selectedeffect = "None";
            int effectxoffset = 2;
            int effectyoffset = 2;
            int transparancy = 128;

            public Color EffectColor1 { get => effectcolor1; set => effectcolor1 = value; }
            public Color EffectColor2 { get => effectcolor2; set => effectcolor2 = value; }
            public Color EffectColor3 { get => effectcolor3; set => effectcolor3 = value; }
            public string SelectedEffect { get => selectedeffect; set => selectedeffect = value; }
            public int EffectXoffset { get => effectxoffset; set => effectxoffset = value; }
            public int EffectYoffset { get => effectyoffset; set => effectyoffset = value; }
            public int Transparancy
            {
                get => transparancy; set => transparancy = value; 
              
            }
        }
        public class XmlColor
        {
            public int Red1 { get; set; }
            public int Green1 { get; set; }
            public int Blue1 { get; set; }
            public int Red2 { get; set; }
            public int Green2 { get; set; }
            public int Blue2 { get; set; }
            public int Red3 { get; set; }
            public int Green3 { get; set; }
            public int Blue3 { get; set; }

            public int EffectRed1 { get; set; }
            public int EffectGreen1 { get; set; }
            public int EffectBlue1 { get; set; }
            public int EffectRed2 { get; set; }
            public int EffectGreen2 { get; set; }
            public int EffectBlue2 { get; set; }
            public int EffectRed3 { get; set; }
            public int EffectGreen3 { get; set; }
            public int EffectBlue3 { get; set; }

            public void ToColor(Color color, Color color2, Color color3, Color effectcolor1, Color effectcolor2, Color effectcolor3)
            {
                Red1 = color.R;
                Green1 = color.G;
                Blue1 = color.B;
                Red2 = color2.R;
                Green2 = color2.G;
                Blue2 = color2.B;
                Red3 = color3.R;
                Green3 = color3.G;
                Blue3 = color3.B;

                EffectRed1 = effectcolor1.R;
                EffectGreen1 = effectcolor1.G;
                EffectBlue1 = effectcolor1.B;
                EffectRed2 = effectcolor2.R;
                EffectGreen2 = effectcolor2.G;
                EffectBlue2 = effectcolor2.B;
                EffectRed3 = effectcolor3.R;
                EffectGreen3 = effectcolor3.G;
                EffectBlue3 = effectcolor3.B;
            }
            public Color GetColor(int color1, int color2, int color3)
            {
                return Color.FromArgb(color1, color2, color3);
            }
            

        }
        public class Wrapper
        {
            public Options WrapedOptions { get; set; }
            public ProjectSettings WrapedPrjSettings { get; set; }
            public EffectSettings WrapedEffectSettings { get; set; }
            public XmlColor WrapedColor { get; set; }
        }
        public class Preparation
        {
            public Font font { get; set; }
            public Brush brush { get; set; }
            public PointF position { get; set; }
            public Matrix transform { get; set; }
         
        }
        Preparation prepare = new Preparation();
        private Graphics DrawGlitchBack(PointF position, Graphics graphics, string text,Font font, EffectSettings efffect)
        {
            Color[] gltchColors = new Color[] { Color.FromArgb(efffect.Transparancy, efffect.EffectColor1), Color.FromArgb(efffect.Transparancy, efffect.EffectColor2), 
               Color.FromArgb(efffect.Transparancy,efffect.EffectColor3), };
            position.X = position.X - efffect.EffectXoffset*3;
            position.Y = position.Y - efffect.EffectYoffset * 3;
            foreach (Color c in gltchColors)
            {
                Brush gltchbrush = new SolidBrush(c);
                graphics.DrawString(text, font, gltchbrush, position);
                position.X = position.X + efffect.EffectXoffset;
                position.Y = position.Y + efffect.EffectYoffset;
            }
            return graphics;
        }
       private Graphics DrawGlitchFront(PointF position, Graphics graphics, string text, Font font, EffectSettings efffect)
        {
            Color[] gltchColors = new Color[] { Color.FromArgb(efffect.Transparancy, efffect.EffectColor1), Color.FromArgb(efffect.Transparancy, efffect.EffectColor2),
                Color.FromArgb(efffect.Transparancy, efffect.EffectColor3), };
            
            foreach (Color c in gltchColors)
            {
                position.X = position.X + efffect.EffectYoffset;
                position.Y = position.Y + efffect.EffectYoffset;
                Brush gltchbrush = new SolidBrush(c);
                graphics.DrawString(text, font, gltchbrush, position);
               
            }
            return graphics;
        }

        private void Draw(PointF position, int row, int column, SizeF size, Font font, Brush brush, Graphics graphics, Options options, ProjectSettings settings, EffectSettings efffectsettings)
        {
          
            // Calculate the position of the watermark
            position.X = column * size.Width + settings.Xoffset;
            position.Y = row * size.Height + settings.Yoffset;
            switch (options.Effect)
            {
                case ("None"):
                    graphics.DrawString(options.Text, font, brush, position);
                    break;
                case ("Glitch"):
                    DrawGlitchBack(position, graphics, options.Text, font, efffectsettings);
                    graphics.DrawString(options.Text, font, brush, position);
                    break;
                case ("HardGlitch"):
                    DrawGlitchBack(position, graphics, options.Text, font,efffectsettings);
                    graphics.DrawString(options.Text, font, brush, position);
                    DrawGlitchFront(position, graphics, options.Text, font,efffectsettings);
                    break;
                default:
                    break;
            }
        }
        public void prepareImage(Options options)
        {
            Image image = Image.FromFile("input.jpg");
            prepare.font = new Font(options.Fontname, options.Fontsize, options.Fontstyle);
            prepare.brush = new SolidBrush(Color.FromArgb(options.Transparancy, options.Color));
            prepare.position = new PointF(image.Width / 2, image.Height / 2);
            prepare.transform = new Matrix();
            prepare.transform.RotateAt(options.Angle, prepare.position);
            image.Dispose();
        }
        public void prepareGradient(Options options)
        {
            ColorBlend blend = new ColorBlend();
            blend.Positions = new float[] { 0, 0.5f, 1 };
            blend.Colors = new Color[] { Color.FromArgb(options.Transparancy, options.Color), Color.FromArgb(options.Transparancy, options.Color2), Color.FromArgb(options.Transparancy, options.Color3) };
            LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(options.Colorstart, 0), new Point(options.Colorend, 100), Color.Red, Color.Blue);
            gradientBrush.InterpolationColors = blend;
            gradientBrush.RotateTransform(options.Colorangle);
            prepare.brush = gradientBrush;
        }


        public void GenPatternFullfill( Options options, ProjectSettings settings, EffectSettings effectsettings)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type and set the interpolation mode to high quality
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //preparing settings for drawing
                prepareImage(options);
                graphics.Transform = prepare.transform;
                SizeF size = graphics.MeasureString(options.Text, prepare.font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows+settings.Rows; row++)
                {
                    for (int column = 0; column < columns+settings.Columns; column++)
                    {
                        Draw(prepare.position, row, column, size, prepare.font, prepare.brush, graphics, options, settings, effectsettings);                           
                    }
                }
            }
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            
        }
        string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      

        
        public void GenPatternFullfillGradient(Options options, ProjectSettings settings, EffectSettings effectsettings)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type and set the interpolation mode to high quality
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //preparing settings for drawing
                prepareImage(options);
                prepareGradient(options);
                graphics.Transform = prepare.transform;
                SizeF size = graphics.MeasureString(options.Text, prepare.font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + settings.Rows; row++)
                {
                    for (int column = 0; column < columns + settings.Columns; column++)
                    {
                        Draw(prepare.position, row, column, size, prepare.font, prepare.brush, graphics, options, settings, effectsettings);

                    }
                }
            }
                // Save the output image to a file
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            
        }

        public void GenPatternChess(Options options, ProjectSettings settings, EffectSettings effectsettings)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type and set the interpolation mode to high quality
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //preparing settings for drawing
                prepareImage(options);
                graphics.Transform = prepare.transform;
                SizeF size = graphics.MeasureString(options.Text, prepare.font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + settings.Rows; row++)
                {
                    for (int column = 0; column < columns + settings.Columns; column++)
                    {
                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }
                        Draw(prepare.position, row, column, size, prepare.font, prepare.brush, graphics, options, settings, effectsettings);
                    }
                }
            }
            // Save the output image to a file
            image.Save("tempinput.jpg", ImageFormat.Png);
            image.Dispose();
        }
        public void GenPatternChessGradient(Options options, ProjectSettings settings, EffectSettings effectsettings)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type and set the interpolation mode to high quality
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //preparing settings for drawing
                prepareImage(options);
                prepareGradient(options);
                graphics.Transform = prepare.transform;
                SizeF size = graphics.MeasureString(options.Text, prepare.font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + settings.Rows; row++)
                {
                    for (int column = 0; column < columns + settings.Columns; column++)
                    {
                       
                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }
                        Draw(prepare.position, row, column, size, prepare.font, prepare.brush, graphics, options, settings, effectsettings);

                    }
                }
            }

            // Save the output image to a file

            image.Save("tempinput.jpg", ImageFormat.Png);
            image.Dispose();

        }
        //TODO NEEDS UPDATE
        public void RandomWatermarkA1(string text)
        {
            Image image = Image.FromFile("input.jpg");
         
            try
            {
                //create graphics object
                Graphics graphics = Graphics.FromImage(image);
                //set font
                Font font = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);
                //set brush
                SolidBrush brush = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
                //set random generator
                Random random = new Random();
                int tempx = 0;
                int tempy = 0;
                int x = 0;
                int y = 0;




                //generate random points
                for (int i = 0; i < 7; i++)
                {
                    do
                    {
                        x = random.Next(0, image.Width);
                        y = random.Next(0, image.Height);
                        Console.WriteLine(x + " " + y);
                    }
                    while (!(tempx > x + 150 || tempx < x - 150) || (tempy > y + 200 || tempy < y - 200));

                    Console.WriteLine("Passed" + x + " " + y);
                   



                    tempx = x;
                    tempy = y;

                    //rotate 60 degrees
                    graphics.TranslateTransform(x, y);
                    graphics.RotateTransform(30);
                    //draw text 
                    graphics.DrawString(text, font, brush, 0, 0);
                    //reset to origin
                    graphics.ResetTransform();
                }
                //save image
                image.Save("outputrandomA1m1.png", ImageFormat.Png);
                Console.WriteLine("Image with watermark generated!");
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }



        }
        //TODO NEEDS UPDATE
        public void RandomWatermarkA2(string text, int iterations, int sizing, int angle)
        {
            Image image = Image.FromFile("input.jpg");

            try
            {
                //create graphics object
                Graphics graphics = Graphics.FromImage(image);
                //set font
                Font font = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);
                //set brush
                SolidBrush brush = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
                //set random generator
                Random random = new Random();

                int x = 0;
                int y = 0;
                int sizex = image.Width / iterations;
                int sizey = image.Height / iterations;
                if (sizing == 0)
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizex * i && x < sizex * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);
                       




                        //rotate 60 degrees
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(angle);
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }
                else if (sizing == 1)
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizey * i && x < sizey * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);
                       




                        //rotate 60 degrees
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(30);
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }
                else
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizey * i && x < sizey * (i + 1) || x > sizex * i && x < sizex * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);
                       




                        //rotate 60 degrees
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(30);
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }



                //save image
                image.Save("outputrandomA2m2.png", ImageFormat.Png);
                Console.WriteLine("Image with watermark generated!");
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

        }
        //TODO NEEDS UPDATE
        public void RandomWatermarkA2(string text, int iterations, int sizing, bool randangle, int randbegin, int randend)
        {
            Image image = Image.FromFile("input.jpg");
            
            try
            {
                //create graphics object
                Graphics graphics = Graphics.FromImage(image);
                //set font
                Font font = new Font("Arial", 90, FontStyle.Bold, GraphicsUnit.Pixel);
                //set brush
                SolidBrush brush = new SolidBrush(Color.FromArgb(170, 0, 0, 0));
                //set random generator
                Random random = new Random();
                int angle = 0;
                int x = 0;
                int y = 0;
                int sizex = image.Width / iterations;
                int sizey = image.Height / iterations;
                if (sizing == 0)
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizex * i && x < sizex * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);
                        if (randangle == true)
                        {
                            angle = random.Next(randbegin, randend);

                            do
                            {
                                angle = random.Next(randend - randbegin) + randbegin;//getting random angle

                            } while (angle > randend);
                            Console.WriteLine("angle res " + angle);
                        }
                        graphics.RotateTransform(angle); //rotate

                        graphics.TranslateTransform(x, y);
                                                          
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }
                else if (sizing == 1)
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizey * i && x < sizey * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);
               
                        //rotate 60 degrees
                        graphics.TranslateTransform(x, y);
                        if (randangle == true)
                        {
                            angle = random.Next(randbegin, randend);

                            do
                            {
                                angle = random.Next(randend - randbegin) + randbegin;//getting random angle

                            } while (angle > randend);
                            Console.WriteLine("angle res " + angle);

                        }
                        graphics.RotateTransform(angle);
                       
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }
                else
                {
                    for (int i = 0; i < iterations; i++)
                    {
                        do
                        {
                            x = random.Next(0, image.Width);
                            y = random.Next(0, image.Height);
                            Console.WriteLine(x + " " + y);
                        }
                        while (!(x > sizey * i && x < sizey * (i + 1) || x > sizex * i && x < sizex * (i + 1)));

                        Console.WriteLine("Passed" + x + " " + y);

                       
                        graphics.TranslateTransform(x, y);
                        if (randangle == true)
                        {
                            angle = random.Next(randbegin, randend);

                            do
                            {
                                angle = random.Next(randend - randbegin) + randbegin;//getting random angle

                            } while (angle > randend);
                            Console.WriteLine("angle res " + angle);

                        }
                        graphics.RotateTransform(angle);
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                }

                //save image
                image.Save("outputrandomA2m3.png", ImageFormat.Png);
                Console.WriteLine("Image with watermark generated!");
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

        }
       //TODO NEEDS UPDATE
        public void RandomWatermarkA3(string text, int frequency, int marksperrow, bool adjacentcol, bool adjacentrow, int angle, int widthbetween, int heightbetween)
        {
           
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font("Arial", 48, FontStyle.Regular);
                Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);
                size.Width = size.Width + widthbetween;
                size.Height = size.Height + heightbetween;
                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Set the rotation angle of the watermark


                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                Console.WriteLine(rows);
                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();
                int marks = 0;

                // Draw the watermark text on the image
                for (int row = 0; row < rows*3; row++)
                {
                    marks = 0;// reset for column per line limit

                    for (int column = 0; column < columns; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height;// - image.Height;
                        int rnd = random.Next(0, 100);
                        if (adjacentrow == true)//if adjacentrow is true, fill watermarks in chess pattern
                        {
                            if ((row + column) % 2 == 0)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (adjacentcol == true)//if adjacentrow isnt true and adjacentcol is true, fill matrix without adjacent colls
                            {
                                if (column % 2 == 0)
                                {
                                    continue;
                                }

                            }
                        }
                        // Draw the watermark text on the image
                        if (rnd <= frequency)
                        {
                            graphics.DrawString(text, font, brush, position);
                            marks++;//inc counter for limit per row
                            if (marks == marksperrow) { break; }
                        }

                    }


                }
            }

            // Save the output image to a file
            image.Save("outputA3m2.png", ImageFormat.Png);
        }
        //TODO NEEDS UPDATE
        public void RandomWatermarkA3(string text, int frequency, int marksperrow, bool adjacentcol, bool adjacentrow, bool randangle, int randbegin, int randend, int widthbetween, int heightbetween)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font("Arial", 48, FontStyle.Regular);
                Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);
                size.Width = size.Width + widthbetween;
                size.Height = size.Height + heightbetween;
                PointF position = new PointF(image.Width / 2, image.Height / 2);
                Random rand = new Random();
                // Set the rotation angle of the watermark

                int angle = 0;
                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
             

                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                
                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();
                int marks;

                // Draw the watermark text on the image
                for (int row = 0; row < rows*3; row++)
                {
                    marks = 0;// reset for column per line limit

                    for (int column = 0; column < columns*2; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height-size.Height*4;// - image.Height;
                        int rnd = random.Next(0, 100);
                        if (adjacentrow == true)//if adjacentrow is true, fill watermarks in chess pattern
                        {
                            if ((row + column) % 2 == 0)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (adjacentcol == true)//if adjacentrow isnt true and adjacentcol is true, fill matrix without adjacent colls
                            {
                                if (column % 2 == 0)
                                {
                                    continue;
                                }

                            }
                        }
                        // Draw the watermark text on the image
                        if (rnd <= frequency)
                        {
                            if (randangle == true)
                            {
                                transform.Reset();
                                graphics.Transform = transform;
                                do
                                {
                                    angle = rand.Next(randend - randbegin) + randbegin;//getting random angle
                                    
                                } while (angle > randend);
                               
                            }
                            transform.RotateAt(angle, position);

                            graphics.Transform = transform;
                            graphics.DrawString(text, font, brush, position);
                            marks++;//inc counter for limit per row
                            if (marks == marksperrow) { break; }
                        }

                    }


                }
            }

            // Save the output image to a file
            image.Save("outputA3m3.png", ImageFormat.Png);
        }

        
       
        

    }
}
   
