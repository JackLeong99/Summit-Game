using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerAttack : MonoBehaviour
{
    //this script is set onto the boss
    private BossManager damage;
    // Start is called before the first frame update
    void Start()
    {
        damage=this.GetComponent<BossManager>();
    }

    public void attackIncrease()
    {
        damage.DamageIncrease();
    }
}
