using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/BaseState")]
public class BaseState : BossAbility
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
}
