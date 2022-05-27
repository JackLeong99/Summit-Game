using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [HideInInspector]
    public bool isAttacking;

    private Animator _animator;

    private float swingTimer;

    [SerializeField] float chainTimer;

    private int attackAnim;

    [SerializeField] float attackDamage;

    [SerializeField] GameObject attackHitbox;

    [SerializeField] AnimationCurve attackCurve;
    // Start is called before the first frame update
    void Start()
    {
        attackAnim = 0;
        _animator = GetComponent<Animator>();
        Keyframe swing_lastFrame = attackCurve[attackCurve.length -1];
		swingTimer = swing_lastFrame.time;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("chainTimer: " + chainTimer);
        Debug.Log("AttackAnim: " + attackAnim);
        if (chainTimer > 0)
        {
            attackAnim = 1;
            chainTimer -= Time.deltaTime;
        }
        else 
        {
            attackAnim = 0;
        }
    }

    public void doAttack()
    {
        if(!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
	{
		Transform player = gameObject.transform;
        if (attackAnim == 0) 
        {
            _animator.SetTrigger("attack0");
            chainTimer = 1.5f;
        }
        if (attackAnim == 1)
        {
            _animator.SetTrigger("attack1");
            chainTimer = 0;
        }
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
		var hitbox = Instantiate(attackHitbox, player.position + new Vector3(0, 1.3f, 0), player.rotation, player.transform);
		hitbox.transform.localPosition += new Vector3(0, 0, 1);
        hitbox.GetComponent<PlayerDamage>().setDamage(attackDamage);
        yield return new WaitForSeconds(swingTimer - 0.2f);
        //float timer = 0;
		//while(timer < swingTimer)
		//{
		//	//Debug.Log("Attacking!!!");
		//	timer += Time.deltaTime;
		//	yield return null;
		//}
		if(hitbox)
		{
			Destroy(hitbox);
		}
		isAttacking = false;
    }
}
