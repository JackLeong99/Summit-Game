using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyActive : MonoBehaviour
{
    public ActiveAbility activeAbility;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Destroy(gameObject);
            other.GetComponent<ActiveItem>().setItem(activeAbility);
        }
    }
}
