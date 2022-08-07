using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(menuName = "Boss/Ability/IdleState")]
public class GolemIdleState : BaseState
{
    //public IdleState(BossStateMachine boss) : base(boss) { }

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        switch (Alive())
        {

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
