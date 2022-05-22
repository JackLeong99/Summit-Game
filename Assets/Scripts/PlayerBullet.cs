using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MaxLifetime = 1000;
    public float CurrentLifetime = 0;
    private int currentGunDamage;

    public void setDamage(int dmg)
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
            Destroy (gameObject);
        }
    }
    //Check for enemy damage and deal damage to the enemy if it hits them.

    private void OnTriggerEnter(Collider other){
        Debug.Log("GunHit!");
        //If the projectile hits an enemy:
        if(other.tag == "Hittable"){
            Debug.Log("EnemyHit!");
            //Store the gameObject that is hit in a variable
            HealthTemp healthTemp = other.GetComponent<HealthTemp>();

            //Deal damage if a health script exists.
            if (healthTemp != null){
                GameManager.Instance.GunDamge(currentGunDamage);
                healthTemp.takeDamage(currentGunDamage);
            }
            //Destroy the projectile if a Hittable is hit. Don't want to pass through a player
            //intended trigger and be destroyed.
            Destroy (gameObject);
        }
    }
}
