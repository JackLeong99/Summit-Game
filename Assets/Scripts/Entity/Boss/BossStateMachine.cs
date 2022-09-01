using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

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
    public bool animationActive = false;

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

public class BossStateMachine : MonoBehaviour
{
    public static BossStateMachine instance;

    [Header("Attributes")]
    public BossAttributes attributes;
    public BossContext components;

    [Header("Ability Keypoints")]
    public GameObject leftHand;
    public GameObject rightHand;

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
    public DamageFlash flash;



    public void Start()
    {
        GetInstances();
        SetParameters();
        Initialize(baseState);
    }

    public void GetInstances()
    {
        instance = this;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        flash = GetComponent<DamageFlash>();
    }

    public void SetParameters()
    {
        components.curHealth = attributes.maxHealth;
    }

    public void Initialize(BossState startingState)
    {
        currentAbility = startingState;
        currentAbility.Invoke(this);
    }

    public void Update()
    {
        currentAbility.Update();
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
        AkSoundEngine.PostEvent("UI_Hit_Indicator", GameManager.mainCamera);

        UIManager.Instance.HealthBossBarSet((int)Mathf.Round(components.curHealth));
        UIManager.Instance.DamageTextPool.Spawn(position, dmg.ToString(), Color.white, dmg > 15f ? 12f : 4f);

        if (flash != null)
            flash.Flash();

        if (!Alive())
        {
            ChangeState(GetState<DeathState>());
            return;
        }

        CheckRage();
    }

    public bool Alive()
    {
        return components.curHealth >= 0;
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
        //attributes.attackTurnSpeed = 120.0f;
        //turnFor = 0.9f;
        yield return null;
    }

    public void onStep()
    {
        AkSoundEngine.PostEvent("Enemy_Footsteps", gameObject);
    }
}
