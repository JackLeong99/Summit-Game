using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    [SerializeField] float maxSize;
    //in seconds = how long it will take to grow to maxSize (affects scale speed)
    [SerializeField] float scaleTime;
    
    [SerializeField] GameObject shockwaveHitbox;
   
    void Update()
    {
        if(Input.GetKeyDown("x"))
        {
            instantiateShockwave();
        }
    }

    public void instantiateShockwave(){
        var wave = Instantiate(shockwaveHitbox, transform.position, transform.rotation) as GameObject;
        wave.GetComponent<ShockwaveHandler>().scaleHitBox(maxSize, scaleTime);
    }
}
