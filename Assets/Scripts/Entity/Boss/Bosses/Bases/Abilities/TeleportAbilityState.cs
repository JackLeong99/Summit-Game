using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Teleport State")]
public class TeleportAbilityState : AbilityState
{
    [Header("Values")]
    public Vector2 teleportRadius;

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
        boss.StartCoroutine(Teleport());
    }

    public IEnumerator Teleport()
    {
        boss.transform.position = GetRandomInRadius();
        yield return new WaitForSeconds(1);
        yield return new WaitForEndOfFrame();
        boss.ChangeState(boss.baseState);
    }

    public Vector3 GetRandomInRadius()
    {
        Vector2 randomPos = Random.insideUnitCircle * teleportRadius.y;
        return new Vector3(randomPos.x, 0, randomPos.y);
    }
}