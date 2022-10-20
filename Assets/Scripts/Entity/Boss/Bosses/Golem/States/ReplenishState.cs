using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Replenish State")]
public class ReplenishState : AbilityState
{
    [Header("Information")]
    public int rocksToSpawn;
    public float spawnRadius;

    [Header("References")]
    public GameObject rockPrefab;

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
        base.Setup();

        boss.callbackEvent.AddListener(CallbackEvent);
    }

    public void CallbackEvent()
    {
        boss.StartCoroutine(Replenish());
    }

    public IEnumerator Replenish()
    {
        for (int i = 0; i < rocksToSpawn; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            Vector2 spawnPos = Random.insideUnitCircle * spawnRadius;
            rock.transform.position = new Vector3(spawnPos.x, 0, spawnPos.y);
        }

        yield return null;
    }
}