using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        var tree = new BinaryHeap();

        Console.WriteLine("Choose mode (examlpe/interactive): ");
        string mode = Console.ReadLine();
        if (mode == "example")
        {
            Console.WriteLine("Add values step by step: `4` `5` `3` `7` `2` `9` `8`");
            int[] arrTree = new int[] { 4, 5, 3, 7, 2, 9, 8 };                 //4 5 3 7 2 9 8 


            Console.WriteLine("Current binary tree: ");
            tree.Display(arrTree);

            Console.WriteLine("Is binary heap: {0}\n", tree.isHeap(arrTree));

            Console.WriteLine("Do heapify: ");
            tree = tree.Reorganize(arrTree, tree);
            tree.Display(tree.tree);


        }
        else if (mode == "interactive")
        {
            int[] arr = new int[0];
            PrintCommands();
            while (true)
            {
                Console.Write("Enter value or command: ");
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    Console.WriteLine("Ending processing...");
                    break;
                }
                else if (command == "commands")
                {
                    PrintCommands();
                }
                else if (int.TryParse(command, out int num))
                {
                    arr = Expand(arr);
                    arr[arr.Length - 1] = num;
                    tree.Display(arr);
                }
                else if (command == "heapify")
                {
                    tree = tree.Reorganize(arr, tree);
                    arr = tree.tree;
                    Console.WriteLine("Tree was reorganized: ");
                    tree.Display(tree.tree);
                }
                else if (command == "isHeap")
                {
                    Console.WriteLine("Is binary heap: {0}", tree.isHeap(arr));
                }
                else
                {
                    Console.WriteLine("Error: Enter integer number");
                }
            }
        }
        else
        {
            Console.WriteLine("Error: Unknown mode");
        }
    }

    private static int[] Expand(int[] arr)
    {
        int oldCapacity = arr.Length;
        int[] oldArray = arr;
        arr = new int[oldCapacity + 1];
        System.Array.Copy(oldArray, arr, oldCapacity);
        return arr;
    }

    public static void PrintCommands()
    {
        Console.WriteLine(@"
Commands:
{integer}       - add value to binary tree
isHeap          - check if tree is binary heap
heapify         - transform binary tree to binary heap
commands        - print helpbox
exit            - exit program   
        ");
    }
}

