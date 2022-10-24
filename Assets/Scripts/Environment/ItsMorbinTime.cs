using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsMorbinTime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) AkSoundEngine.PostEvent("VO_Morbin_Time", gameObject);
    }
}
