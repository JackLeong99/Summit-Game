using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    [SerializeField] float maxSize;
    //in seconds = how long it will take to grow to maxSize (affects scale speed)
    [SerializeField] float scaleTime;
    
    [SerializeField] GameObject shockwaveHitbox;
    [SerializeField] GameObject shockwaveFX;
    [SerializeField] GameObject parentObject;
    [SerializeField] Vector3 handVector;
   
    // void Update()
    // {
    //     if(Input.GetKeyDown("x"))
    //     {
    //         instantiateShockwave();
    //     }
    // }

    public void instantiateShockwave(){
        var handPos = parentObject.transform.position;
        var temp = handPos.x;
        var temp2 = handPos.z;
        handVector = new Vector3(temp, .5f, temp2);
        var wave = Instantiate(shockwaveHitbox, handVector, transform.rotation) as GameObject;
        wave.GetComponent<ShockwaveHandler>().scaleHitBox(maxSize, scaleTime);
        var fx = Instantiate(shockwaveFX, handVector, transform.rotation) as GameObject;
    }
}
