using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopIcon : MonoBehaviour
{
    public Vector3 rotationSpeed;

    public void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
