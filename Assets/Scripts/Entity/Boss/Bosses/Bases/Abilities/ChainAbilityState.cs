using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Chain Abilities")]
public class ChainAbilityState : BaseState
{
    [Header("Chaining")]
    public List<AbilityState> abilities = new List<AbilityState>();

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        for (int i = 0; i < abilities.Count; i++)
        {
            int next = i + 1;

            if (next >= abilities.Count)
            {
                continue;
            }

            abilities[i].nextState = abilities[next];
        }

        Debug.Log("Chain started");
        boss.ChangeState(abilities[0]);
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
