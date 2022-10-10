using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFloor : MonoBehaviour
{
    public TeleportAnchor anchor;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(anchor.Teleport());
    }
}
