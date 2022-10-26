using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopIcon : MonoBehaviour
{
    public Vector3 rotationSpeed;

    public void Update() //-326.1 10.92 14.8      -175
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
