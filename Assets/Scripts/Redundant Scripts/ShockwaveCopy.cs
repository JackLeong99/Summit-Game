using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCopy : MonoBehaviour
{
    //[SerializeField] GameObject hitbox;
    public GameObject Player;
    public int damage = 5;
    public GameObject shockwave;
    public Vector3 sizeChange; // This has to be set for (x,y,z) in the editor
    //public float cubeSize = 0.2f;
    [SerializeField] float sizeMax; 
    [SerializeField] float scaleSpeed;

    /*void OnMouseDown()
    {
        shockwave.transform.localScale = shockwave.transform.localScale - sizeChange;    
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(shockwave.transform.localScale.x < sizeMax){
            shockwave.transform.localScale = (shockwave.transform.localScale + sizeChange * Time.deltaTime);
            
        }
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            PlayerStats health = Player.GetComponent<PlayerStats>();
            

            if(health != null){
                health.takeDamage(damage);
            }
        }
    }
}