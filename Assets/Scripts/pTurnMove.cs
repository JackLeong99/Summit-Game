using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class pTurnMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float turningSpeed;
    public float vertical;
    public float horizontal;
    private float vertSpeed;
    // Start is called before the first frame update
        void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        vertical = Input.GetAxis("Vertical"); //vertical measurement from -1 to 1 (-1 = s/down arrow)
        horizontal = Input.GetAxis("Horizontal"); //horizontal measurement from -1 to 1 (-1 = a/left arrow)
        vertSpeed = speed * Time.deltaTime;
    //if a or d are held turn
        if(horizontal != 0){
            transform.Rotate(0, turningSpeed * horizontal * Time.deltaTime, 0, Space.Self);

        }
    //if w is held go forwards
        if (vertical > 0){
            transform.Translate(Vector3.forward * speed *Time.deltaTime , Space.Self);
        }
    //S for backwards
        if (vertical < 0){
            transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.Self);
        }
    }
}
