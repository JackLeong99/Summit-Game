using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/BaseState")]
public class BaseState : BossState
{
    //public BaseState(BossStateMachine boss) : base(boss) { }

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        switch (boss.components.stunState)
        {
            case StunState.Stunned:
                boss.ChangeState(boss.GetState<StunnedState>());
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void LookTowards(Transform target)
    {
        var targetRot = Quaternion.LookRotation(target.position - boss.transform.position);
        var adjustedRot = Quaternion.Euler(0.0f, targetRot.eulerAngles.y, targetRot.eulerAngles.z);
        boss.transform.rotation = Quaternion.RotateTowards(boss.transform.rotation, adjustedRot, boss.attributes.turnSpeed * Time.deltaTime);
    }
}