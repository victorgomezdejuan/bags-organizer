namespace BagsOrganizer;

internal class Bag
{
    public List<string> Items { get; }

    public Bag(int capacity)
        => Items = new(capacity);

    public void AddItem(string item)
        => Items.Add(item);

    internal bool HasFreeSpace()
        => Items.Count < Items.Capacity;
}
