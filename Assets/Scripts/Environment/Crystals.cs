using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    public List<GameObject> GO = new List<GameObject>();
    public Color defaultColor;
    public Color offColor;
    public float LockoutTime;
    public float lerpDuration;
    private bool Lockout;
    void Start()
    {
        if (GO.Count == 0) GO.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Lockout && other.CompareTag("PlayerBullet")) StartCoroutine(toggleOff());
    }

    public IEnumerator toggleOff()
    {
        foreach (GameObject go in GO)
            go.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", this.offColor);
        Lockout = true;
        yield return new WaitForSeconds(LockoutTime);
        foreach (GameObject go in GO)
            StartCoroutine(lerpColor(lerpDuration, go.GetComponent<MeshRenderer>().material));
        Lockout = false;
        yield return null;
    }
    public IEnumerator lerpColor(float d, Material m) 
    {
        float t = 0;
        while (t < d) 
        {
            m.SetColor("_EmissionColor", Color.Lerp(this.offColor, this.defaultColor, t));
            yield return t += Time.deltaTime;
        }
        yield return null;
    }
}
