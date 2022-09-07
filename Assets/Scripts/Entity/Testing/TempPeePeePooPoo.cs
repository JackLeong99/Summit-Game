using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPeePeePooPoo : MonoBehaviour
{
    public GameObject XD;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            XD.gameObject.SetActive(false);
        }
    }
}
