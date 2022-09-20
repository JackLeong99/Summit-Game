using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    [Header("Values")]
    public float delay;
    public float duration;

    [Header("References")]
    public GameObject warning;
    public GameObject attack;

    public void Awake()
    {
        warning.SetActive(true);
        attack.SetActive(false);
    }

    public void Start()
    {
        StartCoroutine(Lifetime());
    }

    public IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(delay);
        attack.SetActive(true);
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
