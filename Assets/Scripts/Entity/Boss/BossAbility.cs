using System.Collections;
using System;
using UnityEngine;

[Serializable] public abstract class BossAbility : ScriptableObject
{
    protected BossStateMachine boss;

    [Header("Information")]
    [SerializeField] public float duration;
    [SerializeField] public float delay;
    [SerializeField] public int layer;

    [Header("References")]
    public GameObject spawnableObject;
    public GameObject parentObject;

    public bool ExitingState { get; protected set; }

    /*public BossAbility(BossStateMachine boss)
    {
        this.boss = boss;
    }*/

    public virtual void Invoke(BossStateMachine boss) 
    {
        SetInstance(boss);
        ExitingState = false;
    }

    public virtual void Update() { }

    public virtual void Exit() 
    {
        ExitingState = true;
    }

    private void SetInstance(BossStateMachine boss)
    {
        if (this.boss == null)
            this.boss = boss;
    }
}
