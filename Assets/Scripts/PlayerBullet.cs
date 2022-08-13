using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float MaxLifetime = 1000;
    public float CurrentLifetime = 0;
    private float currentGunDamage;
    [SerializeField] GameObject boom;
    private BossStateMachine boss;

    void Awake(){
        boss = GameObject.FindWithTag("Boss").GetComponent<BossStateMachine>(); //same as before except added boss the gameObject before getComponent
    }

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
            boomFX();
            Destroy (gameObject);
        }
    }
    //Check for enemy damage and deal damage to the enemy if it hits them.

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemyHitbox")
        {
            boss.components.stunState = StunState.Stunned;
            EnemyDamageReceiver receiver = other.GetComponent<EnemyDamageReceiver>();
            if (receiver)
            {
                receiver.PassDamage(currentGunDamage, transform.position);
                boomFX();
                Destroy(gameObject);
            }
        }
        else 
        {
            boomFX();
            Destroy(gameObject);
        }
    }

    void boomFX() 
    {
        Instantiate(boom, gameObject.transform.position, Quaternion.identity);
        AkSoundEngine.PostEvent("Player_Shoot_Impact", gameObject);
    }
}
