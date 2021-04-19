
using System;

public class Key
{
    private int bookID;
    public Key()
    {
        Random rnd = new Random();
        this.bookID = rnd.Next(1, 1_000_000);
    }

    public Key(int bookID)
    {
        this.bookID = bookID;
    }

    public int GetBookID
    {
        get
        {
            return bookID;
        }
    }
    public override string ToString()
    {
        return $"{bookID.ToString("000000")}";
    }
}
