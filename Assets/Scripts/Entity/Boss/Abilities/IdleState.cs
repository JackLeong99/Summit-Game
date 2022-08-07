using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(menuName = "Boss/Ability/IdleState")]
public class IdleState : BaseState
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

    public bool Alive()
    {
        return boss.components.curHealth <= 0;
    }
}
