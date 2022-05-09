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
<<<<<<< Updated upstream
=======
    //shockwaveattack
    //public GameObject shockwaveHitbox;
    
    //time between actions
    public float delayBeforeNextAttack;
    //to get variables from Shockwave
    //public Shockwave ShockwaveScript;
    //to prevent other actions starting while in one still
    public bool inAttack = false;
    //used to prevent range self-looping
    public bool rangedAllowed = true;
    //the timer for rangedAllowed
    public float delayBeforeRangedAllowed;
    public bool attackException = false;
    public bool rage = false;

    private BossPathing bPathing;
    private tempShockwaveCaller shockwave;
    private MegaPunch punch;
    private GroundSlam slam;
    private Eruption erupt;
    private RockPathFinding rockThrow;
    private new Animator animation;
    

    private void Awake(){
        //defining other scripts referenceds them here- this method avoids an error.
        bPathing = GetComponent<BossPathing>();
        shockwave = GetComponent<tempShockwaveCaller>();
        punch = GetComponent<MegaPunch>();
        slam = GetComponent<GroundSlam>();
        erupt = GetComponent<Eruption>();
        rockThrow = GetComponent<RockPathFinding>();
        animation = gameObject.GetComponent<Animator>();
    }
>>>>>>> Stashed changes
    
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
<<<<<<< Updated upstream
=======
            //if(!inAttack){
            bPathing.bossPathing();
            animation.SetTrigger("Walk");
            //}
            //bPathing.GetComponent<BossPathing>().bossPathing();
            //transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);
            bool isMidRange = Vector3.Distance(transform.position, Player.position) >= MinDist;
            bool isLongRanged = Vector3.Distance(transform.position,Player.position) >= MaxDist;
            if(isMidRange && !inAttack && !isLongRanged){
                Debug.Log("Mid Range!");   
            }                
        }
        
        
>>>>>>> Stashed changes
        
            transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);

                
        }
        // if in 'melee'
        if(Vector3.Distance(transform.position, Player.position) <= MinDist){
            //when doing a move pass SelectMove(Meleeattack1, Meleeattacklast); 
<<<<<<< Updated upstream

=======
            //SelectMove(1, 1); //selectmove 1, last
            //if(MoveSelector == 1){
                //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
                //StartCoroutine(waitTime(2.3f, delayBeforeNextAttack));
            Debug.Log("In melee!");
            animation.SetTrigger("Idle");
            StartCoroutine(meleeActions());    
           // }
>>>>>>> Stashed changes
        }

        //If player is 'far' do 'ranged' 
        if(Vector3.Distance(transform.position,Player.position) >= MaxDist){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);
        } 
    }

<<<<<<< Updated upstream
=======
    IEnumerator meleeActions(){
        //attack animation starts
        inAttack = true;
        SelectMove(1, 4);
        //Shockwave
        if(MoveSelector == 1){
            //Debug.Log("Do Shockwave!");
            //spawn the Shockwave Attack
            shockwave.instantiateShockwave();
            //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
            float animationDuration = 2;//ShockwaveScript.scaleTime;
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);
        }

        //Mega Punch
        if(MoveSelector == 2){
            Debug.Log("Do MegaPunch!");
            animation.SetTrigger("Sweep");
            punch.megaPunch();
            float animationDuration = 2; // Figure this out
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);
            
        }

        //Ground Slam
        if(MoveSelector == 3){
            Debug.Log("Do Ground Slam!");
            animation.SetTrigger("Slam");
            slam.groundSlam();
            float animationDuration = 2; // Figure this out
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);
        }
        //Coroutine finishes and boss is now able to select next action.
        inAttack = false;
        animation.SetTrigger("Idle");
    }

    IEnumerator midActions(){
        inAttack = true;
        SelectMove(3, 3);
        yield return new WaitForSeconds(1);
    }

    IEnumerator rangedActions(){
        inAttack = true;
        rangedAllowed = false;
        SelectMove(5,7);
        //Ground Slam
        if(MoveSelector == 5){
            Debug.Log("Do Eruption!");
            erupt.eruption();
            animation.SetTrigger("Eruption");
            float animationDuration = 2; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
        }
        
        //Rock Throw
        if(MoveSelector == 6){
            attackException = true;
            Debug.Log("Do Rock Throw!");
            rockThrow.SetTarget();
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            while(rockThrow.currentlyTargeting){
                //bPathing.bossPathing();
                yield return new WaitForSeconds(Time.deltaTime);
            }
            animation.SetTrigger("Throw");

            attackException = false;

            float animationDuration = 2; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            yield return new WaitForSeconds(animationDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
        }

         
         //Coroutine finishes and boss is now able to select next action. (including moving)
        inAttack = false;
        animation.SetTrigger("Idle");
        //Prevents boss from spamming ranged attacks and locking itself into ranged animations - gives time to move forwards/advance
        yield return new WaitForSeconds(delayBeforeRangedAllowed);
        rangedAllowed = true;
    }


>>>>>>> Stashed changes
    private void SelectMove(int min, int max){
        //if (Rage == true){ 
            //min+= Whatever the number of moves is;
            //max+= Whatever the number of moves is;
        //}
        
        MoveSelector = Random.Range(min, max);
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
}
