using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SlowShotBuff")]
public class UtilGun : ItemBase
{
    public float bonusDamage;
    public float reducedSpeed;
    public override void effect()
    {
        GameManager.instance.player.GetComponent<ThirdPersonShooting>().bulletDamage += bonusDamage;
        GameManager.instance.player.GetComponent<ThirdPersonShooting>().projectileSpeed -= reducedSpeed;
        GameManager.instance.player.GetComponent<ThirdPersonShooting>().useGrav = true;
    }
}
