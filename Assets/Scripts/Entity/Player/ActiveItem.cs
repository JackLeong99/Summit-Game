using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public ActiveAbility item;
    public float activeItemCD;

    private void Update()
    {
        activeItemCD -= Time.deltaTime;
        activeItemCD = Mathf.Clamp(activeItemCD, 0f, Mathf.Infinity);
        if (GameManager.instance.input.activeItem && activeItemCD <= 0) 
        {
            item.effect();
            activeItemCD = item.cooldown;
        }
    }

    public void setItem(ActiveAbility i) 
    {
        item = i;
        activeItemCD = item.cooldown;
    }
}
