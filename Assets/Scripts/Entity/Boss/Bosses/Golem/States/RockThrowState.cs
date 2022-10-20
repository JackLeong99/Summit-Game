using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Rock Throw State")]
public class RockThrowState : AbilityState
{
    [Header("Values")]
    public SpawnPosition spawnPos;
    public enum SpawnPosition { Left, Right }

    [Header("References")]
    public GameObject projectileObject;
    private GameObject rock;

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

        rock = GetNearestEntity();
        rock.transform.parent = boss.rightHand.transform;
        rock.transform.localPosition = Vector3.zero;

        boss.callbackEvent.AddListener(CallbackEvent);
    }

    public void CallbackEvent()
    {
        boss.StartCoroutine(SpawnProjectile(rock));
    }

    public IEnumerator SpawnProjectile(GameObject rock)
    {
        Vector3 target = GameManager.instance.player.transform.position;
        Vector3 pos = spawnPos == SpawnPosition.Left ? boss.leftHand.transform.position : boss.rightHand.transform.position;

        Destroy(rock);

        var spawnable = Instantiate(projectileObject, pos, Quaternion.identity, boss.transform);
        spawnable.transform.parent = null;
        spawnable.GetComponent<Rigidbody>().velocity = (target - pos).normalized * 150;

        yield return null;
    }

    public GameObject GetNearestEntity()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("rocks");
        float distance = 0;
        int index = 0;

        for (int i = 0; i < entities.Length; i++)
        {
            float currentDist = Vector3.Distance(entities[i].transform.position, boss.transform.position);

            switch (true)
            {
                case bool x when i == 0:
                case bool y when currentDist < distance:
                    distance = currentDist;
                    index = i;
                    break;
            }
        }

        return entities[index];
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