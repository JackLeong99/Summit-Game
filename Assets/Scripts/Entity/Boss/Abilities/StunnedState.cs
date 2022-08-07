using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/StunState")]
public class StunnedState : BaseState
{
    //public StunnedState(BossStateMachine boss) : base(boss) { }

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        boss.StartCoroutine(Stunned());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void Stun(StunState state)
    { 
        boss.components.stunState = state;
    }

    public IEnumerator Stunned()
    {
        boss.agent.speed = 0;
        boss.anim.SetTrigger("Stunned");

        yield return new WaitForSeconds(boss.components.stunTimer);
        boss.anim.SetTrigger("StunEnd");

        boss.agent.speed = boss.components.rage ? boss.attributes.rageSpeed : boss.components.startSpeed;

        Stun(StunState.Accepted);
        boss.ChangeState(boss.baseState);
    }
}
