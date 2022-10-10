using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [Header("Information")]
    public string itemName = "Default Item Name";
    public string description = "This is an item";
    public int cost;

    [Header("Values")]
    public Sprite icon;

    public abstract void effect();
    public abstract void acquire();
}
