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
    private Shockwave shockwave;
    //private bool groundSlamming = false;
    
    private void Awake(){
        shockwave = GetComponent<Shockwave>();
    }

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
        //StartCoroutine(scuffedTimer());
    }


    IEnumerator slam()
    {
        var hitbox = Instantiate(hitboxObject, parentObject.transform.position, Quaternion.identity, parentObject.transform);
        var hitbox2 = Instantiate(hitboxObject2, parentObject2.transform.position, Quaternion.identity, parentObject2.transform);
        yield return new WaitForSeconds(duration);
        Destroy(hitbox);
        Destroy(hitbox2);
    }
    // IEnumerator scuffedTimer()
    // {
    //     Debug.Log("Start Timer");
    //     yield return new WaitForSeconds(.2f);

    //     groundSlamming = true;
    //     Debug.Log("Done Timer");
    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     //GameObject other = collider.gameObject;
    //     Debug.Log(other.tag);
    //     Debug.Log(other);
    //     Debug.Log(groundSlamming);
    //     if(other.tag=="Arena" && groundSlamming)
    //     {
            
    //         shockwave.instantiateShockwave();
    //         groundSlamming = false;
    //     }
    
}
