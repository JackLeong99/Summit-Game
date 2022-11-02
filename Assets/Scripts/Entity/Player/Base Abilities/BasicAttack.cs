using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "ActiveAbilities/BasicAttack")]

public class BasicAttack : ActiveAbility
{
    private ThirdPersonController controller;
    private Animator animator;
    [Header("For now set cooldown to swing animation duration")]
    public float chainWindow;
    public float animationTime;

    //this whole chain thing needs to be reworked
    public enum AttackStates { stage1, stage2 }
    [HideInInspector]
    public AttackStates attackState;

    public override void effect()
    {
        controller = GameManager.instance.player.GetComponent<ThirdPersonController>();
        animator = GameManager.instance.player.GetComponent<Animator>();
        controller.StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect()
    {
        this.castTime = animationTime;
        AkSoundEngine.PostEvent("Player_Attack", controller.gameObject);
        switch (attackState) 
        {
            case AttackStates.stage1:
                animator.SetTrigger("attack0");
                controller.StartCoroutine(createChainWindow(castTime + chainWindow));
                attackState = AttackStates.stage2;
                break;

            case AttackStates.stage2:
                animator.SetTrigger("attack1");
                attackState = AttackStates.stage1;
                break;
        }
        controller.stunned = ThirdPersonController.stunState.Stunned;
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(cooldown);
        controller.stunned = ThirdPersonController.stunState.Actionable;
    }

    public IEnumerator createChainWindow(float t) 
    {
        yield return new WaitForSeconds(t);
        attackState = AttackStates.stage1;
    }
}
