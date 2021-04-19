
public interface HashInterface
{
    bool InsertEntry(Key key, Value value);
    bool RemoveEntry(Key key);
    bool FindEntry(Key key);
    bool Rehashing();
    int GetHash(Key key);
    int HashCode(Key key, int i);
}
