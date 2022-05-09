using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPathFinding : MonoBehaviour
{
    private GameObject target;
    private RockPickedUp targeting;
    public bool currentlyTargeting=false;
    private BossPathing boss;
    //private int MoveSpeed = 4; //if calls work remove this and transform.pos


    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      player=GameObject.FindWithTag("Player");
      boss = this.GetComponent<BossPathing>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetButtonDown("Teleport"))
        {
            SetTarget();
        }*/
        if(currentlyTargeting)
        {
            if(Mathf.Abs(this.transform.position.x-target.transform.position.x)<5 && Mathf.Abs(this.transform.position.z-target.transform.position.z)<5)
            {
                targeting.PickedUp();
                //call animation
                currentlyTargeting=false;
                boss.ChangeTarget(player);
            }
        }
    }

    


    public void SetTarget()
    {
        Debug.Log(this.transform.position + "hi");
        target=RockManager.Instance.FindClosesRock(this.transform);
        targeting = target.GetComponent<RockPickedUp>();
        currentlyTargeting=true;
        boss.ChangeTarget(target); //should hopefully work
    }
}