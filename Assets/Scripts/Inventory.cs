using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Item
{   
    public int quantity;
    public ItemSO item;
    public bool isEmpty => item == null;

    public Item ChangeQuantity(int newQuantity)
    {
        return new Item
        {
            quantity = newQuantity,
            item = this.item
        };
    }
}

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private ItemFactory factory;

    [SerializeField] private GameObject slotsParent;
    [HideInInspector] public int maxSlots = 0;
    public List<Item> items;
    public int spaceUsed = 0;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    private string failMessage = "Cannot add this item to the inventory";

    [SerializeField] private Transform player;

    private void Start()
    {
        maxSlots = slotsParent.transform.childCount;
        items = new List<Item>(maxSlots);
    }

    public bool Add(ItemSO item, int quantity)
    {
        Item newItem = new Item 
        { 
            quantity = quantity,
            item = item
        };
        if (newItem.item.isStackable)
        {
            if (IsStored(newItem) >= 0 && ((newItem.quantity + items[IsStored(newItem)].quantity) <= (newItem.item.maxStackAmount)))
            {
                //Stacking itens at the same slot
                items[IsStored(newItem)] = newItem.ChangeQuantity((newItem.quantity) += (items[IsStored(newItem)].quantity));
                if (onItemChangedCallBack != null)
                {
                    onItemChangedCallBack.Invoke();
                }
                return true;
            }
            else if(items.Count + 1 > maxSlots)
            {
                //Not enough space
                FailMessageUI();
                return false;
            }
            else
            {
                //Add an item on a new slot
                AddOnANewSlot(newItem);
                return true;    
            }
        }
        else if(items.Count + 1 <= maxSlots)
        {   
            //Add an item on a new slot 
            AddOnANewSlot(newItem);
            return true;
        }
        else
        {
            //Inventory doesnt have space to an unstackable item
            FailMessageUI();
            return false;
        }
            
    }

    internal void AddOnANewSlot(Item newItem)
    {
        items.Add(newItem);
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    void FailMessageUI()
    {
        InteractionMessageBehavior.instance.SetText(failMessage, MessageType.Fail);
        StartCoroutine(FailFade());
    }

    IEnumerator FailFade()
    {
        yield return new WaitForSecondsRealtime(2);
        InteractionMessageBehavior.instance.DeactiveText(MessageType.Fail);
    }

    public int IsStored(ItemSO newItem)
    {
       Item structItem = new Item
       {
            quantity = 1,
            item = newItem
       };

        return IsStored(structItem);
    }
    public int IsStored(Item newItem)
    {
        foreach (Item item in items)
        {
            if (item.item == newItem.item)
            {
                return items.IndexOf(item);
            }
        }
        return -1;
    }

    public void RemoveItem(ItemSO item)
    {
        Item newItem = new Item
        {
            quantity = 0,
            item = item
        };
        items.RemoveAt(IsStored(newItem));
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }   
    }

    public void Drop(ItemSO item, int quantity)
    {
        DropItem(item, quantity);
        RemoveQuantity(item, quantity);
        
    }

    private void DropItem(ItemSO item, int quantity)
    {
        GameObject itemGO = factory.getItem(item.itemType);
        if (itemGO != null)
            Instantiate(itemGO, player.position, Quaternion.identity);
        else Debug.Log("returned null");
    }

    public void Consume(ItemSO item, int quantity)
    {
        RemoveQuantity(item, quantity);
        
    }

    void RemoveQuantity(ItemSO item, int quantity)
    {
        int newQuantity = items[IsStored(item)].quantity - quantity; 
        items[IsStored(item)] = items[IsStored(item)].ChangeQuantity(newQuantity);
        if (items[IsStored(item)].quantity - quantity < 0)
        {
            RemoveItem(item);
        }
        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    
}


