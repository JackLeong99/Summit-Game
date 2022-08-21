using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseProjectile : ProjectileBase
{
    public GameObject hitFX;

    public override void Hit()
    {
        base.Hit();
        Instantiate(hitFX, gameObject.transform.position, Quaternion.identity);
        AkSoundEngine.PostEvent("Player_Shoot_Impact", gameObject);
    }
}
