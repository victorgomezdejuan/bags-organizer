using BagsOrganizer;

namespace BagsOrganizerTests;

public class OrganizerTests
{
    private Organizer organizer;

    public OrganizerTests()
        => organizer = new();

    [Fact]
    public void Categories()
        => Assert.Equal(new string[] { "clothes", "herbs", "metals", "weapons" }, Organizer.GetCategories());

    [Fact]
    public void CategoryItems()
    {
        Assert.Equal(new string[] { "Leather", "Linen", "Silk", "Wool" }, Organizer.GetItemsOfCategory("clothes"));
        Assert.Equal(new string[] { "Cherry Blossom", "Marigold", "Rose", "Seaweed" }, Organizer.GetItemsOfCategory("herbs"));
        Assert.Equal(new string[] { "Copper", "Gold", "Iron", "Silver" }, Organizer.GetItemsOfCategory("metals"));
        Assert.Equal(new string[] { "Axe", "Dagger", "Mace", "Sword" }, Organizer.GetItemsOfCategory("weapons"));
    }

    [Fact]
    public void EmptyBackPack()
        => Assert.Empty(organizer.GetBackpackItems());

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
    public void AddWrongItem()
        => Assert.Throws<NonExistingItemException>(() => organizer.AddItem("NonExisting"));


    [Fact]
    public void AddOneItemToFirsttBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold");
    }

    [Fact]
    public void FillUpFirstBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold", "Marigold", "Marigold", "Dagger");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold", "Marigold", "Marigold", "Dagger");
    }

    [Fact]
    public void AddOneItemToSecondBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold", "Marigold", "Marigold", "Dagger",
            "Linen");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold", "Marigold", "Marigold", "Dagger");
        AssertBagItems(1, "Linen");
    }

    [Fact]
    public void FillUpSecondBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold", "Marigold", "Marigold", "Dagger",
            "Linen", "Rose", "Linen", "Rose");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold", "Marigold", "Marigold", "Dagger");
        AssertBagItems(1, "Linen", "Rose", "Linen", "Rose");
    }

    [Fact]
    public void AddOneItemToThirdBag()
    {
        AddItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper",
            "Gold", "Marigold", "Marigold", "Dagger",
            "Linen", "Rose", "Linen", "Rose",
            "Mace");
        AssertBackpackItems("Leather", "Axe", "Axe", "Axe", "Axe", "Axe", "Axe", "Copper");
        AssertBagItems(0, "Gold", "Marigold", "Marigold", "Dagger");
        AssertBagItems(1, "Linen", "Rose", "Linen", "Rose");
        AssertBagItems(2, "Mace");
    }

    // Arreglar este test y mirar si se puede generalizar el caso de extra bags para no dejarlo preparado sólo para 4

    private void AddItems(params string[] items)
    {
        foreach(string item in items)
            organizer.AddItem(item);
    }

    private void AssertBackpackItems(params string[] items)
    {
        string[] backpackItems = organizer.GetBackpackItems();
        Assert.Equal(items.Length, backpackItems.Length);

        for (int i = 0; i < items.Length; i++) {
            Assert.Equal(items[i], backpackItems[i]);
        }
    }

    private void AssertBagItems(int bagIndex, params string[] items)
    {
        string[] bagItems = organizer.GetItemsOfBag(bagIndex);
        Assert.Equal(items.Length, bagItems.Length);

        for (int i = 0; i < items.Length; i++) {
            Assert.Equal(items[i], bagItems[i]);
        }
    }
}