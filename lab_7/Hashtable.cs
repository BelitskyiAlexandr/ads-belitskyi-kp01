
using System;

public class Hashtable : HashInterface
{
    private Node[] table;
    private double loadness;
    private int size;
    private int sizeAllNonNull;
    private int numberOfResize;

    public Hashtable()
    {
        this.table = new Node[31];
        this.loadness = 0.0;
        this.size = 0;
        this.sizeAllNonNull = 0;
        this.numberOfResize = 4;
    }

    public Node[] GetTable
    {
        get
        {
            return table;
        }
    }

    public struct Node
    {
        public Entry entry;
        public bool state;

        public Node(Entry value)
        {
            this.entry = value;
            this.state = true;
        }
    }
    public bool InsertEntry(Key key, Value value)
    {
        if (LoadnessFactor(size + 1))
        {
            Console.WriteLine($"`Hashtable` Tip: Loadness factor is `{loadness}` but recomended is `< 0.5`. Resize...");
        }
        else if (sizeAllNonNull > size)
        {
            Console.WriteLine($"`Hashtable` Tip: Number of deleted elements is too much. Rehashing...");
            Rehashing();
        }
        int firstDeleted = -1;

        Entry entry = ChooseEntry(key, value);
        for (int i = 0; i < table.Length; i++)
        {
            int index = HashCode(entry.key, i);
            if (table[index].entry == null && firstDeleted == -1)
            {
                firstDeleted = index;
                break;
            }
            if (table[index].entry == null)
                break;
            if (table[index].entry.key == entry.key && table[index].state && key != null)
            {
                table[index].entry.value = value;
                Console.WriteLine($"`Hashtable` Tip: Value under key {entry.key} was updated");
                return true;
            }
            else if (table[index].entry.key == entry.key && table[index].state && key == null)
            {
                Console.WriteLine($"`Hashtable` Tip: Key {entry.key} was generated but this key already exists. Please, try again");
                return false;
            }
            if (!table[index].state && firstDeleted == -1)
            {
                firstDeleted = index;
            }
        }
        if (firstDeleted == -1)
        {
            int index = HashCode(entry.key, 0);

            table[index] = new Node(entry);
            sizeAllNonNull++;
        }
        else
        {
            table[firstDeleted].entry = entry;
            table[firstDeleted].state = true;
        }
        size++;
        return true;
    }

    private Entry ChooseEntry(Key key, Value value)
    {
        if (key == null)
        {
            Entry temp = new Entry(value);
            return temp;
        }
        else
        {
            Entry temp = new Entry(key, value);
            return temp;
        }
    }

    public bool RemoveEntry(Key key)
    {
        for (int i = 0; i < table.Length; i++)
        {
            int index = HashCode(key, i);
            if (table[index].entry == null)
                continue;
            if (table[index].entry.key.GetBookID == key.GetBookID)
            {
                table[index].state = false;
                size--;
                return true;
            }
        }
        return false;
    }

    public bool FindEntry(Key key)
    {
        for (int i = 0; i < table.Length; i++)
        {
            int index = HashCode(key, i);
            if (table[index].entry == null)
                continue;
            if (table[index].entry.key.GetBookID == key.GetBookID && table[index].state)
            {
                Console.WriteLine(table[index].entry);
                return true;
            }
        }
        return false;
    }


    public bool Rehashing()
    {
        Node[] oldArray = this.table;
        //TODO rehash
        this.sizeAllNonNull = 0;
        for (int i = 0; i < oldArray.Length; i++)
        {
            if (oldArray[i].state && oldArray[i].entry != null)
            {
                InsertEntry(oldArray[i].entry.key, oldArray[i].entry.value);
            }
        }
        return true;
    }
    private void Resize()
    {
        // 15 -> 2^(3+1)-1 - non - prime num            //намагався знайти якусь закономірність для, хоч і малої, оптимізації
        // 31 -> 2^(4+1)-1 - prime num
        // 127 -> 2^(6+1)-1 - prime
        // 8191 -> 2^(12+1)-1 - prime
        int oldCapacity = this.table.Length;
        Node[] oldArray = this.table;
        this.table = new Node[(int)Math.Pow(2, numberOfResize + 1) - 1];
        this.numberOfResize++;

        Rehashing();
    }
    public int GetHash(Key key)
    {
        return key.GetBookID;
    }

    public int HashCode(Key key, int i)
    {
        int hash = GetHash(key);
        int index = (hash + i * i) % table.Length;
        return index;
    }

    public bool LoadnessFactor(int size)
    {
        this.loadness = size / this.table.Length;
        if (loadness >= 0.5)
        {
            Resize();
            return true;
        }
        return false;
    }

    public void PrintTable()
    {
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i].entry == null)
                continue;
            Console.WriteLine(table[i].entry);
        }
        Console.WriteLine();
    }
}
