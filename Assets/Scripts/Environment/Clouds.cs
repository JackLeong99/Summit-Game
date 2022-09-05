using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Vector3 rotationDirection;
    public float smoothTime;
    private float smooth;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        smooth = Time.deltaTime * smoothTime;
        transform.Rotate(rotationDirection * smooth);
    }
}
