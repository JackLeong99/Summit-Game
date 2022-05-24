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
    
    [HideInInspector]
    public float cdTimer = 0;
    public float dodgeMultiplier;

    [HideInInspector]
    public bool isDodging;

    [SerializeField] float cooldown;

    [SerializeField] AnimationCurve dodgeCurve;

    //will move to ui later
    public Color OffCD;
    public Color OnCD;
    public Image CdBackground;
    public TextMeshProUGUI CdDisplay;

    void Start()
    {
        	Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length -1];
			dodgeTimer = dodge_lastFrame.time;
            controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            
            //move to ui later
            CdDisplay.text = "";
            CdBackground.color = OffCD;
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
            CdDisplay.text = "";
            CdBackground.color = OffCD;
        }
        else
        {
            CdDisplay.text = cdTimer.ToString("0");
            CdBackground.color = OnCD;
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
            _animator.SetTrigger("Dodge");
			float timer = 0;
            cdTimer = 0.5f;//was 1000
			while(timer < dodgeTimer)
			{
				//Debug.Log("dodging");
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
