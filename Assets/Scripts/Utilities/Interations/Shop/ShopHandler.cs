using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public ItemBase item;
    public bool purchased = false;

    [Header("Components")]
    public SpriteRenderer icon;

    public void BuyItem()
    {
        switch (Inventory.instance.CanPurchase(item.cost)) 
        {
            case true:
                Purchase();
                break;
        }
    }

    public void Purchase()
    {
        item.acquire();
        purchased = true;
        icon.sprite = ShopManager.instance.purchaseIcon;
        ShopManager.instance.EnableDisplay(false);
    }

    public void DisplayItem()
    {
        if (purchased) { return; }
        ShopManager.instance.DisplayItem(item);
    }

    public void SetIcon()
    {
        icon.sprite = item.icon;
    }
}
