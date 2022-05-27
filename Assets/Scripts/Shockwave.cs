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
    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftHand;
    private Vector3 rightHandVector;
    private Vector3 leftHandVector;
    private Vector3 spawnVector;
   
    // void Update()
    // {
    //     if(Input.GetKeyDown("x"))
    //     {
    //         instantiateShockwave();
    //     }
    // }

    public void instantiateShockwave(){
        var hand1Pos = rightHand.transform.position;
        var rightTemp = hand1Pos.x;
        var rightTemp2 = hand1Pos.z;
        var hand2Pos = leftHand.transform.position;
        var leftTemp = hand2Pos.x;
        var leftTemp2 = hand2Pos.z;
        rightHandVector = new Vector3(rightTemp, .5f, rightTemp2);
        leftHandVector = new  Vector3(leftTemp, .5f, leftTemp2);
        spawnVector = Vector3.Lerp(rightHandVector, leftHandVector, 0.5f);
        var wave = Instantiate(shockwaveHitbox, spawnVector, transform.rotation) as GameObject;
        wave.GetComponent<ShockwaveHandler>().scaleHitBox(maxSize, scaleTime);
        var fx = Instantiate(shockwaveFX, spawnVector, transform.rotation) as GameObject;
    }
}
