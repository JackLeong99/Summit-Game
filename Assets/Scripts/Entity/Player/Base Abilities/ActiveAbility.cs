using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ItemBase
{
    public float cooldown;
    //[HideInInspector]
    public float castTime;
    public abstract IEnumerator doEffect();
}
