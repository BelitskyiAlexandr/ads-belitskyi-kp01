using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var Hashtable = new Hashtable();
        var HashTableAuthors = new HashTableAuthors();
        PrintCommands();
        while (true)
        {
            Console.WriteLine("Enter command: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string command = Console.ReadLine();
            Console.ResetColor();
            if (command.StartsWith("add-"))
            {
                AddProccess(command, Hashtable, HashTableAuthors);
            }
            else if (command.StartsWith("find-"))
            {
                FindProccess(command, Hashtable);
            }
            else if (command.StartsWith("Find-all-books"))
            {
                FindAllBooksProcess(command, HashTableAuthors);
            }
            else if (command.StartsWith("remove-"))
            {
                RemoveProccess(command, Hashtable);
            }
            else if (command.StartsWith("print"))
            {
                PrintProccess(command, Hashtable, HashTableAuthors);
            }
            else if (command.StartsWith("exit"))
            {
                Console.WriteLine("Ending of proccessing...");
                break;
            }
            else
            {
                Console.WriteLine("Error: unknown command");
            }
        }
    }
    static void AddProccess(string command, Hashtable hashtable, HashTableAuthors hashTableAuthors)
    {
        string[] components = command.Split('-');

        if (components.Length == 2)
        {
            if (Value.ParseValue(components[1], out string[] ss, out LinkedList<string> list, out int year))
            {
                Key key = null;
                Value value = Value.CreateValue(components[1]);
                if (hashtable.InsertEntry(key, value))
                {
                    foreach (var author in list)
                    {
                        hashTableAuthors.InsertEntry(author, ss[0]);
                    }
                    Console.WriteLine("Tip: Operation was successful. New record was added");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error: check correctness of value");
                return;
            }
        }
        else if (components.Length == 3)
        {
            if (int.TryParse(components[1], out int intKey))
            {
                if (Value.ParseValue(components[2], out string[] ss, out LinkedList<string> list, out int year))
                {
                    Key key = new Key(intKey);
                    Value value = Value.CreateValue(components[2]);
                    if (hashtable.InsertEntry(key, value))
                    {
                        foreach (var author in list)
                        {
                            hashTableAuthors.InsertEntry(author, ss[0]);
                        }
                        Console.WriteLine("Tip: Operation was successful. Data was updated");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Error: check correctness of value");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error: check correctness of key");
                return;
            }
        }
        else
        {
            Console.WriteLine("Error: check correctness of command");
            return;
        }
    }

    static void FindProccess(string command, Hashtable hashtable)
    {
        string[] components = command.Split('-');
        if (components.Length != 2)
        {
            Console.WriteLine("Error: Check correctness of `find` command");
            return;
        }
        if (int.TryParse(components[1], out int intKey))
        {
            Key key = new Key(intKey);
            if (!hashtable.FindEntry(key))
            {
                Console.WriteLine($"Error: Value under key {key} did not find.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Error: check correctness of key");
            return;
        }
    }

    static void FindAllBooksProcess(string command, HashTableAuthors hashTableAuthors)
    {
        string[] components = command.Split('-');
        if (components.Length != 4)
        {
            Console.WriteLine("Error: Check correctness of `find-all-books` command");
            return;
        }
        hashTableAuthors.FindAllBooks(components[3]);

    }
    static void RemoveProccess(string command, Hashtable hashtable)
    {
        string[] components = command.Split('-');
        if (components.Length != 2)
        {
            Console.WriteLine("Error: Check correctness of `remove` command");
            return;
        }
        if (int.TryParse(components[1], out int intKey))
        {
            Key key = new Key(intKey);
            if (!hashtable.RemoveEntry(key))
            {
                Console.WriteLine($"Error: Cannot remove -> Value under key {key} did not find.");
                return;
            }
            else
            {
                Console.WriteLine("Tip: Removing was successful");
            }
        }
        else
        {
            Console.WriteLine("Error: check correctness of key");
            return;
        }
    }
    static void PrintProccess(string command, Hashtable hashtable, HashTableAuthors hashTableAuthors)
    {
        string[] components = command.Split('-');

        if (components.Length == 1)
        {
            Console.WriteLine("Main hashtable: ");
            for (int i = 0; i < hashtable.GetTable.Length; i++)
            {
                if (hashtable.GetTable[i].entry != null && hashtable.GetTable[i].state)
                {
                    Console.WriteLine(hashtable.GetTable[i].entry);
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Secondary hashtable: ");
            for (int i = 0; i < hashTableAuthors.GetTable.Length; i++)
            {
                if (hashTableAuthors.GetTable[i].entry != null && hashTableAuthors.GetTable[i].state)
                {
                    Console.WriteLine(hashTableAuthors.GetTable[i].entry);
                }
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        else if (components.Length == 2 && components[1] == "commands")
        {
            PrintCommands();
        }
        else
        {
            Console.WriteLine("Error: Check correctness of `print` command");
            return;
        }
    }

    static void PrintCommands()
    {
        Console.WriteLine(@"
Available commands to work with the program:

/c o m m a n d/   /f o r m a t/                 /e x e c u t i o n/
+------------------------------------------------------------------------------------------------------------------+
add             - add-{value}               -  adds the value {value} under unique key to the hashtable
add             - add-{key}-{value}         -  change the value under key {key}
find            - find-{key}                -  checks if the pair {key}-value exists in hashtable
Find            - Find-all-books-{author}   -  finds all books of {author}
remove          - remove-{key}              -  removes the pair {key}-value from hashtable
print           - print                     -  print hashtable
print           - print-commands            -  print commands sheet
exit            - exit                      -  exit the program
+------------------------------------------------------------------------------------------------------------------+
Value format -> {title};{author1},{author2};{year} 
            ");
    }
}

