/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject parentObject;
    [SerializeField] public GameObject hitboxObject2;
    [SerializeField] public GameObject parentObject2;

    [SerializeField] public float duration;
    [SerializeField] public float warmup;

    private Shockwave shockwave;

    public void groundSlam()
    {
        shockwave = gameObject.GetComponent<Shockwave>();
        StartCoroutine(slam());
    }

    IEnumerator slam()
    {
        yield return new WaitForSeconds(warmup);
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, Quaternion.identity, parentObject.transform);
        var hitbox2 = Instantiate(hitboxObject2, parentObject2.transform.position, Quaternion.identity, parentObject2.transform);
        yield return new WaitForSeconds(duration);
        shockwave.instantiateShockwave();
        Destroy(hitbox);
        Destroy(hitbox2);
    }   
}
*/