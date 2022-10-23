using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : ScriptableObject
{
    [Header("Information")]
    public string itemName = "Default Item Name";
    public string description = "This is an item";

    public Rarity rarity = Rarity.Common;
    public enum Rarity { Common = 5, Rare = 10, Mythic = 20, Corrupt = 15}
    public int cost;

    public void OnValidate()
    {
        cost = (int)rarity;
    }

    [Header("Values")]
    public Sprite icon;
    public Image imageIcon;

    public abstract void effect();
    public abstract void acquire();
}
