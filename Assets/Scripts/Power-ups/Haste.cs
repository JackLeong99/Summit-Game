using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : MonoBehaviour
{
    private bool speedEnabled=false;
    private StarterAssets.ThirdPersonController movement;
    // Start is called before the first frame update
    void Start()
    {
        movement=this.GetComponent<StarterAssets.ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSpeed()
    {
        if(speedEnabled)
        {
            movement.DecreaseSpeed();
            speedEnabled=false;
        }
        else
        {
           movement.IncreaseSpeed();
           speedEnabled=true;
        }
        
    }
}
