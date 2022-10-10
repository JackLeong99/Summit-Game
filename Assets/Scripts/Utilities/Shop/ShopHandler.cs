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
        switch (true) 
        {
            case bool x when Inventory.instance.gold >= item.cost:
                Inventory.instance.gold -= item.cost;
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
