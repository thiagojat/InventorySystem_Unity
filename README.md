
# Inventory System
Hey there, this is an Inventory System, thought to a top-down RPG game, made by me :). 

Besides some basic movement script, this project includes the Inventory UI which works with a delegate.

The Inventory System itself have some basic functionalities:

> - Collecting items from the ground
> - Dropping items into the ground
> - Consuming items

## Commands for user

- Press E to collect an item when colliding with it;
- Click on the slot to use the current item;
- Click on the "X" at the top corner of a slot to drop the current item.

## Instructions for implementing
Here are some instructions for those who want to use this code on your project.

### Scriptable Object
This project items are set based on a scriptable object on `ItemSO.cs`. It has some already implemented properties like:

    public ItemType itemType;
    new public string name = "New item";
    public Sprite icon;
    public Sprite sprite;
    public string interactText = "Press E to pickup ";
    public string description = "";
    public bool isStackable = false;
    public int maxStackAmount = 3;

Feel free to add more properties if you want. You can also create a 
derived scriptable object from `ItemSO.cs`, like I did with 
`Consumable.cs` and added some features that only consumable objects have.

#### Adding a new Item
Due to the next line of code at `ItemSO.cs`, you can create a new asset clicking
the mouse right button at the project files and clicking "Create > Inventory > Item". 

        [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
But make sure to add a new constant at the ItemType enum defined in `ItemSO.cs`, 
and set it at the new item fields, defined at the inspector.

### Inventory
The Inventory works with a list of a struct called `Item`, defined at `Inventory.cs`, which has 3 variables:

    public int quantity;
    public ItemSO item;
    public bool isEmpty => item == null;

As every position at the list has the quantity and the item stored, it
 makes easier to render the UI. It just uses the information of the struct
 and update the UI using a delegate at `Inventory.cs` and calling `UpdateUI()` at `InventoryUI.cs`. 

### Command
Commands are called from `InventorySlot.cs`, invoking `Use()` or `Drop()` 
methods to execute the different behaviors of the current item.  
