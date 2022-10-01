using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "ActiveAbilities/BasicDodge")]

public class BasicDodge : ActiveAbility
{
    private GameObject player;
    private ThirdPersonController controller;
    private Animator animator;
    [SerializeField]
    private AnimationCurve dodgeCurve;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float percentInvulnerable;

    public override void effect()
    {
        player = GameManager.instance.player;
        controller = GameManager.instance.player.GetComponent<ThirdPersonController>();
        animator = GameManager.instance.player.GetComponent<Animator>();
        GameManager.instance.player.GetComponent<PlayerAbilities>().StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect()
    {
        float totalSpeed = speed + controller.SprintSpeed;
        float dodgeTimer = distance / totalSpeed;
        this.castTime = dodgeTimer;
        AkSoundEngine.PostEvent("Player_Dodge", player);
        controller._Inactionable = true;
        player.GetComponent<PlayerHealth>().invulnerable = true;
        player.GetComponent<KnockbackReciever>().invulnerable = true;
        animator.SetTrigger("Dodge");
        animator.speed = 1/dodgeTimer;
        float timer = 0;
        while (timer < dodgeTimer)
        {
            if (timer >= (dodgeTimer / percentInvulnerable))
            {
                player.GetComponent<PlayerHealth>().invulnerable = false;
                player.GetComponent<KnockbackReciever>().invulnerable = false;
            }
            Vector3 dir = (dodgeCurve.Evaluate(timer) * player.transform.forward);
            player.GetComponent<CharacterController>().Move(dir * (Time.deltaTime*(distance / dodgeTimer)));
            timer += Time.deltaTime;
            yield return null;
        }
        controller._Inactionable = false;
        animator.speed = 1;
    }
}
