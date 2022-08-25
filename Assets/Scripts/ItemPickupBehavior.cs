using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupBehavior : Interactable
{

    public ItemSO item;
    [SerializeField] public int quantity = 1;
    private bool showingMesssage = false;
    private InteractionMessageBehavior messageUI;

    private void Start()
    {
        messageUI = InteractionMessageBehavior.instance;
    }

    public override void Interact()
    {
        base.Interact();
        if(item.interactText.Length > 0 && !showingMesssage)
        {   
            showingMesssage = true;
            ShowPickUpMessage();        
        }
        if (Input.GetButtonDown("Interact"))
        {
            PickupItem();
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        if (item.interactText.Length > 0)
        {
            TurnOffPickUpMessage();
            showingMesssage = false;
        }
    }

    private void TurnOffPickUpMessage()
    {
        messageUI.DeactiveText(MessageType.Interact);
    }

    private void PickupItem()
    {
        if (Inventory.instance.Add(item, quantity))
        {
            Debug.Log("adding " + item + " to the inventory");
            Destroy(gameObject);
        }
    }

    private void ShowPickUpMessage()
    {
        Debug.Log(item.interactText);
        messageUI.SetText(item.interactText, MessageType.Interact);
    }
}
