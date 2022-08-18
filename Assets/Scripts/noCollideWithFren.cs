using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noCollideWithFren : MonoBehaviour
{

    GameObject[] fren;
    private float privacyBubble = 1.5f;
    public float stuckCounter = 0f;
    
    //[HideInInspector]
    public bool tooClose = false;

    // Start is called before the first frame update
    void Start()
    {
        fren = GameObject.FindGameObjectsWithTag("hordeEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in fren)
        {
            if(item != gameObject)
            {
                float distance = Vector3.Distance(item.transform.position, this.transform.position);
                if(distance <= privacyBubble && stuckCounter <= 3)
                {
                    tooClose = true;
                    Vector3 direction = transform.position - item.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                    stuckCounter = stuckCounter + Time.deltaTime;
                }
                else
                {
                    tooClose = false;
                    if(stuckCounter > 0)
                    {
                        stuckCounter = stuckCounter - Time.deltaTime;
                    }
                }
            }
        }
    }
}
