using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MaxLifetime = 1000;
    public float CurrentLifetime = 0;
    private float currentGunDamage;

    public void setDamage(float dmg)
    {
        currentGunDamage = dmg;
    }

    void Update()
    {
        CurrentLifetime += Time.deltaTime;
        if (CurrentLifetime >= MaxLifetime)
        {
            Destroy (gameObject);
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "PlayerBullet" && hit.gameObject.tag != "Player")
        {
            AkSoundEngine.PostEvent("Player_Shoot_Impact", gameObject);
            Destroy (gameObject);
        }
    }
    //Check for enemy damage and deal damage to the enemy if it hits them.

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyHitbox")
        {
            EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
            if (receiver)
            {
                receiver.PassDamage(currentGunDamage);
                Destroy(gameObject);
            }
        }
    }
}
