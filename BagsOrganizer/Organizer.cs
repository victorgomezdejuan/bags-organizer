namespace BagsOrganizer;

public class Organizer
{
    private static readonly Dictionary<string, string[]> Items = new() {
        ["clothes"] = new string[] { "Leather", "Linen", "Silk", "Wool" },
        ["herbs"] = new string[] { "Cherry Blossom", "Marigold", "Rose", "Seaweed" },
        ["metals"] = new string[] { "Copper", "Gold", "Iron", "Silver" },
        ["weapons"] = new string[] { "Axe", "Dagger", "Mace", "Sword" }
    };

    private int itemCount;
    private List<string> backpack;
    private List<KeyValuePair<string, List<string>>> bags;

    public Organizer()
    {
        itemCount = 0;
        backpack = new(8);
        bags = new();

        for (int i = 0; i < 4; i++)
            bags.Add(new KeyValuePair<string, List<string>>(string.Empty, new List<string>(4)));
    }

    public static string[] GetCategories()
        => Items.Keys.ToArray();

    public static string[] GetItemsOfCategory(string category)
        => Items[category];

    public void AddItem(string item)
    {
        if (!Items.SelectMany(c => c.Value).Contains(item))
            throw new NonExistingItemException();

        if (itemCount < 8)
            backpack.Add(item);
        else {
            if (itemCount < 12)
                bags.ElementAt(0).Value.Add(item);
            else {
                if (itemCount < 16)
                    bags.ElementAt(1).Value.Add(item);
                else
                    bags.ElementAt(2).Value.Add(item);
            }
                
        }
            

        itemCount++;
    }

    public string[] GetBackpackItems()
        => backpack.ToArray();

    public string[] GetItemsOfBag(int bagIndex)
    {
        return bags.ElementAt(bagIndex).Value.ToArray();
    }
}