using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPathing : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Transform targetPos;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos = target.transform;
    }

    void Update()
    {
        agent.destination = targetPos.position;
    }
}
