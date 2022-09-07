using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dodge : MonoBehaviour
{
    private CharacterController controller;
    private float dodgeTimer;
    private Animator _animator;
    public float invulnTime = .25f;
    [HideInInspector]
    public float cdTimer = 0;
    public float dodgeMultiplier;

    
    public bool invuln = false;

    [HideInInspector]
    public bool isDodging;

    [SerializeField] float cooldown;

    [SerializeField] AnimationCurve dodgeCurve;

    void Start()
    {
        	Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length -1];
			dodgeTimer = dodge_lastFrame.time;
            controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
           
    }

    void Update()
    {   
        CooldownHandler();
    }

    void CooldownHandler()
    {
        cdTimer -= Time.deltaTime;

        if(cdTimer <=0)
        {
            cdTimer = 0;
        }
    }

    public void callDodge()
    {
        if(cdTimer <= 0 && !isDodging)
        {
            StartCoroutine(DoDodge());
        }
    }

    IEnumerator DoDodge()
		{
            AkSoundEngine.PostEvent("Player_Dodge", gameObject);
            isDodging = true;
            invuln = true;
            _animator.SetTrigger("Dodge");
			float timer = 0;
            //cdTimer = 0.5f;//was 1000
			while(timer < dodgeTimer)
			{
                if(timer >= (dodgeTimer/5)){
                    invuln = false;
                }
				//Debug.Log("dodging");
				float dSpeed = dodgeCurve.Evaluate(timer) * dodgeMultiplier;
				Vector3 dir = (transform.forward * dSpeed); 
				controller.Move(dir * Time.deltaTime);
				timer += Time.deltaTime;
				yield return null;
			}
            cdTimer = cooldown;
			isDodging = false;
            //invuln = false;
		}
}
