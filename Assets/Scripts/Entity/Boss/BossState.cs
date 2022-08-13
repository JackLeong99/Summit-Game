using System.Collections;
using System;
using UnityEngine;

[Serializable] public abstract class BossState : ScriptableObject
{
    protected BossStateMachine boss;

    public bool ExitingState { get; protected set; }

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
