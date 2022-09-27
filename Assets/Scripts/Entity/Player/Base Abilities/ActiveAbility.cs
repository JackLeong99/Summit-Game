using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ItemBase
{
    public abstract IEnumerator doEffect();
}
