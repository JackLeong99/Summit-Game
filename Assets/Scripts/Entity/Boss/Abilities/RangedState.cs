using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
[CreateAssetMenu(menuName = "Boss/Ability/RangedState")]
public class RangedState : BaseState
{
    public List<BossState> abilities = new List<BossState>();

    //public IdleState(BossStateMachine boss) : base(boss) { }

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
