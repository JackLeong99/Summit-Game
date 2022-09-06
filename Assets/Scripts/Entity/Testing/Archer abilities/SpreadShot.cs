using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileDamage;
    public float angle;
    public List<OnHitEffect> OnHitEffects = new List<OnHitEffect>();
    public List<GameObject> projectiles;
    public GameObject player;
    public Transform originPoint;
    public Vector3 target;

    private void Update()
    {
        target = player.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        if (Input.GetKeyDown("v"))
        {
            Debug.Log("pewpew");
            ShootProjectiles();
        }
    }

    public void ShootProjectiles()
    {
        float totalAngle = (angle * (projectiles.Count - 1))/2;
        List<GameObject> instantiatedProjectiles = new List<GameObject>();
        foreach (var obj in projectiles) 
        {
            var projectileObj = Instantiate(obj, originPoint.position, Quaternion.identity) as GameObject;
            instantiatedProjectiles.Add(projectileObj);
        }
        foreach (var obj in instantiatedProjectiles) 
        {
            obj.GetComponent<ProjectileBase>().SetOnHitEffect(OnHitEffects);
            obj.GetComponent<ProjectileBase>().SetDamage(projectileDamage);
            var targetDir = (target - originPoint.position).normalized;
            var angleDir = Vector3.Cross(targetDir, Vector3.left);
            var releaseVector = Quaternion.AngleAxis(totalAngle, angleDir) * targetDir;
            obj.GetComponent<Rigidbody>().velocity = releaseVector * projectileSpeed;
            totalAngle -= angle;
        }
    }
}
