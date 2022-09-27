using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public ActiveAbility item;

    private void Update()
    {
        if (GameManager.instance.input.activeItem) 
        {
            item.effect();
        }
    }

    public void setItem(ActiveAbility i) 
    {
        item = i;
    }
}
