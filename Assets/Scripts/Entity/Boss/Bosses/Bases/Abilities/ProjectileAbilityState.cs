using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Projectile AbilityState")]
public class ProjectileAbilityState : AbilityState
{
    [Header("Values")]
    public float spawnLength;
    public float projectileSpeed;
    public SpawnPosition spawnPos = SpawnPosition.Left;
    public enum SpawnPosition { Left, Right }

    [Header("References")]
    public GameObject projectileObject;

    public override void Invoke(BossStateMachine boss)
    {
        base.Invoke(boss);

        Setup();
    }

    public override void Update()
    {
        base.Update();

        boss.transform.rotation = RotateTowards();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Setup()
    {
        base.Setup();

        boss.StartCoroutine(SpawnProjectile());
    }

    public IEnumerator SpawnProjectile()
    {
        yield return new WaitForSeconds(boss.anim.GetCurrentAnimatorStateInfo(0).length * spawnLength);

        Vector3 target = GameManager.instance.player.transform.position;

        Vector3 pos = spawnPos == SpawnPosition.Left ? boss.leftHand.transform.position : boss.rightHand.transform.position;

        var spawnable = Instantiate(projectileObject, pos, Quaternion.identity, boss.transform);
        spawnable.transform.parent = null;
        spawnable.GetComponent<Rigidbody>().velocity = (target - pos).normalized * projectileSpeed;

        yield return null;
    }

    public Quaternion RotateTowards()
    {
        Vector3 dir = GameManager.instance.player.transform.position - boss.transform.position;
        Quaternion rot = Quaternion.Slerp(boss.transform.rotation, Quaternion.LookRotation(dir), 150 * Time.deltaTime);
        rot.x = 0;
        rot.z = 0;

        return rot;
    }
}