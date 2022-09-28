using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "ActiveAbilities/BasicAttack")]

public class BasicAttack : ActiveAbility
{
    private GameObject player;
    private ThirdPersonController controller;
    private Animator animator;
    private PlayerAbilities playerAbilities;
    [Header("For now set cooldown to swing animation duration")]
    public float damage;
    public float chainWindow;
    public GameObject attackBox;

    //this whole chain thing needs to be reworked
    [HideInInspector]
    public enum AttackStates { stage1, stage2 }
    [HideInInspector]
    public AttackStates attackState;

    public override void effect()
    {
        player = GameManager.instance.player;
        controller = GameManager.instance.player.GetComponent<ThirdPersonController>();
        animator = GameManager.instance.player.GetComponent<Animator>();
        playerAbilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        playerAbilities.StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect()
    {
        AkSoundEngine.PostEvent("Player_Attack", player);
        //animator.speed = ? cooldown;
        switch (attackState) 
        {
            case AttackStates.stage1:
                animator.SetTrigger("attack0");
                playerAbilities.StartCoroutine(createChainWindow(cooldown + chainWindow));
                attackState = AttackStates.stage2;
                break;

            case AttackStates.stage2:
                animator.SetTrigger("attack1");
                attackState = AttackStates.stage1;
                break;
        }
        controller._Inactionable = true;
        yield return new WaitForSeconds(0.2f);
        var hitbox = Instantiate(attackBox, player.transform.position + new Vector3(0, 1.3f, 0), player.transform.rotation, player.transform);
        hitbox.transform.localPosition += new Vector3(0, 0, 1.5f);
        hitbox.GetComponent<PlayerDamage>().setDamage(damage);
        yield return new WaitForSeconds(cooldown);
        if (hitbox)
        {
            Destroy(hitbox);
        }
        controller._Inactionable = false;
        //animator.speed = 1;
    }

    public IEnumerator createChainWindow(float t) 
    {
        yield return new WaitForSeconds(t);
        attackState = AttackStates.stage1;
    }
}
