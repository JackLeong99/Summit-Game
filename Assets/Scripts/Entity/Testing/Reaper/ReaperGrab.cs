using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperGrab : MonoBehaviour
{
    public GameObject hand;
    public List<OnHitEffect> fx;
    public float duration;
    public float moveUp;
    public float d;

    private void Update()
    {
        if (Input.GetKeyDown("k")) 
        {
            StartCoroutine(grab(d));
        }
    }

    public IEnumerator grab(float d) 
    {
        GameObject h = Instantiate(hand, GameManager.instance.player.transform.position + new Vector3(0, -5, 0), Quaternion.identity) as GameObject;
        Vector3 u = h.transform.position; 
        Vector3 g = u + new Vector3(0, moveUp, 0);
        float t = 0;

        while (t < d) 
        {
            h.transform.position = Vector3.Lerp(u, g, (t / d));
            t += Time.deltaTime;
            yield return null;
        }
        h.transform.GetChild(1).gameObject.SetActive(true);
        h.transform.GetChild(1).GetComponent<HandStun>().setFX(fx);
        yield return new WaitForSeconds(duration);
        Destroy(h);
    }
}
