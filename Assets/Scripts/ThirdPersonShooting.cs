using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class ThirdPersonShooting : MonoBehaviour
{
    [SerializeField] int bulletDamage;
    public Camera cam;

    public GameObject projectile;

    public TextMeshProUGUI CdDisplay;

    public Transform FirePoint;

    public Image CdBackground;

    public float projectileSpeed = 30;

    public float shotTimeBuffer = 0f;

    public float shotAnimEnd = 0f;

    public float cooldown = 0;
    private float cdTimer = 0;
    private Vector3 destination;

    private ThirdPersonController TPCScript;

    public Color OffCD;
    public Color OnCD;

    // Start is called before the first frame update
    void Start()
    {
        CdDisplay.text = "";
        CdBackground.color = OffCD;
        TPCScript = this.gameObject.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        CooldownHandler();

        if(Input.GetButtonDown("Spell1") && cdTimer <= 0 && !TPCScript._Inactionable &&TPCScript.Grounded && !TPCScript.isAttacking)
        {
            StartCoroutine(ShootProjectile());
        }
    }

    void CooldownHandler()
    {
        cdTimer -= Time.deltaTime;

        if(cdTimer <=0)
        {
            cdTimer = 0;
            CdDisplay.text = "";
            CdBackground.color = OffCD;
        }
        else
        {
            CdDisplay.text = cdTimer.ToString("0");
            CdBackground.color = OnCD;
        }
    }

    IEnumerator ShootProjectile()
    {
        Quaternion originalR = TPCScript._controller.transform.rotation;
        TPCScript._controller.transform.rotation = TPCScript._mainCamera.transform.rotation;
        TPCScript._animator.SetTrigger("Shoot");
        TPCScript._Inactionable = true;

        yield return new WaitForSeconds(shotTimeBuffer);

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
        
        TPCScript._controller.transform.rotation = originalR;
        TPCScript._Inactionable = false;
        cdTimer = cooldown;

    }

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate (projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<PlayerBullet>().setDamage(bulletDamage);
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
    }
}
