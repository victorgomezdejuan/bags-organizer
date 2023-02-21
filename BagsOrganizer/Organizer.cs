namespace BagsOrganizer;

public class Organizer
{
    private static readonly Dictionary<string, string[]> Items = new() {
        ["clothes"] = new string[] { "Leather", "Linen", "Silk", "Wool" },
        ["herbs"] = new string[] { "Cherry Blossom", "Marigold", "Rose", "Seaweed" },
        ["metals"] = new string[] { "Copper", "Gold", "Iron", "Silver" },
        ["weapons"] = new string[] { "Axe", "Dagger", "Mace", "Sword" }
    };

    private List<Bag> bags;

    public Organizer()
    {
        bags = new() {
            new Bag(8), // backpack
            new Bag(4),
            new Bag(4),
            new Bag(4),
            new Bag(4)
        };
    }

    public static string[] GetCategories()
        => Items.Keys.ToArray();

    public static string[] GetItemsOfCategory(string category)
        => Items[category];

    public void AddItem(string item)
    {
        if (!Items.SelectMany(c => c.Value).Contains(item))
            throw new NonExistingItemException();

        foreach(Bag bag in bags) {
            if(bag.HasFreeSpace()) {
                bag.AddItem(item);
                break;
            }
                
        }
    }

    public string[] GetBackpackItems()
        => bags[0].Items.ToArray();

    public string[] GetItemsOfBag(int bagIndex)
        => bags[bagIndex + 1].Items.ToArray();
}