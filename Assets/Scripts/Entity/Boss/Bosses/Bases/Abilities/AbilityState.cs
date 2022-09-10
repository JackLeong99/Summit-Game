using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/AbilityState")]
public class AbilityState : BaseState
{
    [Header("Information")]
    [SerializeField] public float duration;
    [SerializeField] public float delay;
    [SerializeField] public int layer;

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

        boss.previousAbility = this;
    }

    public virtual void Setup()
    { }
}