using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//remove all movement stuff when adding the improved boss pathing. Pathing is baked through window- AI.
//AI refactoring is priority over hitbox for now.
public class BossManager : MonoBehaviour
{
    //player's coordinates
    public Transform Player;

    //Some Distance before player is considered to be at 'range'
    public int MaxDist = 10;

    //Some Distance before player is considered to be in 'melee' (Don't keep running like a doofus)
    public int MinDist = 5;

    //Do a random number and give to MoveSelector- this chooses what 'attack' is chosen.
    public int MoveSelector;

    //is Rage?
    public bool Rage;
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
    Animator animatr;
    NavMeshAgent agent;

    private void Awake(){
        //defining other scripts referenceds them here- this method avoids an error.
        bPathing = GetComponent<BossPathing>();
        shockwave = GetComponent<tempShockwaveCaller>();
        punch = GetComponent<MegaPunch>();
        slam = GetComponent<GroundSlam>();
        erupt = GetComponent<Eruption>();
        rockThrow = GetComponent<RockPathFinding>();
        animatr = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update(){
        /*GroundedCheck();
        //Looks at the player
        transform.LookAt(Player);

        if(!Grounded)
        {
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
        }*/
        animatr.SetFloat("Speed", agent.velocity.magnitude);
        //if not in melee
        if (Vector3.Distance(transform.position, Player.position) >= MinDist){
            //if(!inAttack){
            bPathing.bossPathing();
            //}
            //bPathing.GetComponent<BossPathing>().bossPathing();
            //transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);
            bool isMidRange = Vector3.Distance(transform.position, Player.position) >= MinDist;
            bool isLongRanged = Vector3.Distance(transform.position,Player.position) >= MaxDist;
            if(isMidRange && !inAttack && !isLongRanged){
                //Debug.Log("Mid Range!");   
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
            //Debug.Log("In melee!");
            StartCoroutine(meleeActions());    
           // }
        }

        //If player is 'far' do 'ranged' 
        //frustration mechanic Option
        bool isRanged = Vector3.Distance(transform.position,Player.position) >= MaxDist;
        if(isRanged && !inAttack && rangedAllowed){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);

            Debug.Log("Long Range!");
            StartCoroutine(rangedActions());

        } 
    }

    IEnumerator meleeActions(){
        //attack animatr starts
        inAttack = true;
        SelectMove(1, 4);
        //Shockwave
        if(MoveSelector == 1){
            //Debug.Log("Do Shockwave!");
            //spawn the Shockwave Attack
            shockwave.instantiateShockwave();
            //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
            float animatrDuration = 2;//ShockwaveScript.scaleTime;
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);
        }

        //Mega Punch
        if(MoveSelector == 2){
            Debug.Log("Do MegaPunch!");
            animatr.SetTrigger("Sweep");
            punch.megaPunch();
            float animatrDuration = 2; // Figure this out
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);
            
        }

        //Ground Slam
        if(MoveSelector == 3){
            Debug.Log("Do Ground Slam!");
            animatr.SetTrigger("Slam");
            slam.groundSlam();
            animatr.SetTrigger("Slam");
            float animatrDuration = 2; // Figure this out
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);
        }
        //Coroutine finishes and boss is now able to select next action.
        inAttack = false;
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
            animatr.SetTrigger("Eruption");
            float animatrDuration = 2; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
        }
        
        //Rock Throw
        RockManager.Instance.ClearUpList();
        if(MoveSelector == 6 && RockManager.Instance.IsThereStillRocks()){
            attackException = true;
            Debug.Log("Do Rock Throw!");
            rockThrow.SetTarget();
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            while(rockThrow.currentlyTargeting){
                //bPathing.bossPathing();
                yield return new WaitForSeconds(Time.deltaTime);
            }
            animatr.SetTrigger("Throw");

            
            attackException = false;
            animatr.SetTrigger("Throw");
            float animatrDuration = 2; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
        }
 
         //Coroutine finishes and boss is now able to select next action. (including moving)
        inAttack = false;
        //Prevents boss from spamming ranged attacks and locking itself into ranged animatrs - gives time to move forwards/advance
        yield return new WaitForSeconds(delayBeforeRangedAllowed);
        rangedAllowed = true;
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
    IEnumerator waitTime(float animatrDuration, float delay){
        yield return new WaitForSeconds(animatrDuration + delay);
    }*/
}
