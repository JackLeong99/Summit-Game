using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackReciever : MonoBehaviour
{   
    private CharacterController player;
    private Inventory inventory;
    [SerializeField] float mass;
   
    public Vector3 impact;
    [HideInInspector]
    public bool invulnerable;

    void Start()
    {
        player = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

    public void AddImpactH(Vector3 dir, float force)
    {
        dir.y = 0;
        Debug.Log("knockback direction x: " + dir);
        impact += dir.normalized * force * 10 / (mass * (1 + inventory.knockbackReduction));
    }

    public void AddImpactV(Vector3 dir, float force)
    {
        dir.x = 0;
        dir.z = 0;
        Debug.Log("knockback direction y: " + dir);
        if(dir.y < 0)
        {
            dir.y = -dir.y;
        }
        impact += dir.normalized * force * 10 / (mass * (1 + inventory.knockbackReduction));
    }

    void Update()
    {
        if(impact.magnitude > 5 && !invulnerable)
        {
            player.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
        else
        {
            impact = Vector3.zero;
        }
    }
}
