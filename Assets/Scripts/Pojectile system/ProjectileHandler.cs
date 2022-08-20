using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileHandler : ScriptableObject
{
    //Does the projectile use gravity
    protected bool useGrav;
    //The velocity the projectile is initialised with
    protected float projectileSpeed;
    //The minimum possible velocity for this projectile
    protected float minSpeed;
    //The maximum possible velocity for this projectile
    protected float maxSpeed;
    //Sets the projectile being fired
    protected GameObject projectile;
    //The on hit effect applied by this projectile
    //(this needs to be updated to a class type variable once the onhiteffect class is implemented)
    protected GameObject OnHitEffect;
    //The point the projectile originates from
    protected Transform originPoint;
    //The point the projectile is aimed at
    protected Vector3 target;

    public virtual void ShootProjectile() 
    {
        var projectileObj = Instantiate(projectile, originPoint.position, Quaternion.identity) as GameObject;
        //projectileObj.collisionHandler.setOnHitEffect(OnHitEffect)
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
