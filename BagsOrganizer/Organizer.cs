namespace BagsOrganizer;

public class Organizer
{
    public static readonly List<Item> AllItems = new() {
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

    public static Item[] GetItemsOfCategory(Category category)
        => AllItems.Where(i => i.Category.Equals(category)).ToArray();

    public void AddItem(Item item)
    {
        if (!AllItems.Contains(item))
            throw new NonExistingItemException();

        foreach(Bag bag in backpackAndBags) {
            if(bag.HasFreeSpace()) {
                bag.AddItem(item);
                break;
            }
        }
    }

    public Item[] GetBackpackItems()
        => backpack.Items.ToArray();

    public Item[] GetItemsOfBag(int bagIndex)
        => bags[bagIndex].Items.ToArray();

    public void SetCategoryOfBag(int bagIndex, Category category)
    {
        bags[bagIndex].Category = category;
    }

    public Category? GetCategoryOfBag(int bagIndex)
        => bags[bagIndex].Category;

    public void Organize()
    {
        List<Item> allItems = backpackAndBags.SelectMany(b => b.Items).ToList();
        backpackAndBags[0].Items.Clear();

        foreach (Item item in allItems) {
            bool addedToCategoryBag = false;

            foreach (Bag bag in bags) {
                if (bag.Category is not null && bag.Category.Equals(item.Category)) {
                    bag.AddItem(item);
                    addedToCategoryBag = true;
                    break;
                }
            }

            if (!addedToCategoryBag)
                backpackAndBags[0].AddItem(item);
        }
    }
}