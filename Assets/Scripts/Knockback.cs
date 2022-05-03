using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float strength;
    [SerializeField] private float knockup;
    [SerializeField] private float duration;

    private void Start(){

    }

	private void OnTriggerEnter(Collider collision)
	{
		CharacterController player = collision.GetComponent<CharacterController>();
        Debug.Log(player);
        if(player != null)
        {
            StartCoroutine(Knock(collision, player));
        }
    }

    IEnumerator Knock(Collider collision, CharacterController player)
    {
        Vector3 dir = ((collision.transform.position - transform.position) * strength) + (Vector3.up * knockup);
        float timer = 0;
        while(timer < duration)
        {
            Debug.Log(timer);
            player.Move(dir * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
