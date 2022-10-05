using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    public string itemName;
    public int cost;
    public abstract void effect();
    public abstract void acquire();
}
