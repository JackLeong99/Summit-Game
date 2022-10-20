using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[Serializable] public class BossContext
{
    [Header("Movement")]
    public bool canTurn = false;

    [Header("Attacks")]
    public AttackState attackState = AttackState.CanRanged;
    public float cooldownTimer;
    public float moveRepeated;

    [Header("Health")]
    public float curHealth;
    public IState iFrame = IState.Inactive;

    [Header("Speed")]
    public float startSpeed;

    [Header("Rage")]
    public bool rage = false;

    [Header("Stun")]
    public StunState stunState = StunState.Accepted;  
}

public enum AttackState { CanAttack, CanRanged, InAttack }
public enum IState { Active, Inactive }
public enum StunState { Accepted, Stunned, Ignore }
public enum AnimationState { Accepted, Ignore, Done }

public class BossStateMachine : MonoBehaviour
{
    [Header("Attributes")]
    public BossAttributes attributes;
    public BossContext components;
    public UnityEvent callback;
    public UnityEvent callbackEvent;

    [Header("Ability Keypoints")]
    public MeleeHitbox leftHand;
    public MeleeHitbox rightHand;

    [Header("Abilities")]
    public BossState currentAbility;
    public BossState baseState;
    public BossState idleState;
    public List<BossState> abilities = new List<BossState>();

    [Header("Previous States")]
    public BossState previousState;
    public BossState previousAbility;
    public BossState previousDecision;

    [Header("Component References")]
    public Animator anim;
    public NavMeshAgent agent;

    public void Start()
    {
        GetInstances();
        StartCoroutine(SetParameters());
    }

    public void GetInstances()  
    {
        BossManager.instance.boss = this;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public IEnumerator SetParameters()
    {
        while (GameManager.instance.inLoading) { yield return null; }

        components.curHealth = attributes.maxHealth;
        yield return AnnouncementHandler.instance.Announcement(attributes.bossName, 2);
        BossManager.instance.SetBoss();
        Initialize(baseState);
    }

    public void Initialize(BossState startingState)
    {
        currentAbility = startingState;
        currentAbility.Invoke(this);
    }

    public void Update()
    {
        switch (true)
        {
            case bool x when currentAbility != null:
                currentAbility.Update();
                break;
            default:
                Debug.LogWarning("CurrentAbility is: " + currentAbility);
                break;
        }
    }

    public void ChangeState(BossState changeToAbility)
    {
        if (currentAbility.ExitingState)
            return;
        currentAbility.Exit();
        currentAbility = changeToAbility;
        currentAbility.Invoke(this);
    }

    public BossState GetState<T>() where T : BossState
    {
        return abilities.Find(x => x.GetType().Equals(typeof(T)));
    }

    public void AnimationInvoke()
    {
        callback.Invoke();
    }

    public void AnimationEvent()
    {
        callbackEvent.Invoke();
    }

    public void TakeDamage(float dmg, Vector3 position)
    {
        switch (components.iFrame)
        {
            case IState.Active:
                return;
        }

        StartCoroutine(IFrame());

        components.curHealth -= dmg;

        DamageHandler(dmg, position);
    }

    public void TakeDamage(float[] dmg, float tickRate, Vector3 position)
    {
        StartCoroutine(IFrame());

        StartCoroutine(TickDamage(dmg, tickRate, position));
    }

    public IEnumerator TickDamage(float[] dmg, float tickRate, Vector3 position)
    {
        foreach (var tick in dmg)
        {
            if (!Alive()) continue;
            components.curHealth -= tick;
            DamageHandler(tick, position);

            yield return new WaitForSeconds(tickRate);
        }
    }

    public IEnumerator IFrame()
    {
        components.iFrame = IState.Active;
        yield return new WaitForSeconds(attributes.iFrameTime);
        components.iFrame = IState.Inactive;
    }

    public void DamageHandler(float dmg, Vector3 position)
    {
        AkSoundEngine.PostEvent("Enemy_Damage", gameObject);
        AkSoundEngine.PostEvent("UI_Hit_Indicator", GameManager.instance.mainCamera);

        BossManager.instance.damageTextPool.Spawn(position, dmg.ToString(), Color.white, dmg > 15f ? 12f : 4f);

        if (!Alive())
        {
            ChangeState(GetState<DeathState>());
            return;
        }

        CheckRage();
    }

    public bool Alive()
    {
        return components.curHealth > 0;
    }

    public void CheckRage()
    {
        if (components.rage == false && (components.curHealth <= (attributes.maxHealth / 2)))
        {
            StartCoroutine(triggerRage());
        }
    }

    IEnumerator triggerRage()
    {
        components.rage = true;
        agent.speed = attributes.rageSpeed;
        yield return null;
    }

    public float DamageCalculation(float baseDamage)
    {
        return components.rage ? baseDamage * attributes.rageMultiplier : baseDamage;
    }

    public void onStep()
    {
        AkSoundEngine.PostEvent("Enemy_Footsteps", gameObject);
    }
}
