using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerAttack : MonoBehaviour
{
    //this script is set onto the boss
    private HealthTemp damage;
    // Start is called before the first frame update
    void Start()
    {
        damage=this.GetComponent<HealthTemp>();
    }

    public void attackIncrease()
    {
        damage.DamageIncrease();
    }
}
