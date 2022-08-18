using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Some of this (such as the attack stuff) could be coming back so commented for now.

public class hordePathing : MonoBehaviour
{

    //Determine the object the boss will path to
    [SerializeField] private GameObject target;

    //Determine the position object the enemy will path to
    private Transform targetPos;
    [HideInInspector]
    public NavMeshAgent agent;

    private noCollideWithFren frenCollision;
    //private BossManager Attacking;

    //private Transform backupTargetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos = target.transform;
        
        
    }

    void Update()
    {
        //agent.destination = targetPos.position;
        // Attacking = this.GetComponent<BossManager>();
        // bool isAttacking = Attacking.inAttack;
        // bool isException = Attacking.attackException;
        // bool isStunned = Attacking.stunned;
        //bool isRockThrow = Attacking.inRockThrow;
        // if(isStunned || (isAttacking && !isException)){
        //     agent.isStopped = true;
        // }
        // else{
        //     agent.isStopped = false;
        // }
        frenCollision = this.GetComponent<noCollideWithFren>();
        bool isTooClose = frenCollision.tooClose;
        
        if(isTooClose)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            Pathing();
        }
    }

    public void Pathing()
    {
        agent.destination = targetPos.position;
    }

    public void ChangeTarget(GameObject newTarget)
    {
        target=newTarget;
        targetPos=target.transform;
    }
}

