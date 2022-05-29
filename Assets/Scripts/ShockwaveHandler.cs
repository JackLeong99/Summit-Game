using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveHandler : MonoBehaviour
{
    //NOTE: maxSize is multiplied by whatever the prefab's base scale/size is. If base is at (1, 1, 1) then maxSize = 20 will change the scale to (20, 1, 20).
    //However if, for example, base is at (0.1, 1, 0.1) then maxSize at 20 = (2, 1, 2), or maxSize = 100 = (10, 1, 10)
    [SerializeField] float maxSize;
    //in seconds = how long it will take to grow to maxSize (affects scale speed)
    [SerializeField] float scaleTime;
    //is set to float scaleTime;
    //this script is Instantiated by the Boss Manager Script at a location. It should not exist naturally.

    //private void Update()
    //{
    //    if (Input.GetKeyDown("x")) 
    //    {
    //        StartCoroutine(scale());
    //    }
    //}

    private void Awake()
    {
        StartCoroutine(scale());
    }
    private void OnTriggerEnter(Collider collision)
    {
        KnockbackReciever reciever = collision.gameObject.GetComponent<KnockbackReciever>();
        if(reciever)
        {
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }

    //the coroutine
    IEnumerator scale()
    {
        //the timer tracks how long it has grown for 
        float timer = 0;
        //the scale value of the prefab- eg: (.1, 1, .1)
        Vector3 scale = gameObject.transform.localScale;

        //create a copy so that the original is not altered and no clashes exist
        Vector3 toScale = scale;
        //scale the copy by the factor of maxSize
        toScale.x = toScale.x * maxSize;
        toScale.z = toScale.z * maxSize;

        //scaling by scaletime speed formula
        while (timer < scaleTime)
        {
            gameObject.transform.localScale = Vector3.Lerp(scale, toScale, timer/scaleTime);
            timer += Time.deltaTime;
            //yield return inside the loop so that each cycle of the while loop is enacted individually rather than instantly after the whole loop has completed.
            yield return null;
        }
        //when finished expanding remove from the scene
        Destroy(gameObject);
    }
}
