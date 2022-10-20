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
    [SerializeField] public float delay;

    public AbilityState nextState;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        boss.callback.AddListener(Callback);
    }

    public override void Update()
    {
        //Debug.Log("Before: " + animationActive + "nextState: " + nextState);
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        boss.callback.RemoveAllListeners();
        boss.callbackEvent.RemoveAllListeners();

        nextState = null;
        boss.previousAbility = this;
    }

    public virtual void Setup()
    {
        boss.anim.SetTrigger(animation);

        //Debug.Log(this.name + " Loaded");
        switch (true)
        {
            case bool _ when soundEvent.Length != 0:
                AkSoundEngine.PostEvent(soundEvent, boss.gameObject);
                break;
        }
    }

    public void Callback()
    {
        Debug.Log("CALLBACK!");
        boss.StartCoroutine(SwapState(nextState != null ? nextState : boss.baseState, delay));
    }

    public virtual IEnumerator SwapState(BossState state, float delay)
    {
        Debug.Log("Swapped to " + state);
        yield return new WaitForSeconds(delay);
        boss.ChangeState(state);
    }
}