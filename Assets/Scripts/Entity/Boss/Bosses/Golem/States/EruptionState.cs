using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Ability/Golem/Eruption")]
public class EruptionState : AbilityState
{
    public GameObject warning;

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
        boss.anim.SetTrigger("Eruption");
        AkSoundEngine.PostEvent("Enemy_Eruption_Cast", boss.gameObject);

        boss.StartCoroutine(Eruption());
    }

    public IEnumerator Eruption()
    {
        GameObject target = GameManager.instance.player;
        int layerMask = 1 << layer;

        RaycastHit hit;
        if (Physics.Raycast(target.transform.position, boss.transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Vector3 targetPoint = hit.point;
            var prehit = Instantiate(warning, hit.point, Quaternion.identity);
            AkSoundEngine.PostEvent("Enemy_Eruption_Lava", prehit);
            prehit.transform.localPosition += new Vector3(0, -0.3f, 0);
            yield return new WaitForSeconds(delay);
            var hitbox = Instantiate(spawnableObject, prehit.transform.position, Quaternion.identity);
            hitbox.transform.localPosition += new Vector3(0, -0.05f, 0);
            yield return new WaitForSeconds(duration);
            Destroy(prehit);
            Destroy(hitbox);
            Debug.DrawRay(target.transform.position, boss.transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow, 3);
        }
        else
        {
            var prehit = Instantiate(warning, target.transform.position, Quaternion.identity);
            AkSoundEngine.PostEvent("Enemy_Eruption_Lava", prehit);
            prehit.transform.localPosition += new Vector3(0, -0.3f, 0);
            yield return new WaitForSeconds(delay);
            var hitbox = Instantiate(spawnableObject, prehit.transform.position, Quaternion.identity);
            hitbox.transform.localPosition += new Vector3(0, -0.05f, 0);
            yield return new WaitForSeconds(duration);
            Destroy(prehit);
            Destroy(hitbox);
        }

        yield return new WaitForSeconds(2);
        boss.ChangeState(boss.baseState);
    }
}