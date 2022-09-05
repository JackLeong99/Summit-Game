using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisOrb : MonoBehaviour
{
    private float timer=15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<=0)
        {
            OrbSpawner.Instance.Failure();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag=="sword" || other.tag == "PlayerBullet")
        {
            OrbSpawner.Instance.OrbDestroyed();
            Destroy(gameObject);
        }
    }
}
