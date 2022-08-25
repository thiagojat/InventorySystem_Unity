using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] private GameObject SlotsParent;
    private InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        ClearSlots();
    }

    void Update()
    {

    }

    void UpdateUI()
    {   
        ClearSlots();
        for(int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].AddItem(inventory.items[i]);
        }
    }

    void ClearSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }
}
