using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] Color charged;

    [SerializeField] Color charging;

    [SerializeField] int matNumber;

    private Material mat;

    private ThirdPersonShooting gun;

    void Start()
    {
        gun = GetComponentInParent<ThirdPersonShooting>();
        mat = GetComponent<Renderer>().materials[matNumber];
    }
    void Update()
    {
        if (gun.onCooldown == true)
        {
            mat.SetColor("_EmissionColor", charging * 6);
        }
        else 
        {
            mat.SetColor("_EmissionColor", charged * 6);
        }
    }
}
