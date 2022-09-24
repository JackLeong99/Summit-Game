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

    public AnimationState animationActive;

    [Header("Information")]
    [SerializeField] public float delay;

    public AbilityState nextState;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);
    }

    public override void Update()
    {
        //Debug.Log("Before: " + animationActive + "nextState: " + nextState);
        base.Update();

        switch (animationActive)
        {
            case AnimationState.Done:
                animationActive = AnimationState.Accepted;
                //Debug.Log("After: " + animationActive);
                boss.StartCoroutine(SwapState(nextState != null ? nextState : boss.baseState, delay));
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
        animationActive = AnimationState.Accepted;
        boss.anim.SetTrigger(animation);

        //Debug.Log(this.name + " Loaded");
        AkSoundEngine.PostEvent(soundEvent, boss.gameObject);
        boss.StartCoroutine(AnimationControl());
    }

    public IEnumerator AnimationControl()
    {
        animationActive = AnimationState.Ignore;
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(boss.anim.GetCurrentAnimatorStateInfo(0).length);
        animationActive = AnimationState.Done;
    }

    public virtual IEnumerator SwapState(BossState state, float delay)
    {
        //Debug.Log("Swapped to " + state);
        yield return new WaitForSeconds(delay);
        boss.ChangeState(state);
    }
}