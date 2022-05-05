using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPathFinding : MonoBehaviour
{
    private GameObject target;
    private RockPickedUp targeting;
    private bool currentlyTargeting=false;
    private int MoveSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
      //  rockManager = rockManager.GetComponent<RockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Teleport"))
        {
            SetTarget();
        }
        if(currentlyTargeting)
        {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if(Mathf.Abs(this.transform.position.x-target.transform.position.x)<5 && Mathf.Abs(this.transform.position.z-target.transform.position.z)<5)
            {
                targeting.PickedUp();
                currentlyTargeting=false;
            }
        }
    }

    


    public void SetTarget()
    {
        Debug.Log(this.transform.position + "hi");
        target=RockManager.Instance.FindClosesRock(this.transform);
        targeting = target.GetComponent<RockPickedUp>();
        //targeting.PickedUp(); //need to move first before pickup
        
        
        transform.LookAt(target.transform);
        currentlyTargeting=true;
    }
}//if close pick up
//set currently targeting to false
