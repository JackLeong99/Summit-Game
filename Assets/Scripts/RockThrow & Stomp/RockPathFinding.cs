using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPathFinding : MonoBehaviour
{
    private GameObject target;
    RockManager rockManager;
    // Start is called before the first frame update
    void Start()
    {
        rockManager = rockManager.GetComponent<RockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTarget()
    {
        target=rockManager.FindClosesRock(this.transform);
        RockPickedUp targeting = target.GetComponent<RockPickedUp>();
    }
}
