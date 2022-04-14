using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMoving : MonoBehaviour
{ 
    //private Transform target;
    public Transform target;
    private float translationZ;
    private float translationY;
    private float translationX;
    private float speed=20;

        private float trackZ;
    private float trackY;
    private float trackX;
    private Vector3 localArea;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject player = GameObject.Find("Player"); //probably can do this in boss that then sends the values needed to here.
        //target=player.transform;


        //puts position into local space
        localArea= transform.InverseTransformPoint(target.position); 
        localArea.Normalize();
        //takes position and stores it for later use
        translationZ=localArea.z;
        translationY=localArea.y;
        translationX=localArea.x;
    }

    // Update is called once per frame
   void Update()
    {
        transform.Translate(translationX *(speed* Time.deltaTime), translationY *(speed* Time.deltaTime), translationZ *(speed* Time.deltaTime));

    }
}
