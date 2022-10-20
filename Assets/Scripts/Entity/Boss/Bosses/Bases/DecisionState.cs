using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Boss/Ability/DecisionState")]
public class DecisionState : BaseState
{
    public List<BossState> abilities = new List<BossState>();
    private List<BossState> previousAbilities;

    [Header("Values")]
    public int abilityLimit;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        if (abilities.Count == 0 || abilities[0] != null)
        {
            var selected = DetermineAbility();
            if (previousAbilities == null) 
            {
                if (abilityLimit != 0)
                    SetPrevious(abilities[selected]);

                boss.ChangeState(abilities[selected]);
                return;
            }
            Debug.LogWarning("previousabilitieis.count = " + previousAbilities.Count);
            Debug.LogWarning(". abilityLimit = " + abilityLimit);
            if (previousAbilities.Count(x => x == abilities[selected]) == previousAbilities.Count && abilityLimit != 0 && previousAbilities.Count != 0)
            {
                return;
            }
            else
            {
                if (abilityLimit != 0)
                    SetPrevious(abilities[selected]);

                boss.ChangeState(abilities[selected]);
            }
        }
        else
            Debug.Log("Warning: Abilities is empty. Class: " + name);
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

    public void SetPrevious(BossState state)
    {
        switch (true)
        {
            case bool _ when previousAbilities.Count == abilityLimit:
                previousAbilities.RemoveAt(0);
                break;
        }

        previousAbilities.Add(state);
    }
}