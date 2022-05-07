using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] float maxSize;
    [SerializeField] float scaleTime;

    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            scaleHitBox();
        }
    }

    public void scaleHitBox()
    {
        StartCoroutine(scale());
    }

    IEnumerator scale()
    {
        float timer = 0;
        Vector3 scale = gameObject.transform.localScale;

        Vector3 toScale = scale;
        toScale.x = toScale.x * maxSize;
        toScale.z = toScale.z * maxSize;

        while (timer < scaleTime)
        {
            timer += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(scale, toScale, timer / scaleTime);
            yield return null;
        }
    }
}
