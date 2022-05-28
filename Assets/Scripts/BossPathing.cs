using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPathing : MonoBehaviour
{

    //Determine the object the boss will path to
    [SerializeField] private GameObject target;

    //Determine the position object the boss will path to
    private Transform targetPos;
    [HideInInspector]
    public NavMeshAgent agent;
    private BossManager Attacking;

    //public Transform rockPos;
    //private Transform backupTargetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos = target.transform;
        
    }

    void Update()
    {
        //agent.destination = targetPos.position;
        Attacking = this.GetComponent<BossManager>();
        bool isAttacking = Attacking.inAttack;
        bool isException = Attacking.attackException;
        bool isStunned = Attacking.stunned;
        //bool isRockThrow = Attacking.inRockThrow;
        if(isStunned || (isAttacking && !isException)){
            agent.isStopped = true;
        }
        else{
            agent.isStopped = false;
        }
    }

    public void bossPathing(){
        agent.destination = targetPos.position;

    }

    public void ChangeTarget(GameObject newTarget)
    {
        target=newTarget;
        targetPos=target.transform;
        //bossPathing();

    }
}
