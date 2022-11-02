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
    public GameObject weakSpotGO;
    public Material mat;
    public float lerpDuration;
    public Color defaultColor;
    public Color offColor;

    void Start() 
    {
        boss = GetComponentInParent<BossStateMachine>();
        gameObject.tag = "enemyHitbox";
        if (!weakSpotGO) weakSpotGO = gameObject;
        switch (true) 
        {
            case bool _ when weakSpotGO.GetComponent<MeshRenderer>():
                mat = weakSpotGO.GetComponent<MeshRenderer>().material;
                break;
            case bool _ when weakSpotGO.GetComponent<SkinnedMeshRenderer>():
                mat = weakSpotGO.GetComponent<SkinnedMeshRenderer>().material;
                break;
        }
    }
    public void PassDamage(float dmg, Vector3 position, bool canStun)
    {
        if (!boss.Alive()) return;
        if (weakSpot && canStun) StartCoroutine(tryStun());
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
            mat.SetColor("_EmissionColor", offColor);
            stunLockout = true;
            yield return new WaitForSeconds(stunLockoutTime);
            StartCoroutine(lerpColor(lerpDuration, mat));
            stunLockout = false;
        }
        yield return null;
    }

    public IEnumerator lerpColor(float d, Material m)
    {
        float t = 0;
        while (t < d)
        {
            m.SetColor("_EmissionColor", Color.Lerp(offColor, defaultColor, t));
            yield return t += Time.deltaTime;
        }
        yield return null;
    }

    public void DamageHandler()
    {
        Instantiate(onHitParticles, gameObject.transform.position, Quaternion.identity);
        OnDamageTaken?.Invoke();
    }
}
