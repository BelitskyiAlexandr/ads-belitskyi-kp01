using System;
using System.Collections.Generic;

public class Value
{
    public string title;
    public LinkedList<string> authors;
    public int yearOfPublishing;
    private string authorsString;

    public Value(string title, LinkedList<string> authors, int yearOfPublishing)
    {
        this.title = title;
        this.authors = authors;
        this.yearOfPublishing = yearOfPublishing;
        this.authorsString = ListToString(authors);
    }

    static public bool ParseValue(string input, out string[] components, out LinkedList<string> authorsList, out int year)
    {
        components = input.Split(';');
        authorsList = new LinkedList<string>();
        year = 0;
        if (components.Length != 3)
        {
            Console.WriteLine($"Error: check correctness of number components of input data. There are {components.Length} but expected - 3");
            return false;
        }
        string[] authors = components[1].Split(',');
        authorsList = new LinkedList<string>(authors);

        if (string.IsNullOrWhiteSpace(components[1]))
        {
            Console.WriteLine("Error: check correctness of input authors");
            return false;
        }
        if (int.TryParse(components[2], out year))
        {
            return true;
        }
        else
        {
            Console.WriteLine("Error: check correctness of input data. Can't transform publishing year");
            return false;
        }
    }

    static public Value CreateValue(string input)
    {
        string[] components = new string[3];
        int year;
        LinkedList<string> writers = new LinkedList<string>();
        if (ParseValue(input, out components, out writers, out year))
        {
            return new Value(components[0], writers, year);
        }
        return null;
    }

    private string ListToString(LinkedList<string> list)
    {
        string str = null;
        if (list == null)
            return str;
        var item = list.First;
        while (item != null)
        {
            if (item.Next == null)
            {
                str += item.Value;
            }
            else
            {
                str += item.Value + ", ";
            }
            item = item.Next;
        }
        return str;
    }

    public override string ToString()
    {
        return $"Title: {title}; Authors: {authorsString}; Year: {yearOfPublishing}";
    }
}
