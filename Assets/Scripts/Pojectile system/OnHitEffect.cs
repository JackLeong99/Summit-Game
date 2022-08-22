using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitEffect : ScriptableObject
{
    public abstract void ApplyOnHitEffects(GameObject target);
}
