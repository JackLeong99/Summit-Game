using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/AbilityState")]
public class AbilityState : BaseState
{
    [Header("Attributes")]
    public string animation;
    public string soundEvent;

    [Header("Information")]
    [SerializeField] public float duration;
    [SerializeField] public float delay;
    [SerializeField] public int layer;

    public AbilityState nextState;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        base.Update();

        switch (boss.animationActive)
        {
            case AnimationState.Done:
                boss.animationActive = AnimationState.Accepted;
                switch (true)
                {
                    case bool x when nextState != null:
                        boss.StartCoroutine(SwapState(nextState, delay));
                        break;
                    default:
                        boss.StartCoroutine(SwapState(boss.baseState, delay));
                        break;
                }
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        nextState = null;
        boss.previousAbility = this;
    }

    public virtual void Setup()
    {
        Debug.Log("Lmao");
        boss.anim.SetTrigger(animation);
        AkSoundEngine.PostEvent(soundEvent, boss.gameObject);
    }

    public virtual IEnumerator SwapState(BossState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        boss.ChangeState(state);
    }
}