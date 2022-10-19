using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInArea : MonoBehaviour
{
    public float armor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Inventory.instance.updateStat(Inventory.StatType.defense, armor);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.instance.updateStat(Inventory.StatType.defense, -armor);
        }
    }
}
