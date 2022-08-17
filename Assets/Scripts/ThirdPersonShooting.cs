using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class ThirdPersonShooting : MonoBehaviour
{
    public bool useGrav;

    public float bulletDamage;

    public float chargeTime;

    public float succTime;

    public ParticleSystem succ;

    public Camera cam;

    public GameObject projectile;

    public TextMeshProUGUI CdDisplay;

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
    public Color OffCD;
    public Color OnCD;

    private ThirdPersonController controller;

    private Animator _animator;

    private GameObject _mainCamera;

    //Temporary code that resets player y axis rotation until we add custom player model/animations
    private CharacterController player;

    private ParticleSystem.EmissionModule pp;

    private void Awake()
	{
		if (_mainCamera == null)
		{
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}
	}

    void Start()
    {
        CdDisplay.text = "";
        CdBackground.color = OffCD;
        _animator = GetComponent<Animator>();
        //Temporary code that resets player y axis rotation until we add custom player model/animations
        player = GetComponent<CharacterController>();
        controller = GetComponent<ThirdPersonController>();
        pp = succ.emission;
        pp.enabled = false;
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
            CdDisplay.text = "";
            CdBackground.color = OffCD;
        }
        else
        {
            CdDisplay.text = (cdTimer+1).ToString("0");
            CdBackground.color = OnCD;
            onCooldown = true;
        }
    }

    IEnumerator ShootProjectile()
    {
        player.transform.rotation = _mainCamera.transform.rotation;
        controller.LockCameraPosition = true;
        casting = true;
        _animator.SetTrigger("Charge");
        AkSoundEngine.PostEvent("Player_Shoot_Charge", gameObject);
        yield return new WaitForSeconds(succTime);
        pp.enabled = true;
        yield return new WaitForSeconds(chargeTime);
        pp.enabled = false;
        _animator.SetTrigger("Shoot");
        AkSoundEngine.PostEvent("Player_Shoot_Fire", gameObject);
        //AkSoundEngine.PostEvent("Player_Shoot_Cast", gameObject);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f , 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
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
        projectileObj.GetComponent<PlayerBullet>().setDamage(bulletDamage);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
        if (useGrav) 
        {
            projectileObj.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
