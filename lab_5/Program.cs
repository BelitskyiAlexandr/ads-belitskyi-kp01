using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;

namespace lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Choose mode (example/interactive): ");
            string mode = ReadLine();
            if (mode == "interactive")
            {
                WriteLine("Enter N: ");
                string ns = ReadLine();
                WriteLine("Enter M: ");
                string ms = ReadLine();
                int n, m;
                if ((n = CheckInteger(ns)) <= 0)
                {
                    WriteLine("Error: N must be positive");
                    Environment.Exit(0);
                }
                if ((m = CheckInteger(ms)) <= 0)
                {
                    WriteLine("Error: M must be positive");
                    Environment.Exit(0);
                }
                LinkedList<int> list = new LinkedList<int>();
                for (int i = 0; i < n; i++)
                {
                    Write("Enter value: ");
                    string val = ReadLine();
                    int value = CheckInteger(val);
                    list.AddLast(value);
                }

                int size = 0;
                var unit = list.First;
                while (unit != null)
                {
                    if (unit.Value < 0)
                    {
                        while (true)
                        {
                            if ((unit.Next == null) || (unit.Next.Value > 0))
                            {
                                break;
                            }

                            unit = unit.Next;
                        }
                        size++;
                    }
                    unit = unit.Next;
                }

                PrintList(list);

                LinkedList<int> sortedList = TransferPositives(list);

                var item = list.First;
                int[] supArr = new int[list.Count];
                int k = 0;
                int[] colors = new int[size];
                int c = 0;
                while (item != null)
                {
                    if (item.Value < 0)
                    {
                        while (true)
                        {
                            colors[c] += 1;
                            supArr[k] = item.Value;
                            k++;
                            if ((item.Next == null) || (item.Next.Value > 0))
                            {
                                supArr = ReverseArray(supArr);
                                supArr = ChooseSort(supArr, m, k);
                                supArr = ReverseArray(supArr);
                                sortedList = AddArray(sortedList, supArr, k);
                                Array.Clear(supArr, 0, supArr.Length);
                                k = 0;
                                break;
                            }
                            item = item.Next;
                        }
                        c++;
                    }
                    item = item.Next;
                }
                PrintSortedList(sortedList, colors);
            }
            else if (mode == "example")
            {
                int m = 3;
                int[] exArray = new int[] { 0, -5, -4, -3, 3, -2, -1 };
                LinkedList<int> list = new LinkedList<int>(exArray);
                var item = list.First;
                int[] supArr = new int[list.Count];
                int k = 0;
                int[] colors = new int[2];
                int c = 0;
                LinkedList<int> sortedList = TransferPositives(list);
                PrintList(list);
                while (item != null)
                {
                    if (item.Value < 0)
                    {
                        while (true)
                        {
                            colors[c] += 1;
                            supArr[k] = item.Value;
                            k++;
                            if ((item.Next == null) || (item.Next.Value > 0))
                            {
                                supArr = ReverseArray(supArr);
                                supArr = ChooseSort(supArr, m, k);
                                supArr = ReverseArray(supArr);
                                sortedList = AddArray(sortedList, supArr, k);
                                Array.Clear(supArr, 0, supArr.Length);
                                k = 0;
                                break;
                            }
                            item = item.Next;
                        }
                        c++;
                    }
                    item = item.Next;
                }
                PrintSortedList(sortedList, colors);
            }
            else
            {
                WriteLine("Error: Check correctness of input data");
            }

        }

        static void PrintSortedList(LinkedList<int> list, int[] colors)
        {
            var item = list.First;
            while (item != null)
            {
                if ((item.Value < 0))
                {
                    for (int c = 0; c < colors.Length; c++)
                    {
                        for (int i = 0; i < colors[c]; i++)
                        {
                            if (c % 3 == 0) 
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Write("[{0}]", item.Value);
                                Console.ResetColor();
                            }
                            else if (c % 3 == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Write("[{0}]", item.Value);
                                Console.ResetColor();
                            }
                            else if (c % 3 == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Write("[{0}]", item.Value);
                                Console.ResetColor();
                            }
                            if (item.Next != null)
                            {
                                Write(" -> ");
                                item = item.Next;
                            }
                        }
                    }
                }
                else
                {
                    Write("[{0}]", item.Value);
                    if (item.Next != null)
                    {
                        Write(" -> ");
                    }
                }
                item = item.Next;
            }
            WriteLine();
        }

        static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
            {
                return array;
            }

            int middle = array.Length / 2;
            return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
        }

        static int[] Merge(int[] arr1, int[] arr2)
        {
            int ptr1 = 0, ptr2 = 0;
            int[] merged = new int[arr1.Length + arr2.Length];

            for (int i = 0; i < merged.Length; ++i)
            {
                if (ptr1 < arr1.Length && ptr2 < arr2.Length)
                {
                    merged[i] = arr1[ptr1] > arr2[ptr2] ? arr2[ptr2++] : arr1[ptr1++];
                }
                else
                {
                    merged[i] = ptr2 < arr2.Length ? arr2[ptr2++] : arr1[ptr1++];
                }
            }

            return merged;
        }
        static int[] ReverseArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = -arr[i];
            }
            return arr;
        }
        static LinkedList<int> AddArray(LinkedList<int> list, int[] arr, int k)
        {
            for (int i = 0; i < k; i++)
            {
                list.AddLast(arr[i]);
            }
            return list;
        }
        static int[] ChooseSort(int[] arr, int m, int k)
        {
            int[] array = new int[k];
            for (int i = 0; i < k; i++)
            {
                array[i] = arr[i];
            }
            if (k < m)
            {
                InsertionSort(array);
                for (int i = 0; i < k; i++)
                {
                    arr[i] = array[i];
                }
            }
            else
            {
                array = MergeSort(array);
                for (int i = 0; i < k; i++)
                {
                    arr[i] = array[i];
                }
            }
            return arr;
        }

        static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int cur = array[i];
                int j = i;
                while (j > 0 && cur < array[j - 1])
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = cur;
            }
        }
        static LinkedList<int> TransferPositives(LinkedList<int> list)
        {
            LinkedList<int> newList = new LinkedList<int>();
            int[] supArray = new int[list.Count];
            var item = list.First;
            int k = 0;
            while (item != null)
            {
                if (item.Value >= 0)
                {
                    supArray[k] = item.Value;
                    k++;
                }
                item = item.Next;
            }
            for (int i = k - 1; i >= 0; i--)
            {
                newList.AddLast(supArray[i]);
            }
            return newList;

        }
        static int CheckInteger(string n)
        {
            if (int.TryParse(n, out int nnum))
            {
                return nnum;
            }
            else
            {
                WriteLine("Error: Check the correctness of input data");
                Environment.Exit(0);
            }
            return 0;
        }

        static void PrintList(LinkedList<int> list)
        {
            var item = list.First;
            int p = 0;
            while (item != null)
            {
                if ((item.Value < 0) && (p == 0))
                {
                    while (true)
                    {
                        if (item.Next == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            break;
                        }
                        else if (item.Next.Value >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                            break;
                        }
                        else if (item.Next.Value < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                        }
                        item = item.Next;
                    }
                    p += 1;
                }
                else if ((item.Value < 0) && (p == 1))
                {
                    while (true)
                    {
                        if (item.Next == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            break;
                        }
                        else if (item.Next.Value >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                            break;
                        }
                        else if (item.Next.Value < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                        }
                        item = item.Next;
                    }
                    p += 1;
                }
                else if ((item.Value < 0) && (p == 2))
                {
                    while (true)
                    {
                        if (item.Next == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            break;
                        }
                        else if (item.Next.Value >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                            break;
                        }
                        else if (item.Next.Value < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Write("[{0}]", item.Value);
                            Console.ResetColor();
                            Write(" -> ");
                        }
                        item = item.Next;
                    }
                    p = 0;
                }
                if (item.Value >= 0)
                {
                    Write("[{0}]", item.Value);
                    if (item.Next != null)
                    {
                        Write(" -> ");
                    }
                }
                item = item.Next;
            }
            WriteLine();
        }
    }
}
