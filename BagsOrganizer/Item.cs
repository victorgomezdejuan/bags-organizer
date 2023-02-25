namespace BagsOrganizer;

public enum Category
{
    Clothes,
    Herbs,
    Metals,
    Weapons
}

public class Item
{
    public Category Category { get; set; }
    public string Name { get; set; }

    public Item(Category category, string name)
    {
        Category = category;
        Name = name;
    }
}