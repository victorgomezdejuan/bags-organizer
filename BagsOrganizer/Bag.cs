namespace BagsOrganizer;

internal class Bag
{
    public List<Item> Items { get; }
    public Category? Category { get; internal set; }

    public Bag(int capacity)
        => Items = new(capacity);

    public void AddItem(Item item)
        => Items.Add(item);

    internal bool HasFreeSpace()
        => Items.Count < Items.Capacity;
}
