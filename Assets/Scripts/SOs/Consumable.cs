using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]

public class Consumable : ItemSO
{
    public HealingMode healingMode;
    public int healAmount;
    public override void Use()
    {
        base.Use();
        if(healingMode == HealingMode.Health)
        {
            Debug.Log("Adding " + healAmount + " to your health points");
            RemoveFromInventory();
        }else if(healingMode == HealingMode.Shield)
        {
            Debug.Log("Adding " + healAmount + " to your shield points");
            RemoveFromInventory();
        }
    }

    private void RemoveFromInventory()
    {
        Inventory.instance.Consume(this, 1);
    }
}

public enum HealingMode
{
    Health,
    Shield
}