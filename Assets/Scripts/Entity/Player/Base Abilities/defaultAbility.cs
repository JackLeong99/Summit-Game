using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActiveAbilities/DefaultAbility")]

public class defaultAbility : ActiveAbility
{
    public override void effect()
    {
        Debug.Log("Cast default ability");
    }

    public override IEnumerator doEffect()
    {
        throw new System.NotImplementedException();
    }
}
