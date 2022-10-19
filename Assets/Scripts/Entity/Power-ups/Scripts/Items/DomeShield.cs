using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Dome Shield")]

public class DomeShield : ActiveAbility
{
    public GameObject shield;

    public float duration;

    public override void effect()
    {
        GameManager.instance.player.GetComponent<PlayerAbilities>().StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect()
    {
        GameObject dome = Instantiate(shield, GameManager.instance.player.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(duration);
        Destroy(dome);
    }
}
