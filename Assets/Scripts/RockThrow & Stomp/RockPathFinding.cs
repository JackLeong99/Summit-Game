using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPathFinding : MonoBehaviour
{
    private GameObject target;
   // public RockManager rockManager;
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
    }


    public void SetTarget()
    {
        Debug.Log(this.transform.position + "hi");
        target=RockManager.Instance.FindClosesRock(this.transform);
        RockPickedUp targeting = target.GetComponent<RockPickedUp>();
        targeting.PickedUp();
    }
}
