using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class ThirdPersonShooting : MonoBehaviour
{
    public bool useGrav;

    public float bulletDamage;

    public float chargeTime;

    public float succTime;

    //public ParticleSystem succ;

    public GameObject projectile;

    public Transform FirePoint;

    public Image CdBackground;

    public float projectileSpeed;

    public float shotTimeBuffer;

    public float shotAnimEnd;

    public float cooldown;
    [HideInInspector]
    public float cdTimer;
    [HideInInspector]
    public bool casting;
    [HideInInspector]
    public bool onCooldown;
    private Vector3 destination;

    private ThirdPersonController controller;

    private Animator _animator;

    //Temporary code that resets player y axis rotation until we add custom player model/animations
    private CharacterController player;

    //private ParticleSystem.EmissionModule pp;

    public List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        //Temporary code that resets player y axis rotation until we add custom player model/animations
        player = GetComponent<CharacterController>();
        controller = GetComponent<ThirdPersonController>();
        //pp = succ.emission;
        //pp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CooldownHandler();
        projectileSpeed = Mathf.Clamp(projectileSpeed, 20f, 300f);

    }

    public void CastShoot()
    {
        StartCoroutine(ShootProjectile());
    }

    void CooldownHandler()
    {
        cdTimer -= Time.deltaTime;

        if(cdTimer <=0)
        {
            cdTimer = 0;
            onCooldown = false;
        }
        else
        {
            onCooldown = true;
        }
    }

    IEnumerator ShootProjectile()
    {
        player.transform.rotation = GameManager.instance.mainCamera.transform.rotation;
        controller.LockCameraPosition = true;
        casting = true;
        _animator.SetTrigger("Charge");
        AkSoundEngine.PostEvent("Player_Shoot_Charge", gameObject);
        yield return new WaitForSeconds(succTime);
        //pp.enabled = true;
        yield return new WaitForSeconds(chargeTime);
        //pp.enabled = false;
        _animator.SetTrigger("Shoot");
        AkSoundEngine.PostEvent("Player_Shoot_Fire", gameObject);
        CameraListener.instance.CameraShake(5, 0.1f);
        //AkSoundEngine.PostEvent("Player_Shoot_Cast", gameObject);
        Ray ray = GameManager.instance.mainCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f , 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, 0))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateProjectile();
        
        yield return new WaitForSeconds(shotAnimEnd);
        controller.LockCameraPosition = false;
        casting = false;
        cdTimer = cooldown;

    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate (projectile, FirePoint.position, Quaternion.identity) as GameObject;
        //projectileObj.GetComponent<PlayerBullet>().setDamage(bulletDamage);
        projectileObj.GetComponent<ProjectileBase>().SetOnHitEffect(OnHitEffects);
        projectileObj.GetComponent<ProjectileBase>().SetDamage(bulletDamage);
        if (projectileObj.GetComponent<ProjectileBase>()) 
        {
            Debug.Log("Got ProjectileBase Component");
        }
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
        if (useGrav) 
        {
            projectileObj.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
