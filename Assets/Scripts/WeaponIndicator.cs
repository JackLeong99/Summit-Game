using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndicator : MonoBehaviour
{
    [SerializeField] Material charged;

    [SerializeField] Material charging;

    private Renderer rende;

    private ThirdPersonShooting gun;

    void Start()
    {
        gun = GetComponentInParent<ThirdPersonShooting>();
        rende = GetComponent<Renderer>();
    }
    void Update()
    {
        Debug.Log(rende.materials[3]);
    }
}
