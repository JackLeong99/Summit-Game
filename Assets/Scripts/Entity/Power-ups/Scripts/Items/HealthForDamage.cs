using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthForDamage")]
public class HealthForDamage : PassiveItem
{
    public float damagePercentIncrease;
    public float healthPercentDecrease;
    

    //Increases damage percent at moment of pickup. As it directly modifies the value it is much stronger
    //the more damage increases are active before it is acquired. Multiplicatively stacks.
    public override void effect()
    {
        //need to also do for guns but not overarching method to increase the damage of all guns / actives without manually doing it
        GameManager.instance.player.GetComponent<AutoAttack>().attackDamage += GameManager.instance.player.GetComponent<AutoAttack>().attackDamage/100 * damagePercentIncrease;
        //scales even if player later gains hp.
        GameManager.instance.player.GetComponent<PlayerHealth>().healthPercentCap -= healthPercentDecrease;
    }
}
