using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/DeathState")]
public class DeathState : BossState
{
    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        boss.StartCoroutine(Death());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public IEnumerator Death()
    {
        boss.anim.SetTrigger("Death");
        AkSoundEngine.PostEvent("Enemy_Death", boss.gameObject);
        yield return new WaitForSeconds(8.2f);
        BossManager.instance.OnDeath(boss.attributes.position);
    }
}