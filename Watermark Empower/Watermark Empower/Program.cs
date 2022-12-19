using System;
using System.Drawing;
using WatermarkGenerator;

namespace Watermark_Empower
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Generator gen = new Generator();
            gen.GenPatternFullfill("Maxim",0);
            gen.GenPatternFullfill("Maxim",0);
           
            
            gen.RandomWatermarkA1("Maxim");
           
            gen.RandomWatermarkA2("Maxim", 10, 3, true,0, 40);
            
            gen.RandomWatermarkA3("Maxim", 50, 5,true,true,30,50,50);
            gen.RandomWatermarkA3("Maxim",100, 5,true,true,true,0,40,0,0);
            gen.GenPatternChess("Maxim",0);
           
            gen.GenPatternChess("Maxim", "Comic Sans MS", 24, FontStyle.Bold, 100, 100, 30, 128);
            Console.ReadKey();
        }
    }
}
