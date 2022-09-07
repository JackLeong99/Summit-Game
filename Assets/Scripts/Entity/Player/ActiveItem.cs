using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public ItemBase item;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && item != null) 
        {
            item.effect(GameManager.instance.player);
        }
    }

    public void setItem(ItemBase i) 
    {
        item = i;
    }
}
