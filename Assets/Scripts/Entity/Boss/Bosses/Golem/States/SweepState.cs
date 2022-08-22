using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Sweep")]
public class SweepState : AbilityState
{
    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        boss.anim.SetTrigger("Sweep");
        AkSoundEngine.PostEvent("Enemy_Melee_Right_Hook", boss.gameObject);

        boss.StartCoroutine(Sweep());
    }

    IEnumerator Sweep()
    {
        yield return new WaitForSeconds(delay);
        var hitbox = Instantiate(spawnableObject, boss.leftHand.transform.position, boss.leftHand.transform.rotation, boss.leftHand.transform);
        var hitboxB = Instantiate(spawnableObject, boss.rightHand.transform.position, boss.rightHand.transform.rotation, boss.rightHand.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
        Destroy(hitboxB);

        yield return new WaitForSeconds(2f);
        boss.ChangeState(boss.baseState);
    }
}