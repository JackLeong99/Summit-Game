using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    //store the player object
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
        //Looks at the player
        transform.LookAt(Player);
        

        //if not in melee
        if(Vector3.Distance(transform.position, Player.position) >= MinDist){
        
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //when doing a move pass SelectMove(Midattack1, Midattacklast);

                
        }
        // if in 'melee'
        if(Vector3.Distance(transform.position, Player.position) <= MinDist){
            //when doing a move pass SelectMove(Meleeattack1, Meleeattacklast); 

        }

        //If player is 'far' do 'ranged' 
        if(Vector3.Distance(transform.position,Player.position) >= MaxDist){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);
        } 
    }

    private void SelectMove(int min, int max){
        //if (Rage == true){ 
            //min+= Whatever the number of moves is;
            //max+= Whatever the number of moves is;
        //}
        
        MoveSelector = Random.Range(min, max);
    }
}
