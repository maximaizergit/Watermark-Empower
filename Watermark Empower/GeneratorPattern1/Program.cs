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

            Generator p = new Generator();
            p.GenPatternFullfill("Maxim");
            p.GenPatternFullfill("Maxim", "Comic Sans MS", 48, 255, 40);
            p.GenPatternFullfill("Maxim", "Arial", 56, 128, 40, 100, 100, FontStyle.Regular);
            p.RandomWatermarkA3("Maxim", 100, 5, true, true);
            p.GenPatternChess("Maxim");
            Console.ReadKey();

        }

        public void GenPatternFullfill(string text)
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
                PointF position = new PointF(10, 10);

                // Set the rotation angle of the watermark
                float angle = 25;

                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);


                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + 10; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;

                        // Draw the watermark text on the image
                        graphics.DrawString(text, font, brush, position);
                    }
                }
            }

            // Save the output image to a file
            image.Save("outputfulfillm1.png", ImageFormat.Png);
        }

        public void GenPatternFullfill(string text, string fontname, int fontsize, int alpha, int angle)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(fontname, fontsize, FontStyle.Regular);//Arial
                Brush brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255));
                PointF position = new PointF(0, 0);
                if (angle < 90)
                {
                    position = new PointF(image.Width, image.Height);

                }
                else
                {
                    position = new PointF(image.Width / 2, image.Height / 2);
                }

                // Set the rotation angle of the watermark


                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);


                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + 30; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;

                        // Draw the watermark text on the image
                        graphics.DrawString(text, font, brush, position);
                    }
                }
            }

            // Save the output image to a file
            image.Save("outputfullfillm2.png", ImageFormat.Png);
        }

        public void GenPatternFullfill(string text, string fontname, int fontsize, int alpha, int angle, int widthbetween, int heightbetween, FontStyle fontstyle)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(fontname, fontsize, fontstyle);
                Brush brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255));
                PointF position = new PointF(10, 10);

                // Set the rotation angle of the watermark


                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);

                size.Width = size.Width + widthbetween;
                size.Height = size.Height + heightbetween;
                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
                int columns = (int)Math.Ceiling(image.Width / size.Width);

                // Draw the watermark text on the image
                for (int row = 0; row < rows + 10; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height - image.Height;

                        // Draw the watermark text on the image
                        graphics.DrawString(text, font, brush, position);
                    }
                }
            }

            // Save the output image to a file
            image.Save("outputfullfillm3.png", ImageFormat.Png);
        }
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
        public void RandomWatermarkA2(string text, int iterations, int sizing)
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
                        graphics.RotateTransform(30);
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
                image.Save("outputrandomA2m1.png", ImageFormat.Png);
                Console.WriteLine("Image with watermark generated!");
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

        }
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
                        int angle = 0;
                        if (randangle == true)
                        {
                            angle = random.Next(randbegin, randend);
                            graphics.RotateTransform(angle);
                        }
                        graphics.RotateTransform(30);
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
                            graphics.RotateTransform(random.Next(randbegin,randend));
                            
                        }
                       
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
                        if (randangle == true)
                        {
                            graphics.RotateTransform(random.Next(randbegin, randend));

                        }
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
        public void RandomWatermarkA3(string text, int frequency, int marksperrow, bool adjacentcol, bool adjacentrow)
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
                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Set the rotation angle of the watermark
                float angle = 0;

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
                for (int row = 0; row < rows * 3; row++)
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
            image.Save("outputA3m1.png", ImageFormat.Png);
        }
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
                if (randangle == true)
                {
                    angle = rand.Next(randbegin, randend);//getting random angle
                }
                transform.RotateAt(angle, position);
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
                            if (randangle == true)
                            {
                                angle = rand.Next(0, 0);
                                
                                angle = rand.Next(randbegin, randend);//getting random angle
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

        public void GenPatternChess(string text)
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
                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Set the rotation angle of the watermark
                float angle = 0;

                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);
               
                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();
                

                // Draw the watermark text on the image
                for (int row = 0; row < rows; row++)
                {
                    

                    for (int column = 0; column < columns; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height;// - image.Height;
                        
                        
                            if ((row + column) % 2 == 0)
                            {
                                continue;
                            }

                        // Draw the watermark text on the image
                        
                            graphics.DrawString(text, font, brush, position);
                    }


                }
            }

            // Save the output image to a file
            image.Save("outputchessm1.png", ImageFormat.Png);
        }
        public void GenPatternChess(string text, string fontname, int fontsize,FontStyle fontstyle)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(fontname, fontsize, fontstyle);
                Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);
                PointF position = new PointF(image.Width / 2, image.Height / 2);

                // Set the rotation angle of the watermark
                float angle = 0;

                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);

                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();


                // Draw the watermark text on the image
                for (int row = 0; row < rows; row++)
                {


                    for (int column = 0; column < columns; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height;// - image.Height;


                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }

                        // Draw the watermark text on the image

                        graphics.DrawString(text, font, brush, position);
                    }


                }
            }

            // Save the output image to a file
            image.Save("outputchessm2.png", ImageFormat.Png);
        }
        public void GenPatternChess(string text, int angle, int alpha, int widthbetween,int heightbetween)
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
                Brush brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255));
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

                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();


                // Draw the watermark text on the image
                for (int row = 0; row < rows; row++)
                {


                    for (int column = 0; column < columns; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height;// - image.Height;


                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }

                        // Draw the watermark text on the image

                        graphics.DrawString(text, font, brush, position);
                    }


                }
            }

            // Save the output image to a file
            image.Save("outputchessm3.png", ImageFormat.Png);
        }
        public void GenPatternChess(string text, string fontname, int fontsize, FontStyle fontstyle, int widthbetween, int heightbetween, int angle, int alpha)
        {
            Image image = Image.FromFile("input.jpg");
            using (Graphics graphics = Graphics.FromImage(image))
            {
                // Set the text rendering mode to clear type
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                // Set the interpolation mode to high quality
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Set the font, color, and position of the watermark text
                Font font = new Font(fontname, fontsize, fontstyle);
                Brush brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255));
                // Measure the size of the watermark text
                SizeF size = graphics.MeasureString(text, font);
                size.Width = size.Width + widthbetween;
                size.Height = size.Height + heightbetween;
                PointF position = new PointF(image.Width / 2, image.Height / 2);

               
                

                // Set the transformation matrix for the watermark
                Matrix transform = new Matrix();
                transform.RotateAt(angle, position);
                graphics.Transform = transform;

                // Calculate the number of rows and columns of watermarks
                int rows = (int)Math.Ceiling(image.Height / size.Height);

                int columns = (int)Math.Ceiling(image.Width / size.Width);
                Random random = new Random();


                // Draw the watermark text on the image
                for (int row = 0; row < rows; row++)
                {


                    for (int column = 0; column < columns; column++)
                    {

                        // Calculate the position of the watermark
                        position.X = column * size.Width;
                        position.Y = row * size.Height;// - image.Height;


                        if ((row + column) % 2 == 0)
                        {
                            continue;
                        }

                        // Draw the watermark text on the image

                        graphics.DrawString(text, font, brush, position);
                    }


                }
            }

            // Save the output image to a file
            image.Save("outputchessm4.png", ImageFormat.Png);
        }
    }
}
   
