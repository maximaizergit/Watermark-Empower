
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace WatermarkImage
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> points = new List<string>();
            Console.WriteLine("Please enter the path of image");
            string imagePath = Console.ReadLine();
            Console.WriteLine("Please enter the path of watermark text");
            string textPath = Console.ReadLine();
            Console.WriteLine("Please enter the output image name");
            string outputImageName = Console.ReadLine();
            for (; ; )
            {
                try
                {
                    //read image and text
                    Image image = Image.FromFile(imagePath);
                    StreamReader textFile = new StreamReader(textPath);
                    string text = textFile.ReadToEnd();
                    textFile.Close();
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
                        Program program = new Program();
                        points.Add("x:" + x + " " + "y:" + y);


                        tempx = x;
                        tempy = y;

                        //rotate 60 degrees
                        graphics.TranslateTransform(x, y);
                        graphics.RotateTransform(0);
                        //draw text 
                        graphics.DrawString(text, font, brush, 0, 0);
                        //reset to origin
                        graphics.ResetTransform();
                    }
                    //save image
                    image.Save(outputImageName, ImageFormat.Png);
                    Console.WriteLine("Image with watermark generated!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
                Console.WriteLine("Info:");
                foreach (string p in points) { Console.WriteLine(p); }
                Console.ReadKey();

            }
        }

    }
}


