using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPathFinding : MonoBehaviour
{
    private GameObject target;
    private RockPickedUp targeting;
    public bool currentlyTargeting=false;
    private BossPathing boss;
    private GameObject bManager;
    private BossManager bAlive;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      player=GameObject.FindWithTag("Player");
      boss = this.GetComponent<BossPathing>();
    //   bManager = GameObject.FindWithTag("Boss");
    //   bAlive = this.GetComponent<BossManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyTargeting) //&& bAlive.Alive)
        {
            if(Mathf.Abs(this.transform.position.x-target.transform.position.x)<5 && Mathf.Abs(this.transform.position.z-target.transform.position.z)<5)
            {
                targeting.PickedUp();
                //call animation
                currentlyTargeting=false;
                boss.ChangeTarget(player);
                Debug.Log("Targeting Player!");
            }
        }
    }

    


    public void SetTarget()
    {
        Debug.Log(this.transform.position + "hi");
        target=RockManager.Instance.FindClosesRock(this.transform);
        targeting = target.GetComponent<RockPickedUp>();
        currentlyTargeting=true;
        boss.ChangeTarget(target);
        Debug.Log("Changing Target to " + target);
    }

    public void stunnedInPathing()
    {
        if(currentlyTargeting){
            currentlyTargeting = false;
        }
        else{
            currentlyTargeting = true;
        }
        
    }
}