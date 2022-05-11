using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private CharacterController controller;
    private float dodgeTimer;
    
    [HideInInspector]
    public float cdTimer = 0;
    public float dodgeMultiplier;

    [HideInInspector]
    public bool isDodging;

    [SerializeField] float cooldown;

    [SerializeField] AnimationCurve dodgeCurve;

    void Start()
    {
        	Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length -1];
			dodgeTimer = dodge_lastFrame.time;
            controller = GetComponent<CharacterController>();
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
			isDodging = true;
			float timer = 0;
            cdTimer = 1000f;
			while(timer < dodgeTimer)
			{
				Debug.Log("dodging");
				float dSpeed = dodgeCurve.Evaluate(timer) * dodgeMultiplier;
				Vector3 dir = (transform.forward * dSpeed); 
				controller.Move(dir * Time.deltaTime);
				timer += Time.deltaTime;
				yield return null;
			}
            cdTimer = cooldown;
			isDodging = false;
		}
}
