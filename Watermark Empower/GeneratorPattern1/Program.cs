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
        public class ProjectSettings
        {
            int rows = 10;
            int columns = 10;
            int xoffset =-200;
            int yoffset = -200;
     

            public int Rows { get => rows; set => rows = value; }
            public int Columns { get => columns; set => columns = value; }
            public int Xoffset { get => xoffset; set => xoffset = value; }
            public int Yoffset { get => yoffset; set => yoffset = value; }

        }
       
       

         

        public void GenPatternFullfill(int operation, Options options, ProjectSettings settings)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(options.Fontname, options.Fontsize, options.Fontstyle);
                ColorBlend blend = new ColorBlend();
                Color[] gltchColors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue), };
            
                Brush brush = new SolidBrush(Color.FromArgb(options.Transparancy, options.Color));
              

                PointF position = new PointF(image.Width / 2, image.Height / 2);
                


                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(options.Angle, position);
                graphics.Transform = transform;

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(options.Text, font);

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
                        // Calculate the position of the watermark
                        position.X = column * size.Width+settings.Xoffset;
                        position.Y = row * size.Height +settings.Yoffset;
                        switch (options.Effect)
                        {
                            case ("None"):
                                graphics.DrawString(options.Text, font, brush, position);
                                break;
                            case ("Glitch"):

                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {
                                   
                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, brush, position);
                                break;
                            case ("HardGlitch"):
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, brush, position);
                                foreach (Color c in gltchColors)
                                {
                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                position.X = position.X - 6;
                                position.Y= position.Y - 6;
                                break;
                            default:
                                break;
                        }
                        // Draw the watermark text on the image
                        
                    }
                }
            }

            // Save the output image to a file

            if (operation == 0)
            {
                image.Save("tempinput2.jpg", ImageFormat.Png);
                image.Dispose();
            }
            else
            {
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            }
        }
        public void GenPatternFullfillGradient( int operation, Options options)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(options.Fontname, options.Fontsize, options.Fontstyle);
                ColorBlend blend = new ColorBlend();
                Color[] gltchColors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue), };
                blend.Positions = new float[] { 0, 0.5f,1 };
                    blend.Colors = new Color[] { Color.FromArgb(options.Transparancy, options.Color), Color.FromArgb(options.Transparancy, options.Color2),Color.FromArgb(options.Transparancy, options.Color3) };
                    LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(options.Colorstart, 0), new Point(options.Colorend, 100), Color.Red, Color.Blue);
                    gradientBrush.InterpolationColors = blend;

                    gradientBrush.RotateTransform(options.Colorangle);
                

                
                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Set the rotation angle of the watermark


                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(options.Angle, position);
                graphics.Transform = transform;

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(options.Text, font);

                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;
                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows * 3; row++)
                {
                    for (int column = 0; column < columns * 2; column++)
                    {
                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;
                        switch (options.Effect)
                        {
                            case ("None"):
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                break;
                            case ("Glitch"):

                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                break;
                            case ("HardGlitch"):
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                foreach (Color c in gltchColors)
                                {
                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                break;
                            default:
                                break;
                        }
                        // Draw the watermark text on the image
                      
                    }
                }
            }

            // Save the output image to a file

            if (operation == 0)
            {
                image.Save("tempinput2.jpg", ImageFormat.Png);
                image.Dispose();
            }
            else
            {
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            }
        }
        //TODO NEEDS UPDATE
        public void RandomWatermarkA1(string text)
        {
            Image image = Image.FromFile("input.jpg");
            image = Image.FromFile("input.jpg");
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

        
        //TODO NEEDS gradient
        public void GenPatternChess( int operation, Options options)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(options.Fontname, options.Fontsize, options.Fontstyle);
                ColorBlend blend = new ColorBlend();
                Color[] gltchColors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue), };

                Brush brush = new SolidBrush(Color.FromArgb(options.Transparancy, options.Color));
                
                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(options.Text, font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;
                PointF position = new PointF(image.Width / 2, image.Height / 2);

               
                

                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(options.Angle, position);
                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);

                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();


                // Draw the watermark text on the image
                for (int row = 0; row < rows*3; row++)
                {


                    for (int column = 0; column < columns*2; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;


                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }
                        switch (options.Effect)
                        {
                            case ("None"):
                                graphics.DrawString(options.Text, font, brush, position);
                                break;
                            case ("Glitch"):

                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, brush, position);
                                break;
                            case ("HardGlitch"):
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, brush, position);
                                foreach (Color c in gltchColors)
                                {
                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                break;
                            default:
                                break;
                        }                                                                   
                    }
                }
            }
            // Save the output image to a file

            if (operation == 0)
            {
                image.Save("tempinput2.jpg", ImageFormat.Png);
                image.Dispose();
            }
            else
            {
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            }
        }
        public void GenPatternChessGradient(int operation, Options options)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(options.Fontname, options.Fontsize, options.Fontstyle);
                ColorBlend blend = new ColorBlend();
                Color[] gltchColors = new Color[] { Color.FromArgb(128, Color.Red), Color.FromArgb(128, Color.Green), Color.FromArgb(128, Color.Blue), };
                blend.Positions = new float[] { 0, 0.5f, 1 };
                blend.Colors = new Color[] { Color.FromArgb(options.Transparancy, options.Color), Color.FromArgb(options.Transparancy, options.Color2), Color.FromArgb(options.Transparancy, options.Color3) };
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(options.Colorstart, 0), new Point(options.Colorend, 100), Color.Red, Color.Blue);
                gradientBrush.InterpolationColors = blend;

                gradientBrush.RotateTransform(options.Colorangle);

                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(options.Text, font);
                size.Width = size.Width + options.Widthbetween;
                size.Height = size.Height + options.Heightbetween;
               
                Matrix transform = new Matrix();
                transform.RotateAt(options.Angle, position);
                graphics.Transform = transform;
                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);

                int columns = (int)Math.Ceiling(image.Width / size.Width);
                // Draw the watermark text on the image
                for (int row = 0; row < rows * 3; row++)
                {
                    for (int column = 0; column < columns * 2; column++)
                    {
                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;
                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }
                        switch (options.Effect)
                        {
                            case ("None"):
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                break;
                            case ("Glitch"):

                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                break;
                            case ("HardGlitch"):
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                foreach (Color c in gltchColors)
                                {

                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                // Draw the text at the specified position
                                graphics.DrawString(options.Text, font, gradientBrush, position);
                                foreach (Color c in gltchColors)
                                {
                                    Brush gltchbrush = new SolidBrush(c);
                                    graphics.DrawString(options.Text, font, gltchbrush, position);
                                    position.X = position.X + 2;
                                    position.Y = position.Y + 2;
                                }
                                position.X = position.X - 6;
                                position.Y = position.Y - 6;
                                break;
                            default:
                                break;
                        }
                        // Draw the watermark text on the image

                    }
                }
            }

            // Save the output image to a file

            if (operation == 0)
            {
                image.Save("tempinput2.jpg", ImageFormat.Png);
                image.Dispose();
            }
            else
            {
                image.Save("tempinput.jpg", ImageFormat.Png);
                image.Dispose();
            }
        }

    }
}
   
