using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResonantObject : MonoBehaviour
{
    public float damagePool;
    [Range(0.0f, 5.0f)]
    public float returnPercent;
    public float duration;
    public Color defaultColor;
    public Color procColor;
    public float flashDuration;
    public float playerHeightOffset;
    private LineRenderer tether;
    private GameObject player;
    private GameObject tetherPoint;
    private Material mat;
    private float t = 1;

    public void Start()
    {
        EventManager.instance.OnTakeDamage.AddListener(addToPool);
        tether = GetComponent<LineRenderer>();
        mat = tether.material;
        tetherPoint = transform.GetChild(0).gameObject;
        player = GameManager.instance.player;
        StartCoroutine(decay());
    }

    public void Update()
    {
        tether.SetPosition(0, tetherPoint.transform.position);
        tether.SetPosition(1, player.transform.position + new Vector3(0, playerHeightOffset, 0));
        if (t < flashDuration) 
        {
            mat.SetColor("_EmissionColor", Color.Lerp(procColor, defaultColor, t));
            t += Time.deltaTime;
        }
    }

    public void addToPool(float d) 
    {
        t = 0;
        damagePool += d;
    }

    public IEnumerator decay() 
    {
        yield return new WaitForSeconds(duration);
        GameManager.instance.player.GetComponent<PlayerHealth>().healDamage(damagePool * returnPercent);
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        EventManager.instance.OnTakeDamage.RemoveListener(addToPool);
    }

}
