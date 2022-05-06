using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] float gravity;
    //store the player object
    [SerializeField] LayerMask GroundLayers;

    [SerializeField] float GroundedOffset;
    public float GroundedRadius; 

    private bool Grounded;
    public Transform Player;

    //speed of enemy
    public int MoveSpeed = 4;

    //Some Distance before player is considered to be at 'range'
    public int MaxDist = 10;

    //Some Distance before player is considered to be in 'melee' (Don't keep running like a doofus)
    public int MinDist = 5;

    //Do a random number and give to MoveSelector- this chooses what 'attack' is chosen.
    public int MoveSelector;

    public bool Rage;
    
    void Update(){
        GroundedCheck();
        //Looks at the player
        transform.LookAt(Player);

        if(!Grounded)
        {
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
        }
        

        //if not in melee
        if(Vector3.Distance(transform.position, Player.position) >= MinDist){
        
            transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);

                
        }
        // if in 'melee'
        if(Vector3.Distance(transform.position, Player.position) <= MinDist){
            //when doing a move pass SelectMove(Meleeattack1, Meleeattacklast); 
            SelectMove(1, 1); //selectmove 1

        }

        //If player is 'far' do 'ranged' 
        if(Vector3.Distance(transform.position,Player.position) >= MaxDist){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);
            SelectMove(5, 5);
        } 
    }

    private void SelectMove(int min, int max){
        //if (Rage == true){ 
            //min+= Whatever the number of moves is;
            //max+= Whatever the number of moves is;
        //}

        //Shockwave ==1
        //Punch ==2
        //Throw ==5
        MoveSelector = Random.Range(min, max);
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
}
