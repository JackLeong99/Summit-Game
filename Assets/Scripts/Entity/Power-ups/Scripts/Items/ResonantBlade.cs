using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Resonant Blade")]

public class ResonantBlade : ActiveAbility
{
    public GameObject bladeObject;
    public float minDistance;
    public float maxDistance;
    public float spawnHeight;
    public override void effect()
    {
        Vector2 dir = Random.insideUnitCircle;
        Vector3 randomPos = (new Vector3(dir.x, 0, dir.y)) * Random.Range(minDistance, maxDistance);
        randomPos.y = spawnHeight;
        Instantiate(bladeObject, GameManager.instance.player.transform.position + randomPos, Quaternion.identity);
    }

    public override IEnumerator doEffect()
    {
        throw new System.NotImplementedException();
    }
}
