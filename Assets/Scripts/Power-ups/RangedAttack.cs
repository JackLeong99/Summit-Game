using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{

    public float damageModifier = 0.8f;
    private float damage;
    public float projectileSpeed = 30;


    public Transform FirePoint;
    private bool alreadyAttacked = false;
    
     //Without reference variable
    public GameObject projectile;
    AutoAttack aAttack;

    //to find target pos
    public Camera cam;
    private Vector3 destination;




    void Awake()
    {
        aAttack = GetComponent<AutoAttack>();
        damage = aAttack.attackDamage * damageModifier;
    }


    void Update()
    {
        if(aAttack.powerUpFlag == true)
        {
            if(alreadyAttacked == false)
            {
                alreadyAttacked = true;

                //from thirdpersonshooting
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f , 0));
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    destination = hit.point;
                }
                else
                {
                    destination = ray.GetPoint(1000);
                }

                InstantiateProjectile();
            }
            
            
        }
        else{
            alreadyAttacked = false;
        }
    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate (projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<PlayerBullet>().setDamage(damage);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
    }

}