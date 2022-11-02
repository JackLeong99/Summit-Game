using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInArea : MonoBehaviour
{
    public float armor;
    public bool present;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Inventory.instance.updateStat(Inventory.StatType.defense, armor);
            present = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.instance.updateStat(Inventory.StatType.defense, -armor);
            present = false;
        }
    }

    public void OnDestroy()
    {
        if (present) Inventory.instance.updateStat(Inventory.StatType.defense, -armor);
    }
}
