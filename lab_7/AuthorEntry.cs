
public class AuthorEntry
{
    public string author;
    public string title;

    public AuthorEntry(string author, string title)
    {
        this.author = author;
        this.title = title;
    }

    public override string ToString()
    {
        return $"Author: {author} -> Books: {title}";
    }
}
