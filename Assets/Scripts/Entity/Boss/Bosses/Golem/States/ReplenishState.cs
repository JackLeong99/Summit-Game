using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Replenish State")]
public class ReplenishState : AbilityState
{
    [Header("Information")]
    public int rocksToSpawn = 8;
    public int increase = 25;
    public float rockRangeDown = -50f;
    public float rockRangeUp = -25f;
    public float constant = -50f;

    [Header("Values")]
    public float spawnLength;

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

        boss.StartCoroutine(Replenish());
    }

    public IEnumerator Replenish()
    {
        yield return new WaitForSeconds(boss.anim.GetCurrentAnimatorStateInfo(0).length * spawnLength);

        for (int i = 1; i < rocksToSpawn / 2; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            rock.transform.position = new Vector3(Random.Range(rockRangeDown, rockRangeUp), 0, Random.Range(constant, constant + 50f));

            if (rocksToSpawn % 2 != 0)
            {
                constant = 0;
            }
            else
            {
                rockRangeDown += increase;
                rockRangeUp += increase;
                constant = -50f;
            }
        }

        yield return null;
    }
}