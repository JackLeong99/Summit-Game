using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public ItemBase item;
    public int cost;

    public void BuyItem()
    {
        //switch ()
        item.effect(GameManager.instance.player);
    }
}
