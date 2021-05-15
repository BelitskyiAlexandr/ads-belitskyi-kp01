using System;
using System.Collections.Generic;
using System.Text;

class BinaryHeap
{
    public int[] tree;
    private int size;

    public BinaryHeap()
    {
        tree = new int[0];
        size = 0;
    }

    public void Print(string indent, bool last, int elem)
    {
        Console.Write(indent);
        if (last)
        {
            Console.Write("\\-");
            indent += " ";
        }
        else
        {
            Console.Write("|-");
            indent += "| ";
        }
        Console.WriteLine(tree[elem]);

        for (int i = 0; i < size; i++)
        {
            Print(indent, i == size - 1, i);
        }
    }

    private void Expand()
    {
        int oldCapacity = this.tree.Length;
        int[] oldArray = this.tree;
        this.tree = new int[oldCapacity + 1];
        System.Array.Copy(oldArray, this.tree, oldCapacity);
    }

    public BinaryHeap Reorganize(int[] arr, BinaryHeap tree)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            tree.Add(arr[i]);
        }
        return tree;
    }
    public void AddValue(int value)
    {
        if (size == tree.Length)
        {
            Expand();
        }
        tree[size] = value;
        size++;
    }

    public void Add(int value)
    {
        AddValue(value);
        int i = size - 1;
        int parent = (i - 1) / 2;

        while (i > 0 && tree[parent] < tree[i])
        {
            int buffer = tree[i];
            tree[i] = tree[parent];
            tree[parent] = buffer;

            i = parent;
            parent = (i - 1) / 2;
        }
    }

    public void Heapify(int i)
    {
        int leftChild;
        int rightChild;
        int largestChild;

        while (true)
        {
            leftChild = 2 * i + 1;
            rightChild = 2 * i + 2;
            largestChild = i;

            if (leftChild < size && tree[leftChild] > tree[largestChild])
                largestChild = leftChild;

            if (rightChild < size && tree[rightChild] > tree[largestChild])
                largestChild = rightChild;

            if (largestChild == i)
            {
                break;
            }

            int buffer = tree[i];
            tree[i] = tree[largestChild];
            tree[largestChild] = buffer;
            i = largestChild;

        }
    }

    public void Display(int[] arr)
    {
        StringBuilder sb = new StringBuilder();
        int max = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < Math.Pow(2, i) && j + Math.Pow(2, i) <= arr.Length; j++)
            {
                if (j > max)
                    max = j;
            }
        }


        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < Math.Pow(2, i) && j + Math.Pow(2, i) <= arr.Length; j++)
            {
                for (int r = 0; (r < max / ((int)Math.Pow(2, i))); r++)
                {
                    sb.Append(" ");
                }
                sb.Append(arr[j + (int)Math.Pow(2, i) - 1] + " ");
            }
            sb.Append("\n");
        }

        Console.WriteLine(sb.ToString());
    }

    public bool isHeap(int[] arr)
    {
        for (int i = 0; i <= (arr.Length - 2) / 2; i++)
        {
            if (arr[2 * i + 1] > arr[i])
                return false;


            if (2 * i + 2 < arr.Length && arr[2 * i + 2] > arr[i])
                return false;
        }
        return true;
    }
}