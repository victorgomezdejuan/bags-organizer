namespace BagsOrganizer;

public class Organizer
{
    public static readonly List<Item> PossibleItems = new() {
        new(Category.Clothes, "Leather"),
        new(Category.Clothes, "Linen"),
        new(Category.Clothes, "Silk"),
        new(Category.Clothes, "Wool"),
        new(Category.Herbs, "Cherry Blossom"),
        new(Category.Herbs, "Marigold"),
        new(Category.Herbs, "Rose"),
        new(Category.Herbs, "Seaweed"),
        new(Category.Metals, "Copper"),
        new(Category.Metals, "Gold"),
        new(Category.Metals, "Iron"),
        new(Category.Metals, "Silver"),
        new(Category.Weapons, "Axe"),
        new(Category.Weapons, "Dagger"),
        new(Category.Weapons, "Mace"),
        new(Category.Weapons, "Sword"),
    };
    private readonly Bag backpack;
    private readonly List<Bag> bags;
    private readonly List<Bag> backpackAndBags;

    public Organizer()
    {
        backpack = new Bag(8);
        bags = new() {
            new Bag(4),
            new Bag(4),
            new Bag(4),
            new Bag(4)
        };
        backpackAndBags = new List<Bag> {
            backpack
        };
        backpackAndBags.AddRange(bags);
    }

    public void AddItem(Item item)
    {
        if (!PossibleItems.Contains(item))
            throw new NonExistingItemException();

        backpackAndBags.First(b => b.HasFreeSpace()).AddItem(item);
    }

    public Item[] GetBackpackItems()
        => backpack.Items.ToArray();

    public Item[] GetItemsOfBag(int bagIndex)
        => bags[bagIndex].Items.ToArray();

    public void SetCategoryOfBag(int bagIndex, Category category)
        => bags[bagIndex].Category = category;

    public Category? GetCategoryOfBag(int bagIndex)
        => bags[bagIndex].Category;

    public void Organize()
    {
        List<Item> allItems = backpackAndBags.SelectMany(b => b.Items).ToList();
        backpackAndBags[0].Items.Clear();

        foreach (Item item in allItems) {
            Bag? matchingFreeBag = bags.FirstOrDefault(b => b.Category.Equals(item.Category) && b.HasFreeSpace());

            if (matchingFreeBag is not null)
                matchingFreeBag.AddItem(item);
            else
                backpackAndBags.First(b => b.HasFreeSpace()).AddItem(item);
        }
    }
}