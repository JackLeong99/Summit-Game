using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Mend & Maim")]

public class MendMaim : EventItem
{
    public float baseDamage;
    public float damagePercent;
    public float activationRange;
    public float cooldown;
    private bool Valid;

    public override void acquire()
    {
        base.acquire();

        Valid = true;
    }

    public override void subscribe()
    {
        EventManager.instance.OnTakeDamage.AddListener(effect);
    }

    public override void effect()
    {
        throw new System.NotImplementedException();
    }

    public void effect(float f)
    {
        if (!Valid) { return; }
        if (Vector3.Distance(GameManager.instance.player.transform.position, BossManager.instance.boss.transform.position) >= activationRange) { Debug.Log("Whoop");  return; }

        // original formula: float[] damage = {baseDamage + (f * (1 - (100/(100 + (damagePercent * Inventory.instance.GetStacks(this))))))};
        float[] damage = {(baseDamage + (f * (damagePercent * Inventory.instance.GetStacks(this)))) * Inventory.instance.percentDamageMod};
        GameObject[] gos = GameObject.FindGameObjectsWithTag("enemyHitbox");
        int i = Random.Range(0, gos.Length);
        gos[i].GetComponent<EnemyDamageReceiver>().PassDamage(damage, 1, gos[i].transform.position);

        GameManager.instance.player.GetComponent<Inventory>().StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        Valid = false;
        yield return new WaitForSeconds(cooldown);
        Valid = true;
    }
}
