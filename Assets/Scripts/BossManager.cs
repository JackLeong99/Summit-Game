using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
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
    //private bool rockPatienceCheck = false;
    public bool attackException = false;
    //private bool rockThrowException = false;
    //rage mode
    public bool rage = false;
    public float rageSpeed;
    public float rageAttackMultiplier;
    private float startSpeed;
    //the last action taken by the boss- used to prevent long repetition
    public string lastMove;
    //how many times the last move has been used in a row
    public int lastMoveRepeated = 0;
    //Dodgy slider method to match the timing with slam- determines when the shockwave is done
    public float scuffedShockTimer;
    private Coroutine mActions;
    private Coroutine rActions; 
    //Health for the boss
    [SerializeField] float maxHP;
    //[HideInInspector]
    //the current HP of the boss
    public float currentHP;
    public int spawnRocksNumber;
    public float spawnNewRocksTime;
    public bool stunned = false;
    public float gunStunDuration;
    private float stunTimer;
    private BossPathing bPathing;
    private Shockwave shockwave;
    private MegaPunch punch;
    private GroundSlam slam;
    private Eruption erupt;
    private RockPathFinding rockThrow;
    //private RockManager rocks;
    private DamagePlayer2 damagePlayer;
    private DamageFlash damageFlash;
    Animator animatr;
    NavMeshAgent agent;
    private bool Alive = true;
    Rigidbody[] rigidBodies;
    [SerializeField] float attackTurnSpeed;
    [SerializeField] float turnFor = 1.0f;
    private bool canTurn = false;
    //used by IncreasePlayerAttack power-up
    private bool increaseDamage=false;
    //private float step;

    private void Awake(){
        //defining other scripts referenceds them here- this method avoids an error.
        bPathing = GetComponent<BossPathing>();
        shockwave = GetComponent<Shockwave>();
        punch = GetComponent<MegaPunch>();
        slam = GetComponent<GroundSlam>();
        erupt = GetComponent<Eruption>();
        //rocks = GetComponent<RockManager>();
        rockThrow = GetComponent<RockPathFinding>();
        damagePlayer = GetComponent<DamagePlayer2>();
        animatr = gameObject.GetComponent<Animator>();
        damageFlash = GetComponent<DamageFlash>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(fightStart());
    }

    void Start()
    {
        currentHP = maxHP;
        //rigidBodies = GetComponentsInChildren<Rigidbody>();
        //setUpHitBoxes();
        startSpeed = agent.speed;
    }
    
    void Update(){
        animatr.SetFloat("Speed", agent.velocity.magnitude);
        smoothLookAt(Player);
        //step = attackTurnSpeed * Time.deltaTime;
        if (Alive && !stunned){
            if(!inAttack && RockManager.Instance.countUnderWantedRocks){
                StartCoroutine(summonRocks());
            }
            else{
                /*GroundedCheck();
                //Looks at the player
                transform.LookAt(Player);

                if(!Grounded)
                {
                transform.Translate(Vector3.down * gravity * Time.deltaTime);
                }*/
                //animatr.SetFloat("Speed", agent.velocity.magnitude);
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
                if (isMidRange && !isRanged){
                    bPathing.bossPathing();
                    //when doing a move pass SelectMove(Midattack1, Midattacklast);
                    if(isMidRange && !inAttack && !isRanged && rangedAllowed){
                        //Debug.Log("Mid Range!");
                        if(currentPatience >= patience){
                            Coroutine rActions = StartCoroutine(rangedActions());
                            currentPatience = 0;
                        }
                        //If moving because of this don't increase patience from here.
                        else if(!inRockThrow){
                            currentPatience = currentPatience + (fullRandomiser(0.1f, 0.2f) * Time.deltaTime);
                        }
                    }   
                }

                //if melee range and not in attack delay
                else if(isMelee && !inAttack){
                    Coroutine mActions = StartCoroutine(meleeActions());
                }

                //if long range and not in attack delay
                else if(isRanged && !inAttack){
                    //Debug.Log("Long Range!");
                    bPathing.bossPathing();
                    //ranged delay only matters for attack actions. Should not affect movement.
                    if(rangedAllowed){
                        Coroutine rActions = StartCoroutine(rangedActions());
                    }
                }   
            }
        }
    }

    //Do any opening animations or behaviours here.
    IEnumerator fightStart(){
        //potentially should be changed to stunned after testing that this doesn't break anything.
        inAttack = true;
        yield return new WaitForSeconds(3);
        inAttack = false;
    }

    //All 'melee' actions/attacks
    IEnumerator meleeActions(){
        //attack animatr starts
        //The boss is in attack and now won't trigger other attacks until this finishes
        inAttack = true;
        //determine which attack to use
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
            //Check if repeated too many times
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
            //Debug.Log("Do MegaPunch!");
            //Face player to aim- smoother method could be used
            //transform.LookAt(Player);
            canTurn = true;
            yield return new WaitForSeconds(turnFor);
            canTurn = false;
            //Do the animation
            animatr.SetTrigger("Sweep");
            //Do the sweep attack
            punch.megaPunch();
            //Hard-coded animation duration.
            float animatrDuration = 2.875f; // Figure this out
            //Sweep has been done 1 more time in a row
            lastMove = "Sweep";
            lastMoveRepeated ++;
            yield return new WaitForSeconds(animatrDuration);
            //Don't do delayBeforeNextAttack if triggering as part of rock throw pathing
            if(!inRockThrow){
                yield return new WaitForSeconds(delayBeforeNextAttack);
            }
            
        }

        //Ground Slam
        if(MoveSelector == 2){
            //Check if repeated too many times
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
            //Debug.Log("Do Ground Slam!");
            canTurn = true;
            yield return new WaitForSeconds(turnFor);
            canTurn = false;
            //Do the animation
            animatr.SetTrigger("Slam");
            //Do the slam attack
            slam.groundSlam();
            //Face player to aim- smoother method could be used
            //transform.LookAt(Player);
            //Wait for the set delay before running shockwave, should be timed to match fists hitting ground
            //yield return new WaitForSeconds(scuffedShockTimer);
            //Do the shockwave attack
            //shockwave.instantiateShockwave();
            //Hard-coded animation duration.
            float animatrDuration = 2.875f; // Figure this out
            //Wait has already been done before timer which is a part of the animation duration
            yield return new WaitForSeconds(animatrDuration);
           //possible to double slam
           //would be nice to have different animation - particle effect on the fists before first slam
           //less drawback on second slam
            if(SelectMove(1, 4) == 3){
                //animatr.SetFloat("Speed", agent.velocity.magnitude);
                //Face player to aim- smoother method could be used
                //transform.LookAt(Player);
                canTurn = true;
                yield return new WaitForSeconds(turnFor);
                canTurn = false;
                //Debug.Log("Double slam!");
                //Do the second slam attack
                slam.groundSlam();
                //Do the animation
                animatr.SetTrigger("Slam");
                //Wait for the set delay before running shockwave, should be timed to match fists hitting ground
                //yield return new WaitForSeconds(scuffedShockTimer);
                //Do the shockwave attack
                //shockwave.instantiateShockwave();
                //Wait has already been done before timer which is a part of the animation duration
                yield return new WaitForSeconds(animatrDuration);
                // yield return new WaitForSeconds(2f * Time.deltaTime);
                // shockwave.instantiateShockwave();
                // yield return new WaitForSeconds(animatrDuration - 2 * Time.deltaTime);
            }
            //Slam has been done 1 more time in a row
            lastMove = "Slam";
            lastMoveRepeated ++;
            //Don't do delayBeforeNextAttack if triggering as part of rock throw pathing
            if(!inRockThrow){
                yield return new WaitForSeconds(delayBeforeNextAttack);
            }
        }
        //Coroutine finishes and boss is now able to select next action.
        //if the boss did meleeActions() as a part of rockThrow then they are still in that attack.
        if(!inRockThrow){
            inAttack = false;
        }
    }

    // IEnumerator midActions(){
    //     inAttack = true;
    //     SelectMove(3, 3);
    //     yield return new WaitForSeconds(1);
    // }

    //All 'ranged' actions/attacks
    IEnumerator rangedActions(){
        //The boss is in attack and now won't trigger other attacks until this finishes
        inAttack = true;
        //The boss does not have permission to trigger a new ranged attack until true
        rangedAllowed = false;
        //determine which attack to use
        //If the boss is in rock throw it should not do rock throw again
        if(!inRockThrow){
            MoveSelector = SelectMove(5,7);
        }
        //prep rock throw
        //RockManager.Instance.ClearUpList();
        //Eruption
        //if(!rockPatienceCheck && (inRockThrow || MoveSelector == 5)){
        if(inRockThrow || MoveSelector == 5){
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
            animatr.SetTrigger("Eruption");
            canTurn = true;
            yield return new WaitForSeconds(turnFor);
            canTurn = false;
            erupt.eruption();
            //transform.LookAt(Player);
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
        // if((rockPatienceCheck || MoveSelector == 6) && RockManager.Instance.IsThereStillRocks()){
        if((MoveSelector == 6) && RockManager.Instance.IsThereStillRocks()){
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
                // if(stunned){
                //     yield break;
                // }
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
                    currentPatience = 0;
                    //attackException = true;
                    //inAttack = false;
                }
                else{
                    //Debug.Log("Increase currentPatience");
                    attackException = true;
                    currentPatience = currentPatience + (fullRandomiser(0.1f, 0.2f)) * Time.deltaTime;
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            animatr.SetTrigger("Throw");
            transform.LookAt(Player);
            //If this is before the wait the boss turns around after the throw animation and walks away for a bit after it.
            //If this is after the wait the boss turns around during the throw animation.
            attackException = false;
            float animatrDuration = 2.875f; // Figure this out
           // float delayBeforeCurrentAttack = 1.5f; // Figure this out
            //while loop for wait for seconds- get isTargeting from RockPathFinding.
            yield return new WaitForSeconds(animatrDuration + delayBeforeNextAttack);// + delayBeforeCurrentAttack
            
            inRockThrow = false;
            lastMove = "Rock Throw";
            lastMoveRepeated ++;
            //rockPatienceCheck = false;
        }
        
        //for all ranged:
        
        //Coroutine finishes and boss is now able to select next action. (including moving)
        inAttack = false;
        //Prevents boss from spamming ranged attacks and locking itself into ranged animatrs - gives time to move forwards/advance
        yield return new WaitForSeconds(delayBeforeRangedAllowed);
        rangedAllowed = true;
    }

    private int SelectMove(int min, int max){
        // if (rage == true){ 
        //     min+= 4;
        //     max+= 4;
        // }

        //Shockwave ==1
        //Punch ==2
        //Throw ==5
        return Random.Range(min, max);
    }

    IEnumerator summonRocks(){
        animatr.SetTrigger("Summoning");
        stunTimer = spawnNewRocksTime;
        StartCoroutine(summoningRocks());
        yield return new WaitForSeconds(spawnNewRocksTime);
        RockManager.Instance.countUnderWantedRocks = false;
        RockManager.Instance.SpawnNewRocks();
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

    public void TakeDamage(float dmg, Vector3 position)
    {
        if(increaseDamage)
        {
            dmg=dmg/2;
        }
        currentHP -= dmg;
        if (currentHP > maxHP){
            currentHP = maxHP;
        }

        UIManager.Instance.HealthBossBarSet((int)Mathf.Round(currentHP));
        UIManager.Instance.DamageTextPool.Spawn(position, dmg.ToString(), Color.white, dmg > 15f ? 12f : 4f);

        if (damageFlash != null)
            damageFlash.Flash();

        if (rage == false && (currentHP <= (maxHP/2))){
            StartCoroutine(triggerRage());
        }
        if (currentHP <= 0.0f)
        {
            StartCoroutine(Death());
        }
    }

    //used by Power-up IncreasePlayerAttack
    public void DamageIncrease()
    {
        if(increaseDamage==true)
        {
            increaseDamage=false;
        }
        else
        {
           increaseDamage=true; 
        }
    }

    public float getCurrentBossHealth()
    {
        return currentHP;
    }

    IEnumerator Death()
    {
        animatr.SetTrigger("Death");
        GameManager.Instance.onBossDeath();
        yield return new WaitForSeconds(2.2f);
        Alive = false;
        agent.speed = 0;
        yield return new WaitForSeconds(8f);
        UIManager.Instance.WinScreen();
        //Instantiate(deathFX, gameObject.transform.position, Quaternion.identity);
    }

    IEnumerator triggerRage(){
        rage = true;
        agent.speed = rageSpeed;
        yield break;
        //stun boss for animation triggers

        // //this method could cause issues- coroutine runs simultaneously so if this triggers mid-attack it's possible for the attack script to then
        // //call inAttack = false and cause havoc
        // inAttack = true;
        // attackException = true;
        
        // //potentially rage animation here.
        // //switch over animationshere
        // yield return new WaitForSeconds(3);
        // //be unleashed
        // //damagePlayer.rageAttackModifier(); //Now handled inside damagePlayer2
        // attackException = false;
        // inAttack = false; // probs borked
    }

    public void gunStun(){
        stunTimer = gunStunDuration;
        StartCoroutine(bossStunned());
    }

    IEnumerator bossStunned(){
        stunned = true;
        agent.speed = 0;

        animatr.SetTrigger("Stunned");

        if(mActions != null){
            StopCoroutine(mActions);
        }
        if(rActions != null){
            StopCoroutine(rActions);
        }
        yield return new WaitForSeconds(stunTimer);
        stunned = false;
        animatr.SetTrigger("StunEnd");
        if(rage){
            agent.speed = rageSpeed;
        }
        else{
            agent.speed = startSpeed;
        }

    }

    //this is a very bandiad solution to needing the boss to have the properties of being stunned but not calling the stun with animation trigger while summoning rocks
    IEnumerator summoningRocks()
    {
        stunned = true;
        agent.speed = 0;

        if (mActions != null)
        {
            StopCoroutine(mActions);
        }
        if (rActions != null)
        {
            StopCoroutine(rActions);
        }
        yield return new WaitForSeconds(stunTimer);
        stunned = false;
        if (rage)
        {
            agent.speed = rageSpeed;
        }
        else
        {
            agent.speed = startSpeed;
        }

    }

    //public void setUpHitBoxes() 
    //{
    //    foreach(var Rigidbody in rigidBodies)
    //    {
    //        Rigidbody.isKinematic = true;
    //        Rigidbody.gameObject.AddComponent<EnemyDamageReceiver>();
    //    }
    //}

    public int rockNumberMinimum(){
        return spawnRocksNumber;
    }

    public void smoothLookAt(Transform target) 
    {
        if (canTurn)
        {
            var targetRot = Quaternion.LookRotation(target.position - transform.position);
            var adjustedRot = Quaternion.Euler(0.0f, targetRot.eulerAngles.y, targetRot.eulerAngles.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, adjustedRot, attackTurnSpeed * Time.deltaTime);
        }
    }
}
