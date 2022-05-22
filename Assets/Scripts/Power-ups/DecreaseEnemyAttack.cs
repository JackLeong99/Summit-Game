using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnemyAttack : MonoBehaviour
{
    private PlayerStats damage;
    // Start is called before the first frame update
    void Start()
    {
        damage=this.GetComponent<PlayerStats>();
    }

    //call this function on enable and disable in the power-up manager
    public void DecreaseAttack()
    {
        damage.DecreaseDamage();
    }
}
