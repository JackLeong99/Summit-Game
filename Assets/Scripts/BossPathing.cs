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

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos = target.transform;
    }

    void Update()
    {
        //agent.destination = targetPos.position;
    }

    public void bossPathing(){
        agent.destination = targetPos.position;

    }
}
