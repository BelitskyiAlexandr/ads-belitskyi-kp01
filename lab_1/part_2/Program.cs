using System;
using static System.Console;


namespace part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter n: ");
            double n = double.Parse(ReadLine());

            if ((n <= 0) || (n % 1 != 0))
            {
                WriteLine("n must be positive and integer");
            }
            else
            {
                for (ulong i = 0; i <= n; i++)
                {
                    if (IsItTrue(i))
                    {
                        Write("Number {0} is ending of his square: {1}", i, i * i);
                        WriteLine();
                    }

                }
            }
        }



        static ulong amount(ulong x)
        {
            ulong am = 0;
            while (x > 0)
            {
                am += 1;
                x = x / 10;
            }
            return am;
        }


        static ulong nums(ulong x)
        {
            ulong nu = 1;
            for (ulong i = 1; i <= x; i += 1)
            {
                nu = nu * 10;
            }
            return nu;
        }



        static bool IsItTrue(ulong x)
        {
            if (x == (x * x) % nums(amount(x)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
