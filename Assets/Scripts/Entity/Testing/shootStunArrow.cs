using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootStunArrow : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileDamage;
    public List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();
    public bool useGrav;
    public GameObject projectile;
    public GameObject player;
    public Transform originPoint;
    public Vector3 target;

    private void Update()
    {
        target = player.transform.position + new Vector3 (0.0f, 2.0f, 0.0f);
        if (Input.GetKeyDown("b")) 
        {
            Debug.Log("pewpew");
            ShootProjectile();
        }
    }

    public void ShootProjectile()
    {
        var projectileObj = Instantiate(projectile, originPoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<ProjectileBase>().SetOnHitEffect(OnHitEffects);
        projectileObj.GetComponent<ProjectileBase>().SetDamage(projectileDamage);
        var rBody = projectileObj.GetComponent<Rigidbody>();
        rBody.velocity = (target - originPoint.position).normalized * projectileSpeed;
        if (useGrav)
        {
            rBody.useGravity = true;
        }
        else
        {
            rBody.useGravity = false;
        }
    }
}
