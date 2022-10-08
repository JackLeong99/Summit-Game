using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public ItemBase item;
    public int cost;

    public void BuyItem()
    {
        switch (true) 
        {
            case bool x when Inventory.instance.gold >= cost:
                Inventory.instance.gold -= cost;
                item.acquire();
                break;
        }
    }

    public void setCost() 
    {
        cost = item.cost;
    }
}
