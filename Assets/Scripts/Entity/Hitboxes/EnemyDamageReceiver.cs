using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
    public BossStateMachine boss;
    public GameObject onHitParticles;
    public static event Action OnDamageTaken;
    public bool weakSpot;
    public float stunLockoutTime;
    private bool stunLockout;
    public Material mat;
    
    void Start() 
    {
        boss = GetComponentInParent<BossStateMachine>();
        gameObject.tag = "enemyHitbox";
        switch (true) 
        {
            case bool _ when GetComponent<MeshRenderer>():
                mat = GetComponent<MeshRenderer>().material;
                break;
            case bool _ when GetComponent<SkinnedMeshRenderer>():
                mat = GetComponent<SkinnedMeshRenderer>().material;
                break;
        }
    }
    public void PassDamage(float dmg, Vector3 position)
    {
        if (!boss.Alive()) return;
        if (weakSpot) StartCoroutine(tryStun());
        boss.TakeDamage(dmg, position);
        DamageHandler();
    }

    public void PassDamage(float[] dmg, float tickRate, Vector3 position)
    {
        if (!boss.Alive()) return;
        //if (weakSpot) boss.components.stunState = StunState.Stunned;
        boss.TakeDamage(dmg, tickRate, position);
        DamageHandler();
    }

    public IEnumerator tryStun() 
    {
        if (!stunLockout && boss.components.stunState != StunState.Stunned) 
        {
            boss.components.stunState = StunState.Stunned;
            mat.DisableKeyword("_EMISSION");
            stunLockout = true;
            yield return new WaitForSeconds(stunLockoutTime);
            mat.EnableKeyword("_EMISSION");
            stunLockout = false;
        }
        yield return null;
    }

    public void DamageHandler()
    {
        Instantiate(onHitParticles, gameObject.transform.position, Quaternion.identity);
        OnDamageTaken?.Invoke();
    }
}
