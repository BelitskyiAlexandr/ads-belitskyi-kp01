using System;
using static System.Console;

namespace lab_3
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter number of strings:");
            string Str = ReadLine();
            int n;
            bool isNum = int.TryParse(Str, out n);
            if (isNum)
            // действие если строка - число
            {
                if (n <= 0)
                {
                    WriteLine("Please enter positive integer number");
                }
                else
                {
                    WriteLine("Enter number of columns:");
                    string Str1 = ReadLine();
                    int m;
                    bool isNum1 = int.TryParse(Str1, out m);
                    if (isNum1)
                    // действие если строка - число
                    {
                        if (m <= 0)
                        {
                            WriteLine("Please enter positive integer number");
                        }
                        else
                        {
                            //создание и заполнение массива
                            int[,] a = new int[n, m];
                            Random random = new Random();
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < m; j++)
                                {
                                    a[i, j] = random.Next(10, 100);
                                }
                            }
                            WriteLine();

                            //нахождение минимального в строке и его окрас
                            int[] icoordinates = new int[n];
                            int[] jcoordinates = new int[n];
                            int[] minelem = new int[n];
                            int[,] colored = new int[n, m];
                            for (int i = 0; i < n; i++)
                            {
                                int minLine = a[i, 0];
                                int jmin = 0;
                                for (int j = 0; j < m; j++)
                                {
                                    if (a[i, j] < minLine)
                                    {
                                        minLine = a[i, j];
                                        jmin = j;
                                    }
                                }
                                for (int j = 0; j < m; j++)
                                {
                                    Write(" ");
                                    if ((jmin == j))
                                    {
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        icoordinates[i] = i;
                                        jcoordinates[i] = j;
                                        minelem[i] = a[i, j];
                                        colored[i, j] = 1;
                                    }
                                    Write("{0}", a[i, j]);
                                    Console.ResetColor();
                                }
                                WriteLine();
                            }


                            //блок проверочных выводов
                            WriteLine();
                            /*
                            for (int i = 0; i < n; i++)
                                Write(" {0}", minelem[i]);
                            WriteLine();

                            for (int i = 0; i < n; i++)
                                Write(" {0}", jcoordinates[i]);
                            WriteLine();

                            for (int i = 0; i < n; i++)
                                Write(" {0}", icoordinates[i]);
                            WriteLine();
                            */

                            minelem = ShakerSort(minelem);
                            jcoordinates = ShakerSortCoordinates(jcoordinates, icoordinates);

                            /*   WriteLine();
                               for (int i = 0; i < n; i++)
                                   Write(" {0}", minelem[i]);
                               WriteLine();

                               for (int i = 0; i < n; i++)
                                   Write(" {0}", jcoordinates[i]);
                               WriteLine();

                               for (int i = 0; i < n; i++)
                                   Write(" {0}", icoordinates[i]);
                               WriteLine();
                               */
                            //


                            WriteLine();

                            //переворот и проход столбцами
                            int k = 0;
                            for (int i = 0; i < m; i++)
                            {
                                for (int j = 0; j < n; j++)
                                {
                                   // Write(" ");
                                    if (k != n)
                                    {
                                        if ((i == jcoordinates[k] && (j == icoordinates[k])))
                                        {
                                            //Console.BackgroundColor = ConsoleColor.Blue;
                                            a[j, i] = minelem[k];
                                            k++;
                                        }
                                    }
                                   // Write("{0}", a[j, i]);
                                  //  Console.ResetColor();
                                }
                              //  WriteLine();
                            }
                            WriteLine();


                            //вывод финальной матрицы
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < m; j++)
                                {
                                    Write(" ");
                                    if (colored[i, j] == 1)
                                    {
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                    }
                                    Write("{0}", a[i, j]);
                                    ResetColor();
                                }
                                WriteLine();
                            }
                        }
                    }

                    else
                    // действие если строка - не число
                    {
                        WriteLine("Please enter positive integer number");

                    }
                }
            }

            else
            // действие если строка - не число
            {
                WriteLine("Please enter positive integer number");

            }
        }

        //метод обмена элементов
        static void Swap(ref int e1, ref int e2)
        {
            int buf = e1;
            e1 = e2;
            e2 = buf;
        }

        //сортировка перемешиванием
        static int[] ShakerSort(int[] array)
        {
            for (int i = 0; i < array.Length / 2; i++)
            {
                bool Flag = false;

                //проход слева направо
                for (int j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        Flag = true;
                    }
                }

                //проход справа налево
                for (int j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1] > array[j])
                    {
                        Swap(ref array[j - 1], ref array[j]);
                        Flag = true;
                    }
                }

                //если обменов не было
                if (!Flag)
                {
                    break;
                }
            }
            return array;
        }

        //сортировка перемешиванием координат
        static int[] ShakerSortCoordinates(int[] col, int[] str)
        {
            for (int i = 0; i < col.Length / 2; i++)
            {
                bool Flag = false;

                //проход слева направо
                for (int j = i; j < col.Length - i - 1; j++)
                {
                    if (col[j] > col[j + 1])
                    {
                        Swap(ref col[j], ref col[j + 1]);
                        Swap(ref str[j], ref str[j + 1]);
                        Flag = true;
                    }
                }

                //проход справа налево
                for (int j = col.Length - 2 - i; j > i; j--)
                {
                    if (col[j - 1] > col[j])
                    {
                        Swap(ref col[j - 1], ref col[j]);
                        Swap(ref str[j - 1], ref str[j]);
                        Flag = true;
                    }
                }

                //если обменов не было
                if (!Flag)
                {
                    break;
                }
            }
            return col;
        }
    }
}
