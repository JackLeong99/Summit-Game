using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ItemFrame : MonoBehaviour
{
    //storing the player related components
    private ThirdPersonController tpc;
    private ThirdPersonShooting tps;
    private PlayerStats stats;
    private KnockbackReciever knock;
    private Dodge dodge;
    private AutoAttack attack;

    [HideInInspector] public int msStacks;
    
    void Awake()
    {
        //getting the player related components
        tpc = GetComponent<ThirdPersonController>();
        tps = GetComponent<ThirdPersonShooting>();
        stats = GetComponent<PlayerStats>();
        knock = GetComponent<KnockbackReciever>();
        dodge = GetComponent<Dodge>();
        attack = GetComponent<AutoAttack>();
    }

    
    void Update()
    {
        bonusMS();
    }

    void bonusMS() 
    {
        tpc.MoveSpeed = 3 + (0.5f*msStacks);
        tpc.SprintSpeed = 10 + (0.5f * msStacks);
    }
}
