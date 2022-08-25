using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    new public string name = "New item";
    public Sprite icon;
    public Sprite sprite;
    public int size = 1;
    public string interactText = "Press E to pickup ";
    public string description = "";
    public bool isStackable = false;
    public int maxStackAmount = 3;

    public virtual void Use()
    {
        Debug.Log("Item " + name + " is being used");
    } 

    public void Drop()
    {
        Inventory.instance.Drop(this, 1);
    }
}

public enum ItemType
{
    GoldenApple,
    Chicken
}
