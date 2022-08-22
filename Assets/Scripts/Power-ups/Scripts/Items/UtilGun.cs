using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SlowShotBuff")]
public class UtilGun : ItemBase
{
    public float bonusDamage;
    public float reducedSpeed;
    public override void effect(GameObject target)
    {
        target.GetComponent<ThirdPersonShooting>().bulletDamage += bonusDamage;
        target.GetComponent<ThirdPersonShooting>().projectileSpeed -= reducedSpeed;
        target.GetComponent<ThirdPersonShooting>().useGrav = true;
    }
}
