using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] GameObject shockwaveHitbox;
    [SerializeField] GameObject hitPoint;

    public void instantiateShockwave()
    {
        var wave = Instantiate(shockwaveHitbox, hitPoint.transform.position, transform.rotation) as GameObject;
    }
}
