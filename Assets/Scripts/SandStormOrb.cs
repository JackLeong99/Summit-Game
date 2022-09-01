using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStormOrb : MonoBehaviour
{
    public int orbHealth=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //needs health
    //something to disable sandstorm
    private void OnTriggerEnter(Collider other){
        if(other.tag=="sword" || other.tag == "PlayerBullet")
        {
            orbHealth--;
            if(orbHealth<=0)
            {
                //disable sandstorm
                Destroy(gameObject);
            }
        }
    }
}
