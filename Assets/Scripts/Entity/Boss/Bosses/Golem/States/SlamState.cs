using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Slam")]
public class SlamState : AbilityState
{
    public GameObject shockwave;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();

        LookTowards(GameManager.player.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        boss.anim.SetTrigger("Slam");
        AkSoundEngine.PostEvent("Enemy_Melee_Overhead_Slam", boss.gameObject);

        boss.StartCoroutine(Slam());
    }

    IEnumerator Slam()
    {
        yield return new WaitForSeconds(delay);
        var hitbox = Instantiate(spawnableObject, boss.rightHand.transform.position, Quaternion.identity, boss.rightHand.transform);
        var hitbox2 = Instantiate(spawnableObject, boss.leftHand.transform.position, Quaternion.identity, boss.leftHand.transform);
        yield return new WaitForSeconds(duration);
        var wave = Instantiate(shockwave, GameObject.FindWithTag("Shockwave").transform.position, boss.transform.rotation);
        Destroy(hitbox);
        Destroy(hitbox2);

        yield return new WaitForSeconds(2f);
        boss.ChangeState(boss.baseState);
    }
}