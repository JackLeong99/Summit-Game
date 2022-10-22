using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "ActiveAbilities/BasicShoot")]

public class BasicShoot : ActiveAbility
{
    private GameObject player;
    private ThirdPersonController controller;
    private Animator animator;
    private Inventory inventory;
    [SerializeField]
    private float chargeTime;
    [SerializeField]
    private float shootTime;
    [SerializeField]
    private GameObject projectile;
    private Transform FirePoint;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float projectileVelocity;
    //[SerializeField]
    //private List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();

    public override void effect()
    {
        player = GameManager.instance.player;
        controller = player.GetComponent<ThirdPersonController>();
        animator = player.GetComponent<Animator>();
        inventory = player.GetComponent<Inventory>();

        FirePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
        GameManager.instance.player.GetComponent<PlayerAbilities>().StartCoroutine(doEffect());
    }

    public override IEnumerator doEffect()
    {
        this.castTime = chargeTime + shootTime;
        player.transform.rotation = GameManager.instance.mainCamera.transform.rotation;
        controller.LockCameraPosition = true;
        controller.stunned = ThirdPersonController.stunState.Stunned;
        animator.SetTrigger("Charge");
        AkSoundEngine.PostEvent("Player_Shoot_Charge", player);
        yield return new WaitForSeconds(chargeTime);
        animator.SetTrigger("Shoot");
        AkSoundEngine.PostEvent("Player_Shoot_Fire", player);
        CameraListener.instance.CameraShake(5, 0.1f);
        Ray ray = GameManager.instance.mainCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 destination;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 0))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        var projectileObj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<ProjectileBase>().SetOnHitEffect(inventory.abilityOnHitEffects);
        projectileObj.GetComponent<ProjectileBase>().SetDamage((damage + Inventory.instance.abilityDamage) * Inventory.instance.percentDamageMod);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileVelocity;
        //if (useGrav)
        //{
        //    projectileObj.GetComponent<Rigidbody>().useGravity = true;
        //}
        yield return new WaitForSeconds(shootTime);
        controller.LockCameraPosition = false;
        controller.stunned = ThirdPersonController.stunState.Actionable;
    }
}
