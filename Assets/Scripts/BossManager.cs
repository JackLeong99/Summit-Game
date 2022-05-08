using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//remove all movement stuff when adding the improved boss pathing. Pathing is baked through window- AI.
//AI refactoring is priority over hitbox for now.
public class BossManager : MonoBehaviour
{
    [SerializeField] float gravity;
    //store the player object
    [SerializeField] LayerMask GroundLayers;

    [SerializeField] float GroundedOffset;
    public float GroundedRadius; 
    //is on ground?
    private bool Grounded;
    //player's coordinates
    public Transform Player;

    //speed of enemy
    public int MoveSpeed = 4;

    //Some Distance before player is considered to be at 'range'
    public int MaxDist = 10;

    //Some Distance before player is considered to be in 'melee' (Don't keep running like a doofus)
    public int MinDist = 5;

    //Do a random number and give to MoveSelector- this chooses what 'attack' is chosen.
    public int MoveSelector;

    //is Rage?
    public bool Rage;
    //shockwaveattack
    public GameObject shockwaveHitbox;
    
    //time between actions
    public float delayBeforeNextAttack;
    //to get variables from Shockwave
    //public Shockwave ShockwaveScript;
    //to prevent other actions starting while in one still
    private bool inAttack;

    public BossPathing bPathing;// = new BossPathing();
    

    private void Awake(){

    }

    
    void Update(){
        /*GroundedCheck();
        //Looks at the player
        transform.LookAt(Player);

        if(!Grounded)
        {
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
        }*/
        

        //if not in melee
        if(Vector3.Distance(transform.position, Player.position) >= MinDist){
            //bPathing.bossPathing();
            bPathing.GetComponent<BossPathing>().bossPathing();
            //transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);
            bool isMidRange = Vector3.Distance(transform.position, Player.position) >= MinDist;
            bool isLongRanged = Vector3.Distance(transform.position,Player.position) >= MaxDist;
            if(isMidRange && !inAttack && !isLongRanged){
                Debug.Log("Mid Range!");
                
            }

                
        }
        // if in 'melee'
        bool isMelee = Vector3.Distance(transform.position, Player.position) <= MinDist;
        if(isMelee && !inAttack){
            //when doing a move pass SelectMove(Meleeattack1, Meleeattacklast); 
            //SelectMove(1, 1); //selectmove 1, last
            //if(MoveSelector == 1){
                //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
                //StartCoroutine(waitTime(2.3f, delayBeforeNextAttack));
            Debug.Log("In melee!");
            StartCoroutine(meleeActions());
                
           // }

        }

        //If player is 'far' do 'ranged' 
        bool isRanged = Vector3.Distance(transform.position,Player.position) >= MaxDist;
        if(isRanged && !inAttack){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);
            Debug.Log("Long Range!");
            SelectMove(5, 5);
        } 
    }

    IEnumerator meleeActions(){
        //attack animation starts
        inAttack = true;
        SelectMove(1, 1);
        //Shockwave
        if(MoveSelector == 1){
            Debug.Log("Do Shockwave!");
            //spawn the Shockwave Attack
            Instantiate(shockwaveHitbox, transform.position, transform.rotation);
            float animationDuration = 2;//ShockwaveScript.scaleTime;
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);
            
        }

        //Coroutine finishes and boss is now able to select next action.
        inAttack = false;
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

    /*private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }*/
/*
    IEnumerator waitTime(float animationDuration, float delay){
        yield return new WaitForSeconds(animationDuration + delay);
    }*/
}
