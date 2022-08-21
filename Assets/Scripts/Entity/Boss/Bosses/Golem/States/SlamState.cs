using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Slam")]
public class SlamState : AbilityState
{
    public GameObject warning;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        boss.anim.SetTrigger("Slam");
        AkSoundEngine.PostEvent("Enemy_Melee_Overhead_Slam", boss.gameObject);

        //boss.StartCoroutine();
    }
}