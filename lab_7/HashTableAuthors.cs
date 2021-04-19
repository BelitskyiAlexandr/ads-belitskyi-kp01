
using System;

public class HashTableAuthors
{
    private Node[] table;
    private double loadness;
    private Int64 size;
    private Int64 sizeAllNonNull;
    private int numberOfResize;

    public HashTableAuthors()
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
        public AuthorEntry entry;
        public bool state;

        public Node(AuthorEntry value)
        {
            this.entry = value;
            this.state = true;
        }
    }
    public bool FindAllBooks(string author)
    {
        Int64 inputHash = GetHash(author);
        bool flag = false;
        int probs = 0;
        for (int i = 0; i < table.Length; i++)
        {
            if (table[i].entry == null)
                continue;
            Int64 currentHash = GetHash(table[i].entry.author);
            if (currentHash == inputHash && table[i].state)
            {
                probs++;
            }
        }
        int counter = 0;
        for (int i = 0; i < table.Length; i++)
        {
            Int64 index = HashCode(author, i);
            if (table[index].entry == null)
                continue;
            Int64 currentHash = GetHash(table[index].entry.author);
            if (currentHash == inputHash && table[index].state)
            {
                if (probs == counter)
                    break;
                Console.WriteLine(table[index].entry);
                flag = true;
                counter++;
            }
        }
        return flag;
    }

    public bool InsertEntry(string author, string book)
    {
        if (LoadnessFactor(size + 1))
        {
            Console.WriteLine($"`HashtableAuthors` Tip: Loadness factor is `{loadness}` but recomended is `< 0.5`. Resize...");
        }
        else if (sizeAllNonNull > size)
        {
            Console.WriteLine($"`HashtableAuthors` Tip: Number of deleted elements is too much. Rehashing...");
            Rehashing();
        }
        Int64 firstDeleted = -1;

        AuthorEntry entry = new AuthorEntry(author, book);
        Int64 inputHash = GetHash(author);
        for (int i = 0; i < table.Length; i++)
        {
            Int64 index = HashCode(entry.author, i);
            if (table[index].entry == null && firstDeleted == -1)
            {
                firstDeleted = index;
                break;
            }
            if (table[index].entry == null)
                break;
            Int64 currentHash = GetHash(table[index].entry.author);
            if (currentHash == inputHash && table[index].state)
            {
                if (table[index].entry.title == book)
                {
                    //Console.WriteLine($"`HashtableAuthors` Error: Book {book} already exists");
                    return false;
                }
            }
            if (!table[index].state && firstDeleted == -1)
            {
                firstDeleted = index;
            }
        }
        if (firstDeleted == -1)
        {
            Int64 index = HashCode(entry.author, 0);

            table[index] = new Node(entry);
            sizeAllNonNull++;
        }
        else
        {
            table[firstDeleted].entry = entry;
            table[firstDeleted].state = true;
        }
        size++;
        Console.WriteLine($"`HashtableAuthors` Tip: Book {book} was added");
        return true;
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
                InsertEntry(oldArray[i].entry.author, oldArray[i].entry.title);
            }
        }
        return true;
    }
    private void Resize()
    {
        int oldCapacity = this.table.Length;
        Node[] oldArray = this.table;
        this.table = new Node[(int)Math.Pow(2, numberOfResize + 1) - 1];
        this.numberOfResize++;

        Rehashing();
    }
    public Int64 GetHash(string author)         //check
    {
        const int p = 53;
        Int64 hash = 0;
        foreach (var c in author)
        {
            int x = (int)(c - 'A' + 1);
            hash = (hash * p + x);
        }
        return hash;
    }

    public Int64 HashCode(string author, int i)
    {
        Int64 hash = GetHash(author);
        Int64 index = (hash + i * i) % table.Length;
        return index;
    }

    public bool LoadnessFactor(Int64 size)
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
