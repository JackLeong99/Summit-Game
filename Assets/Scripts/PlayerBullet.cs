using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MaxLifetime = 100;
    private bool dead;

    void Start()
    {
        
    }


    void Update()
    {
        // for(float i = MaxLifetime; i >= 0; i++)
        // {
        //     if(i == 0)
        //     {
        //         CullProjectile();
        //     }
        // }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "PlayerBullet" && hit.gameObject.tag != "Player" && !dead)
        {
            CullProjectile();
        }
    }

    void CullProjectile()
    {
        dead = true;
        Destroy (gameObject);
    }
}
