using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempShockwaveCaller : MonoBehaviour
{

    public GameObject shockwaveHitbox;
    // Start is called before the first frame update
    void Awake()
    {
        instantiateShockwave();        
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            instantiateShockwave();
        }
    }*/

    void instantiateShockwave(){
        Instantiate(shockwaveHitbox, transform.position, transform.rotation);
    }
}
