using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Spawnable AbilityState")]
public class SpawnAbilityState : AbilityState
{
    [Header("Attributes")]
    public string spawnSoundEvent;

    [Header("Values")]
    public LayerMask layerMask = 6;
    public SpawnPosition spawnPos = SpawnPosition.Ground;
    public enum SpawnPosition { Ground, Target, Random }

    [Header("References")]
    public GameObject spawnableObject;

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
        boss.StartCoroutine(SpawnObject());
    }

    public IEnumerator SpawnObject()
    {

        Vector3 target = GameManager.instance.player.transform.position;

        switch (spawnPos)
        {
            case SpawnPosition.Ground:
                RaycastHit hit;

                if (Physics.Raycast(target, boss.transform.TransformDirection(Vector3.down), out hit, 1000, layerMask))
                {
                    target = hit.point;
                }
                break;
        }

        var spawnable = Instantiate(spawnableObject, target, Quaternion.identity, boss.transform);
        spawnable.transform.parent = null;

        switch (true)
        {
            case bool _ when spawnSoundEvent.Length != 0:
                AkSoundEngine.PostEvent(spawnSoundEvent, spawnable.gameObject);
                break;
        }

        yield return new WaitForEndOfFrame();
    }
}