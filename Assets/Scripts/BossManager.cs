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
    //Special bool for special rockthrow behaviours in update()
    private bool inRockThrow = false;
    //patience value
    public float patience;
    //temporarily public for testing
    public float currentPatience;
    private bool rockPatienceCheck = false;
    public bool attackException = false;
    private bool rockThrowException = false;
    public bool rage = false;
    public string lastMove;
    public int lastMoveRepeated = 0;

    private BossPathing bPathing;
    private Shockwave shockwave;
    private MegaPunch punch;
    private GroundSlam slam;
    private Eruption erupt;
    private RockPathFinding rockThrow;
    Animator animatr;
    NavMeshAgent agent;

    private void Awake(){
        //defining other scripts referenceds them here- this method avoids an error.
        bPathing = GetComponent<BossPathing>();
        shockwave = GetComponent<Shockwave>();
        punch = GetComponent<MegaPunch>();
        slam = GetComponent<GroundSlam>();
        erupt = GetComponent<Eruption>();
        rockThrow = GetComponent<RockPathFinding>();
        animatr = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(fightStart());
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
         // if in 'melee'
        bool isMelee = Vector3.Distance(transform.position, Player.position) <= MinDist;
        //if 'mid ranged'
        bool isMidRange = Vector3.Distance(transform.position, Player.position) >= MinDist;
        //If player is 'far' do 'ranged' 
        //frustration mechanic Option
        bool isRanged = Vector3.Distance(transform.position, Player.position) >= MaxDist;
        // if(inRockThrow){

        //     if(isMelee && currentPatience >= patience){
                
        //             rockThrowException = true;
        //             Debug.Log("Impatient");
        //             //rockThrow.SetPlayer();
        //             StartCoroutine(meleeActions());
        //             //rockThrow.SetTarget();
        //             rockPatienceCheck = true;
        //             StartCoroutine(rangedActions());
        //             currentPatience = 0;
        //             rockThrowException = false;
        //     }
        //     else if(currentPatience >= patience && (isMidRange || isRanged)){
                
        //             rockThrowException = true;
        //             Debug.Log("Impatient");
        //             //rockThrow.SetPlayer();
        //             rockPatienceCheck = false;
        //             StartCoroutine(rangedActions());
        //             //rockThrow.SetTarget();
        //             rockPatienceCheck = true;
        //             StartCoroutine(rangedActions());
        //             currentPatience = 0;
        //             rockThrowException = false;
                
        //     }
        //     else if (!inAttack && !rockThrowException){
        //         currentPatience = currentPatience + (fullRandomiser(0.05f, 0.1f)) * Time.deltaTime;
        //     }
        // }
        //if 'mid range'
        if (isMidRange){
            //if(!inAttack){
            bPathing.bossPathing();
            //}
            //bPathing.GetComponent<BossPathing>().bossPathing();
            //transform.Translate(transform.forward * MoveSpeed * Time.deltaTime);
            //when doing a move pass SelectMove(Midattack1, Midattacklast);
            if(isMidRange && !inAttack && !isRanged && rangedAllowed){
                //Debug.Log("Mid Range!");
                if(currentPatience >= patience){
                    StartCoroutine(rangedActions());
                    currentPatience = 0;
                }
                else if(!inRockThrow){
                    currentPatience = currentPatience + (fullRandomiser(0.1f, 0.2f) * Time.deltaTime);
                }

            }                
        }
       
        else if(isMelee && !inAttack){
            //when doing a move pass SelectMove(Meleeattack1, Meleeattacklast); 
            //SelectMove(1, 1); //selectmove 1, last
            //if(MoveSelector == 1){
                //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
                //StartCoroutine(waitTime(2.3f, delayBeforeNextAttack));
            //Debug.Log("In melee!");
            StartCoroutine(meleeActions());    
           // }
        }

        else if(isRanged && !inAttack && rangedAllowed){
            //when doing a move pass SelectMove(Rangedattack1, Rangedattacklast);

            Debug.Log("Long Range!");
            StartCoroutine(rangedActions());

        }
    }

    //Do any opening animations or behaviours here.
    IEnumerator fightStart(){
        inAttack = true;
        yield return new WaitForSeconds(3);
        inAttack = false;

    }

    IEnumerator meleeActions(){
        //attack animatr starts
        inAttack = true;
        MoveSelector = SelectMove(1, 3);
        //Shockwave
        // if(MoveSelector == 1){
        //     if(lastMove == "Shockwave"){
        //         if(lastMoveRepeated == 2){
        //             Debug.Log("Break!");
        //             inAttack = false;
        //             yield break;
        //         }
        //     }
        //     else{
        //         lastMoveRepeated = 0;
        //     }
        //     Debug.Log("Do Shockwave!");
        //     //spawn the Shockwave Attack
        //     shockwave.instantiateShockwave();
        //     //Instantiate(shockwaveHitbox, transform.position, transform.rotation);
        //     float animatrDuration = 2;//ShockwaveScript.scaleTime;
        //     lastMove = "Shockwave";
        //     lastMoveRepeated ++;
        //     yield return new WaitForSeconds(animatrDuration);
        //     if(!inRockThrow){
        //         yield return new WaitForSeconds(delayBeforeNextAttack);
        //     }
        // }

        //Sweep
        if(MoveSelector == 1){
            if(lastMove == "Sweep"){
                if(lastMoveRepeated == 2){
                    Debug.Log("Break!");
                    inAttack = false;
                    yield break;
                }
            }
            else{
                lastMoveRepeated = 0;
            }
            Debug.Log("Do MegaPunch!");
            transform.LookAt(Player);
            animatr.SetTrigger("Sweep");
            punch.megaPunch();
            float animatrDuration = 2.875f; // Figure this out
            lastMove = "Sweep";
            lastMoveRepeated ++;
            yield return new WaitForSeconds(animatrDuration);
            if(!inRockThrow){
                yield return new WaitForSeconds(delayBeforeNextAttack);
            }
            
        }

        //Ground Slam
        if(MoveSelector == 2){
            if(lastMove == "Slam"){
                if(lastMoveRepeated == 2){
                    Debug.Log("Break!");
                    inAttack = false;
                    yield break;
                }
            }
            else{
                lastMoveRepeated = 0;
            }
            Debug.Log("Do Ground Slam!");
            animatr.SetTrigger("Slam");
            slam.groundSlam();
            transform.LookAt(Player);
            animatr.SetTrigger("Slam");
            float animatrDuration = 2.875f; // Figure this out
            yield return new WaitForSeconds(animatrDuration);
           //possible to double slam
           //would be nice to have different animation - particle effect on the fists before first slam
           //less drawback on second slam
            if(SelectMove(1, 4) == 3){
                //animatr.SetFloat("Speed", agent.velocity.magnitude);
                transform.LookAt(Player);
                Debug.Log("Double slam!");
                slam.groundSlam();
                animatr.SetTrigger("Slam");
                yield return new WaitForSeconds(animatrDuration);
                // yield return new WaitForSeconds(2f * Time.deltaTime);
                // shockwave.instantiateShockwave();
                // yield return new WaitForSeconds(animatrDuration - 2 * Time.deltaTime);
            }
            lastMove = "Slam";
            lastMoveRepeated ++;
            if(!inRockThrow){
                yield return new WaitForSeconds(delayBeforeNextAttack);
            }
        }
        //Coroutine finishes and boss is now able to select next action.
        if(!inRockThrow){
            inAttack = false;
        }
    }

    // IEnumerator midActions(){
    //     inAttack = true;
    //     SelectMove(3, 3);
    //     yield return new WaitForSeconds(1);
    // }

    IEnumerator rangedActions(){
        inAttack = true;
        rangedAllowed = false;
        if(!inRockThrow){
            MoveSelector = SelectMove(5,7);
        }
        //prep rock throw
        //RockManager.Instance.ClearUpList();
        //Eruption
        if(!rockPatienceCheck && (inRockThrow || MoveSelector == 5)){
            if(!inRockThrow && lastMove == "Eruption" && RockManager.Instance.IsThereStillRocks()){
                if(lastMoveRepeated == 2){
                    Debug.Log("Break!");
                    inAttack = false;
                    rangedAllowed = true;
                    yield break;
                }
            }
            else{
                lastMoveRepeated = 0;
            }
            Debug.Log("Do Eruption!");
            erupt.eruption();
            transform.LookAt(Player);
            animatr.SetTrigger("Eruption");
            float animatrDuration = 3.292f; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            lastMove = "Eruption";
            lastMoveRepeated ++;
            yield return new WaitForSeconds(animatrDuration);// + delayBeforeCurrentAttack
            if(!inRockThrow){
                yield return new WaitForSeconds(delayBeforeNextAttack);
            }
        }
        
        //Rock Throw 
        if((rockPatienceCheck || MoveSelector == 6) && RockManager.Instance.IsThereStillRocks()){
            if(lastMove == "Rock Throw"){
                if(lastMoveRepeated == 2){
                    Debug.Log("Break!");
                    inAttack = false;
                    rangedAllowed = true;
                    yield break;
                }
            }
            else{
                lastMoveRepeated = 0;
            }
            attackException = true;
            // inAttack = false;
            // attackException = true;
            Debug.Log("Do Rock Throw!");
            //No longer needed for Rock Throw as we want new options to be possible.
            
            rockThrow.SetTarget();
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            
            //I probably shouldn't be using inAttack for this - it causes clashes 
            while(rockThrow.currentlyTargeting){
                bPathing.bossPathing();
                //inRockThrow = true;
                //inAttack = true;
                
                bool isMelee = Vector3.Distance(transform.position, Player.position) <= MinDist;
                if(isMelee && currentPatience >= patience){
                    attackException = false;
                    //inAttack = true;
                    Debug.Log("Impatient");
                    //rockThrow.SetPlayer();
                    StartCoroutine(meleeActions());
                    //rockThrow.SetTarget();
                    //rockPatienceCheck = true;
                    currentPatience = 0;
                    yield return new WaitForSeconds(2.875f);
                    //attackException = true;
                    //inAttack = false;
                }
                else{
                    Debug.Log("Increase currentPatience");
                    attackException = true;
                    currentPatience = currentPatience + (fullRandomiser(0.1f, 0.2f)) * Time.deltaTime;
                }
                
                yield return new WaitForSeconds(Time.deltaTime);
            }
            inRockThrow = false;
            transform.LookAt(Player);
            animatr.SetTrigger("Throw");
            attackException = false;
            float animatrDuration = 2.875f; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
            lastMove = "Rock Throw";
            lastMoveRepeated ++;
            rockPatienceCheck = false;
        }
        
        //for all ranged:
        
        //Coroutine finishes and boss is now able to select next action. (including moving)
        inAttack = false;
        //Prevents boss from spamming ranged attacks and locking itself into ranged animatrs - gives time to move forwards/advance
        yield return new WaitForSeconds(delayBeforeRangedAllowed);
        rangedAllowed = true;
    }


    private int SelectMove(int min, int max){
        //if (Rage == true){ 
            //min+= Whatever the number of moves is;
            //max+= Whatever the number of moves is;
        //}

        //Shockwave ==1
        //Punch ==2
        //Throw ==5
        return Random.Range(min, max);
    }

    private float fullRandomiser(float min, float max){
        return Random.Range(min, max);
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
