using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/StunState")]
public class StunnedState : BaseState
{
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
        Stun(StunState.Ignore);
        boss.anim.SetTrigger("Stunned");

        yield return new WaitForSeconds(boss.attributes.stunTime);
        boss.anim.SetTrigger("StunEnd");

        Stun(StunState.Accepted);
        boss.ChangeState(boss.baseState);
    }
}
