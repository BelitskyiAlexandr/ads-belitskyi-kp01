
public class Entry
{
    public Key key;

    public Value value;

    public Entry(string input)
    {
        Value value = new Value(null, null, 0);
        this.key = new Key();
        this.value = Value.CreateValue(input);
    }
    public Entry(Key key, Value value)
    {
        this.key = key;
        this.value = value;
    }
    public Entry(Value value)
    {
        this.key = new Key();
        this.value = value;
    }

    public override string ToString()
    {
        return $"Key: {key} -> Value: {value}";
    }
}
