using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileHandler : ScriptableObject
{
    //Does the projectile use gravity
    public bool useGrav;
    //The velocity the projectile is initialised with
    public float projectileSpeed;
    //The minimum possible velocity for this projectile
    public float minSpeed;
    //The maximum possible velocity for this projectile
    public float maxSpeed;
    //Sets the projectile being fired
    public GameObject projectile;
    public ProjectileBase projectileBase;
    public float projectileDamage;
    //The on hit effect applied by this projectile
    public List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();
    //The point the projectile originates from
    public Transform originPoint;
    //The point the projectile is aimed at
    public Vector3 target;

    public virtual void ShootProjectile() 
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
