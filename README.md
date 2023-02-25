# Bags Organizer
Kata got from https://www.codurance.com/katalyst/bags.

Developed with dotnet (c#) and Visual Studio.

## Practice objectives
- TDD
- Visual Studio shortcuts

## Brief explanation
### Requirements
Create an application that helps Durance organize the items in his bags. Each bag can have either a category or not, the backpack has no category.

Items are always added to the backpack, if it happens to be full, the item is added to the next available bag.

After organizing the items, each bag should have the items belonging to its category, sorted alphabetically. If the bag happens to be full, the rest of the items are stored in the backpack or successive bags, following the previous sort.

#### Rules
- Durance has 1 backpack and 4 extra bags
- Each bag has a capacity of 4 items, the backpack has a capacity of 8 items
- Each bag can have a category, the backpack doesn't have one
- Items are sorted alphabetically after organizing the bags