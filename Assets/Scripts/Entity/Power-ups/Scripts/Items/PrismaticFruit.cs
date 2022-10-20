using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Prismatic fruit")]

public class PrismaticFruit : PassiveItem
{
    [Range(0.0f, 1.0f)]
    public float percentHpPerStack;
    public float rate;
    [Range(0.0f, 1.0f)]
    public float regenerateBelowPercent;
    private PlayerHealth chp;
    public override void acquire()
    {
        base.acquire();
    }

    public override void effect()
    {
        if (Inventory.instance.GetStacks(this) <= 1)
        {
            chp = GameManager.instance.player.GetComponent<PlayerHealth>();
            chp.StartCoroutine(regen());
        }
    }

    public IEnumerator regen() 
    {
        while (Inventory.instance.GetStacks(this) > 0) 
        {
            if(chp.currentHealth/chp.maxHealth <= regenerateBelowPercent) 
            {
                chp.currentHealth += (percentHpPerStack * chp.maxHealth);
            }
            yield return new WaitForSeconds(rate);
        }
        yield return null;
    }
}
