using System;
using static System.Console;

namespace lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter size of matrix: ");
            int n = int.Parse(ReadLine());
            int[,] a = new int[n, n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = random.Next(10, 100);
                    Write(" {0}", a[i, j]);
                }
                WriteLine();
            }
            WriteLine();
            Upper(n, a);
            WriteLine();
            Bottom(n, a);
            WriteLine();
            double avg = Average(n, a);
            WriteLine("Average of the elements of the side diagonal: {0:F2}", avg);
            WriteLine();
            MoreThanAvg(n, a, (int)avg);
            WriteLine();

        }

        //верхній прохід
        static void Upper(int n, int[,] up)
        {
            int max;
            int k;
            int s;  //i
            int r;  //j
            int A;
            int B;
            int C;
            int imax;
            int jmax;
            imax = 0;
            jmax = 0;
            max = up[0, 0];
            k = (n * n - n) / 2;
            s = n - 2;
            r = 0;
            A = 0;
            B = n;
            C = n - 1;

            do
            {
                do                                                    //горизонтальний прохід
                {
                    k = k - 1;

                    if (k < 0)
                    {
                        MaxOut(max, imax, jmax);
                        return;
                    }
                    else
                    {
                        int max1 = max;
                        max = MaxSear(max, up, r, s);
                        if (max1 < max)
                        {
                            imax = r;
                            jmax = s;
                        }
                        Write(" {0}", up[r, s]);
                    }
                    s = s - 1;
                }
                while (s >= A);

                s = s + 1;
                B = B - 2;

                do                                                              //вертикальний прохід
                {
                    r = r + 1;
                    k = k - 1;
                    if (k < 0)
                    {
                        MaxOut(max, imax, jmax);
                        return;
                    }
                    else
                    {
                        int max1 = max;
                        max = MaxSear(max, up, r, s);
                        if (max1 < max)
                        {
                            imax = r;
                            jmax = s;
                        }
                        Write(" {0}", up[r, s]);
                    }
                }
                while (r < B);

                C = C - 2;

                do                                                          //діагоняльний прохід
                {
                    s = s + 1;
                    r = r - 1;
                    k = k - 1;
                    if (k < 0)
                    {
                        MaxOut(max, imax, jmax);
                        return;
                    }
                    else
                    {
                        int max1 = max;
                        max = MaxSear(max, up, r, s);
                        if (max1 < max)
                        {
                            imax = r;
                            jmax = s;
                        }
                        Write(" {0}", up[r, s]);
                    }
                }
                while (s < C);

                s = s - 1;
                A = A + 1;
            }
            while (true);

        }

        static int MaxSear(int max, int[,] a, int i, int j)
        {
            if (max < a[i, j])
            {
                max = a[i, j];
                //imax = i
                //jmax = j
            }
            return max;
        }

        static void MaxOut(int max, int i, int j)
        {
            WriteLine();
            WriteLine("The maximum element of the top of the matrix: {0} coordinates:[{1},{2}]", max, i + 1, j + 1);   // із системи [n-1хn-1] в [nхn]
        }



        static double Average(int n, int[,] a)
        {
            double avg;
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j == n - 1)
                    {
                        sum = sum + a[i, j];
                    }
                }
            }
            avg = sum / n;
            return avg;
        }

        //нижній прохід
        static void Bottom(int n, int[,] bt)
        {
            int k;
            int s;  //i
            int r;  //j
            int A;
            int B;
            int C;
            k = (n * n - n) / 2;
            s = n - 1;
            r = 0;
            A = n - 1;
            B = -1;
            C = n - 1;

            for (int i = n - 1; i >= 0; i += -1)              //прохід по побічній діагоналі
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j == n - 1)
                    {
                        Write(" {0}", bt[i, j]);
                    }
                }
            }

            do
            {
                do                                              //вертикальний прохід
                {
                    r = r + 1;
                    k = k - 1;
                    if (k < 0)
                    {

                        return;
                    }
                    else
                    {

                        Write(" {0}", bt[r, s]);
                    }
                }
                while (r < A);

                B = B + 2;
                do                                              //горизонтальний прохід
                {
                    k = k - 1;
                    s = s - 1;
                    if (k < 0)
                    {
                        return;
                    }
                    else
                    {
                        Write(" {0}", bt[r, s]);
                    }
                }
                while (s > B);

                C = C - 1;

                do                                      //діагоняльний прохід
                {
                    s = s + 1;
                    r = r - 1;
                    k = k - 1;
                    if (k < 0)
                    {
                        return;
                    }
                    else
                    {
                        Write(" {0}", bt[r, s]);
                    }
                }
                while (s < C);

                A = A - 1;


            } while (true);
        }

        //вивід і знаходження елементів більше середнього
        static void MoreThanAvg(int n, int[,] b, int avg)
        {   WriteLine("Elements under side diagonal more than average: ");
            for (int i = 0; i < n; i += 1)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j >= n - 1)
                    {
                        if (b[i, j] > avg)
                        {
                            Write(" {0} [{1},{2}]", b[i, j], i+1, j+1);
                        }
                    }
                }
            }
        }
    }
}
