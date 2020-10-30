using System;
using static System.Console;
using static System.Math;


namespace part_1
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter x: ");
            int x = int.Parse(ReadLine());
            WriteLine("Enter y: ");
            int y = int.Parse(ReadLine());
            WriteLine("Enter z: ");
            int z = int.Parse(ReadLine());

            double a = Pow(x, z) + Log10(Abs(5 * x + y));
            double b = 1 / (a * a) + (Sqrt(x - y)) / z;

            if ((5 * x + y == 0) || (x < y))
            {
                WriteLine("Change your Y or X");
            }
            else if (z == 0)
            {
                WriteLine("Z can't be 0");
            }
            else if (a == 0)
            {
                WriteLine("A can't be 0");
            }
            else
            {
                WriteLine("Number a : {0}", a);
                WriteLine("Number b : {0}", b);
            }
        }
    }
}
