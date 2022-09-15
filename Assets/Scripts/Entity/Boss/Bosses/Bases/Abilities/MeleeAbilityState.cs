using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Melee AbilityState")]
public class MeleeAbilityState : AbilityState
{
    [Header("Hitbox Properties")]
    public float damage;
    public Vector2 knockbackForce;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        base.Setup();

        boss.rightHand.force = knockbackForce;
        boss.leftHand.force = knockbackForce;

        boss.rightHand.damage = boss.DamageCalculation(damage);
        boss.leftHand.damage = boss.DamageCalculation(damage);
    }
}