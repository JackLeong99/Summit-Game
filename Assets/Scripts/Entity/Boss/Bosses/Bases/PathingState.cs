using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/PathingState")]
public class PathingState : BaseState
{
    private float patience = 0;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        patience = 0;
        boss.agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        patience += Time.deltaTime;

        switch (true)
        {
            case bool x when patience >= boss.attributes.rangedCooldown && boss.previousDecision == boss.GetState<RangedState>():
                boss.ChangeState(boss.idleState);
                break;
            case bool y when patience >= boss.attributes.attackCooldown && boss.previousDecision == boss.GetState<MeleeState>():
                boss.ChangeState(boss.idleState);
                break;
            case bool z when patience >= boss.attributes.rangedCooldown && boss.previousDecision == null:
                boss.ChangeState(boss.idleState);
                break;
        }

        boss.agent.destination = GameManager.player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();

        boss.agent.isStopped = true;
    }
}