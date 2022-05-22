using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    [SerializeField] public GameObject hitboxObject;
    [SerializeField] public GameObject parentObject;
    [SerializeField] public GameObject hitboxObject2;
    [SerializeField] public GameObject parentObject2;

    [SerializeField] public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(Input.GetKeyDown("y"))
    //     {
    //         groundSlam();
    //     } 
    // }

    public void groundSlam()
    {
        StartCoroutine(slam());
    }

    IEnumerator slam()
    {
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, Quaternion.identity, parentObject.transform);
        var hitbox2 = Instantiate(hitboxObject2, parentObject2.transform.position, Quaternion.identity, parentObject2.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
        Destroy(hitbox2);
    }
}
