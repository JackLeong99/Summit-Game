using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Boss/Ability/DecisionState")]
public class DecisionState : BaseState
{
    public List<BossState> abilities = new List<BossState>();

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        if (abilities[0] != null)
            boss.ChangeState(abilities[DetermineAbility()]);
        else
            Debug.Log("Warning: Abilities is empty");
    }

    public override void Exit()
    {
        base.Exit();

        boss.previousDecision = this;
    }

    public int DetermineAbility()
    {
        return Random.Range(0, abilities.Count);
    }
}