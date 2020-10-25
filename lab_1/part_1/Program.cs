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

            if ((5 * x + y == 0) || (z == 0) || (a == 0) || ((x < y) && (0 <= x)))
            {
                WriteLine("Your numbers are wrong. Please, change variable value.");
            }
            else
            {


                WriteLine("Number a : {0}", a);
                WriteLine("Number b : {0}", b);
            }
        }
    }
}
