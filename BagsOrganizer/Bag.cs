namespace BagsOrganizer;

internal class Bag
{
    internal List<Item> Items { get; }
    internal Category? Category { get; set; }

    internal Bag(int capacity)
        => Items = new(capacity);

    internal void AddItem(Item item)
        => Items.Add(item);

    internal bool HasFreeSpace()
        => Items.Count < Items.Capacity;

    internal void ClearItems()
        => Items.Clear();
}
