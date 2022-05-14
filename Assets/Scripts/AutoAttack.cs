using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [HideInInspector]
    public bool isAttacking;

    private Animator _animator;

    private float swingTimer;

    [SerializeField] GameObject attackHitbox;

    [SerializeField] AnimationCurve attackCurve;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Keyframe swing_lastFrame = attackCurve[attackCurve.length -1];
		swingTimer = swing_lastFrame.time;
    }

    // Update is called once per frame
    void Update()
    {
        
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
		_animator.SetTrigger("attack1");
		isAttacking = true;
		var hitbox = Instantiate(attackHitbox, player.position + new Vector3(0, 1, 0), player.rotation, player.transform);
		hitbox.transform.localPosition += new Vector3(0, 0, 1);
		float timer = 0;
		while(timer < swingTimer)
		{
			Debug.Log("Attacking!!!");
			timer += Time.deltaTime;
			yield return null;
		}
		if(hitbox)
		{
			Destroy(hitbox);
		}
		isAttacking = false;
	}
}
