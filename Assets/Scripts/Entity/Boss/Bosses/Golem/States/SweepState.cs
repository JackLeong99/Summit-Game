using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Sweep")]
public class SweepState : AbilityState
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
        boss.anim.SetTrigger("Sweep");
        AkSoundEngine.PostEvent("Enemy_Melee_Right_Hook", boss.gameObject);

        //boss.StartCoroutine();
    }
}