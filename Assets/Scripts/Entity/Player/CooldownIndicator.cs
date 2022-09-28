using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownIndicator : MonoBehaviour
{
    public Color charged;
    public Color charging;

    public int matNumber;
    public int slotNumber;

    private Material mat;
    private PlayerAbilities playerAbilities;

    void Start()
    {
        playerAbilities = GetComponentInParent<PlayerAbilities>();
        Debug.Log("playerAbilities: " + playerAbilities);
        mat = GetComponent<Renderer>().materials[matNumber];
        Debug.Log("material gotten: " + mat);
    }
    void Update()
    {
        if (playerAbilities.internalCooldown[slotNumber] > 0 && playerAbilities.AbilitySlot[slotNumber].cooldown > 1)
        {
            mat.SetColor("_EmissionColor", charging * 6);
        }
        else
        {
            mat.SetColor("_EmissionColor", charged * 6);
        }
    }
}
