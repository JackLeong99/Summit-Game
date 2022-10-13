using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public ItemBase item;

    [Header("Components")]
    public SpriteRenderer icon;

    public void BuyItem()
    {
        switch (Inventory.instance.CanPurchase(item.cost)) 
        {
            case true:
                item.acquire();
                break;
        }
    }

    public void DisplayItem()
    {
        ShopManager.instance.DisplayItem(item);
    }

    public void SetIcon()
    {
        icon.sprite = item.icon;
    }
}
