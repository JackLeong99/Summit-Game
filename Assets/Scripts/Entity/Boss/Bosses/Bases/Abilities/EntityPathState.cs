using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Entity Pathing State")]
public class EntityPathState : AbilityState
{
    [Header("Values")]
    public string targetTag;
    public float stoppingDist;

    private AnimationState animationActive = AnimationState.Accepted;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();

        switch (true)
        {
            case bool x when Vector3.Distance(boss.agent.destination, boss.transform.position) < stoppingDist && animationActive == AnimationState.Ignore:
                animationActive = AnimationState.Done;
                Callback();
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        animationActive = AnimationState.Accepted;
        boss.agent.isStopped = true;
        boss.agent.destination = GameManager.instance.player.transform.position;
    }

    public override void Setup()
    {
        animationActive = AnimationState.Ignore;
        boss.agent.destination = GetNearestEntity().transform.position;
        boss.agent.isStopped = false;
    }

    public GameObject GetNearestEntity()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag(targetTag);
        float distance = 0;
        int index = 0;

        for (int i = 0; i < entities.Length; i++)
        {
            float currentDist = Vector3.Distance(entities[i].transform.position, boss.transform.position);

            switch (true)
            {
                case bool x when i == 0:
                case bool y when currentDist < distance:
                    distance = currentDist;
                    index = i;
                    break;
            } 
        }

        return entities[index];
    }
}