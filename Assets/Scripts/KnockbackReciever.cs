using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class KnockbackReciever : MonoBehaviour
{   
    private ThirdPersonController controllerScript;
    private CharacterController player;

    [HideInInspector]
    public Vector3 impact;

    [SerializeField] float gravity;

    [SerializeField] float mass;

    private void Awake()
    {
        controllerScript = GetComponent<ThirdPersonController>();
    }

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    public void AddImpactH(Vector3 dir, float force)
    {
        dir.y = 0;
        impact += dir.normalized * force * 10 / mass;
    }

    public void AddImpactV(Vector3 dir, float force)
    {
        dir.x = 0;
        dir.z = 0;
        if(dir.y < 0)
        {
            dir.y = -dir.y;
        }
        impact += dir.normalized * force * 10 / mass;
    }
    void Update()
    {
        Debug.Log(impact.magnitude);
        if(impact.magnitude > 5 && !controllerScript.isDodging)
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
