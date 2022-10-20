using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/DeathState")]
public class DeathState : BossState
{
    public GameObject deathEffect;
    public float goldDrop;

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
        Inventory.instance.updateStat(Inventory.StatType.gold, goldDrop);
        UIItemDisplay.instance.UpdateGold();


        yield return new WaitForSeconds(4f);

        switch (true)
        {
            case bool _ when deathEffect != null:
                Instantiate(deathEffect, boss.transform);
                break;
        }

        yield return new WaitForSeconds(4.2f);
        BossManager.instance.OnDeath(boss.attributes.position);
    }
}