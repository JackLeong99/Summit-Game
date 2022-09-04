using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Melee AbilityState")]
public class MeleeAbilityState : AbilityState
{
    [Header("Attributes")]
    public string animation;
    public string soundEvent;

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

        switch (boss.animationActive)
        {
            case false:
                boss.ChangeState(boss.baseState);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        boss.anim.SetTrigger(animation);
        AkSoundEngine.PostEvent(soundEvent, boss.gameObject);

        boss.rightHand.force = knockbackForce;
        boss.leftHand.force = knockbackForce;

        boss.rightHand.damage = damage;
        boss.leftHand.damage = damage;

        boss.animationActive = true;
    }
}