using BagsOrganizer;

namespace BagsOrganizerTests;

public class OrganizerTests
{
    private Organizer organizer;

    public OrganizerTests()
        => organizer = new();

    [Fact]
    public void EmptyBackPack()
        => Assert.Empty(organizer.GetBackpackItems());

    [Fact]
    public void AddInvalidItem()
        => Assert.Throws<NonExistingItemException>(
            () => organizer.AddItem(new Item(Category.Clothes, "Seaweed")));

    [Fact]
    public void AddOneItemToBackpack()
    {
        AddItems("Leather");
        AssertBackpackItems("Leather");
    }

    [Fact]
    public void AddTwoItemsToBackpack()
    {
        AddItems("Leather", "Axe");
        AssertBackpackItems("Leather", "Axe");
    }

    [Fact]
    public void FillUpBackpack()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
    }


    [Fact]
    public void AddOneItemToFirstBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold");
    }

    [Fact]
    public void FillUpAllBags()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold", "Marigold", "Marigold", "Dagger",
            "Linen", "Rose", "Linen", "Rose",
            "Mace", "Rose", "Linen", "Axe",
            "Silk", "Silk", "Silk", "Iron");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold", "Marigold", "Marigold", "Dagger");
        AssertBagItems(1, "Linen", "Rose", "Linen", "Rose");
        AssertBagItems(2, "Mace", "Rose", "Linen", "Axe");
        AssertBagItems(3, "Silk", "Silk", "Silk", "Iron");
    }

    [Fact]
    public void SetAllBagCategories()
    {
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Metals);
        organizer.SetCategoryOfBag(2, Category.Weapons);
        organizer.SetCategoryOfBag(3, Category.Herbs);

        Assert.Equal(Category.Clothes, organizer.GetCategoryOfBag(0));
        Assert.Equal(Category.Metals, organizer.GetCategoryOfBag(1));
        Assert.Equal(Category.Weapons, organizer.GetCategoryOfBag(2));
        Assert.Equal(Category.Herbs, organizer.GetCategoryOfBag(3));
    }

    [Fact]
    public void SetTwoBagCategories()
    {
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Metals);

        Assert.Equal(Category.Clothes, organizer.GetCategoryOfBag(0));
        Assert.Equal(Category.Metals, organizer.GetCategoryOfBag(1));
        Assert.Null(organizer.GetCategoryOfBag(2));
        Assert.Null(organizer.GetCategoryOfBag(3));
    }

    [Fact]
    public void Organize_AllItemsMatchABagCategory()
    {
        AddItems("Leather", "Linen", "Cherry Blossom", "Marigold", "Copper", "Gold", "Axe", "Dagger");
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Herbs);
        organizer.SetCategoryOfBag(2, Category.Metals);
        organizer.SetCategoryOfBag(3, Category.Weapons);

        organizer.Organize();

        AssertBackpackItems();
        AssertBagItems(0, "Leather", "Linen");
        AssertBagItems(1, "Cherry Blossom", "Marigold");
        AssertBagItems(2, "Copper", "Gold");
        AssertBagItems(3, "Axe", "Dagger");
    }

    [Fact]
    public void Organize_NotAllItemsMatchABagCategory()
    {
        AddItems("Leather", "Linen", "Cherry Blossom", "Marigold", "Copper", "Gold", "Axe", "Dagger");
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Herbs);

        organizer.Organize();

        AssertBackpackItems("Copper", "Gold", "Axe", "Dagger");
        AssertBagItems(0, "Leather", "Linen");
        AssertBagItems(1, "Cherry Blossom", "Marigold");
        AssertBagItems(2);
        AssertBagItems(3);
    }

    [Fact]
    public void Organize_NoItemMatchABagCategory()
    {
        AddItems("Copper", "Gold", "Axe", "Dagger");
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Herbs);

        organizer.Organize();

        AssertBackpackItems("Copper", "Gold", "Axe", "Dagger");
        AssertBagItems(0);
        AssertBagItems(1);
        AssertBagItems(2);
        AssertBagItems(3);
    }

    [Fact]
    public void Organize_TwoBagsWithSameCategory()
    {
        AddItems("Leather", "Leather", "Linen", "Linen", "Silk", "Silk");
        organizer.SetCategoryOfBag(0, Category.Clothes);
        organizer.SetCategoryOfBag(1, Category.Clothes);

        organizer.Organize();

        AssertBackpackItems();
        AssertBagItems(0, "Leather", "Leather", "Linen", "Linen");
        AssertBagItems(1, "Silk", "Silk");
    }

    private void AddItems(params string[] items)
    {
        foreach (string item in items)
            organizer.AddItem(ItemFromName(item));
    }

    private static Item ItemFromName(string item)
        => Organizer.PossibleItems.Single(i => i.Name.Equals(item));

    private void AssertBackpackItems(params string[] items)
    {
        Item[] backpackItems = organizer.GetBackpackItems();
        Assert.Equal(items.Length, backpackItems.Length);

        for (int i = 0; i < items.Length; i++) {
            Assert.Equal(ItemFromName(items[i]), backpackItems[i]);
        }
    }

    private void AssertBagItems(int bagIndex, params string[] items)
    {
        Item[] bagItems = organizer.GetItemsOfBag(bagIndex);
        Assert.Equal(items.Length, bagItems.Length);

        for (int i = 0; i < items.Length; i++) {
            Assert.Equal(ItemFromName(items[i]), bagItems[i]);
        }
    }
}