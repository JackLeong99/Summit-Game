using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRock : MonoBehaviour
{
    public int rockHealth;
    public GameObject sparks;
    public GameObject healBurst;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sword") || other.CompareTag("PlayerBullet"))
        {
            rockHealth--;
            Instantiate(sparks, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
            AkSoundEngine.PostEvent("Enemy_Damage", gameObject);
        }
        if (rockHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.player.GetComponent<PlayerHealth>().healDamage(5.0f);
            Instantiate(healBurst, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            AkSoundEngine.PostEvent("Enemy_Damage", gameObject);
        }
    }
}
