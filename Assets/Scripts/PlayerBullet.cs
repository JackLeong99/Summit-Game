using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MaxLifetime = 1000;
    public float CurrentLifetime = 0;
    private bool dead;

    void Start()
    {

    }


    void Update()
    {
        CurrentLifetime += Time.deltaTime;
        if (CurrentLifetime >= MaxLifetime)
        {
            CullProjectile();
        }
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
