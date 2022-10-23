using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hordePathing : MonoBehaviour
{
    public float lifespan;
    private float lifespanCounter;
    public float explosionDetection;
    public GameObject explosionHitbox;
    public float maxHP;
    public float explosionDelay;
    private float currentHP;

    private Transform targetPos;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        targetPos = GameManager.instance.player.transform;
        currentHP = maxHP;
    }

    void Update()
    {
        Pathing();
        float distance = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        if(currentHP <= 0 || distance <= explosionDetection || lifespanCounter >= lifespan)
        {
            StartCoroutine(explode());
        }
        lifespanCounter += (Time.deltaTime);
    }

    public void Pathing()
    {
        agent.destination = targetPos.position;
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void setSpeed(float f) 
    {
        agent.speed = f;
    }

    public void takeDamage(float dmg)
    {
        currentHP = currentHP - dmg;
    }
    public IEnumerator explode()
    {
        anim.SetTrigger("Explode");
        yield return new WaitForSeconds(explosionDelay);
        Instantiate(explosionHitbox, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            StartCoroutine(explode());
        }
    }
}

