using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(menuName = "Boss/Ability/Shockwave")]
public class ShockwaveAbility : BaseState
{
    //public ShockwaveAbility(BossStateMachine boss) : base(boss) { }

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
        boss.StartCoroutine(CreateShockwave());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public IEnumerator CreateShockwave()
    {
        var wave = Instantiate(spawnableObject, parentObject.transform.position, boss.transform.rotation);
        yield return null;
    }
}
