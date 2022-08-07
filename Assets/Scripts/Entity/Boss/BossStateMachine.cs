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
    public float currentPatience;
    public float moveRepeated;
    public bool attackException = false;

    [Header("Health")]
    public float curHealth;

    [Header("Speed")]
    public float startSpeed;

    [Header("Rage")]
    public bool rage = false;

    [Header("Stun")]
    public StunState stunState = StunState.Accepted;  
}

public enum AttackState { CanAttack, CanRanged, InAttack }
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
    public BossState previousState;
    public BossState baseState;
    public List<BossState> abilities = new List<BossState>();

    [Header("Component References")]
    public Animator anim;
    public NavMeshAgent agent;
    public DamageFlash damage;



    public void Start()
    {
        GetInstances();
        Initialize(baseState);
    }

    public void GetInstances()
    {
        instance = this;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        damage = GetComponent<DamageFlash>();
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
}
