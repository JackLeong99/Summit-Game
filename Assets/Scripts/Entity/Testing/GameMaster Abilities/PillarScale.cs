using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScale : MonoBehaviour
{
    public float maxScale;
    public float defaultScale = 1f;
    public int oneOutOfThis;
    public float scaleSpeed;
    private bool isScalingDown;
    private bool isScalingUp;
    public Vector3 pillar;
    public float currentScale;

    public GameObject pillarTop;

    void Start()
    {
        currentScale = defaultScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 pillarScale = transform.localScale;


        if(!isScalingDown && !isScalingUp)
        {
            if(currentScale <= defaultScale)
            {
                if(DoIScale() == oneOutOfThis-1)
                {
                    Debug.Log("Scale Up");
                    currentScale += (scaleSpeed * Time.deltaTime);
                    transform.localScale = new Vector3(1, currentScale, 1);
                    pillarTop.transform.localPosition = new Vector3(0, currentScale, 0);
                    isScalingUp = true;
                }
            }

            if(currentScale >= maxScale)
            {
                if(DoIScale() == oneOutOfThis-1)
                {
                    currentScale -= (scaleSpeed * Time.deltaTime);
                    transform.localScale = new Vector3(1, currentScale, 1);
                    pillarTop.transform.localPosition = new Vector3(0, currentScale, 0);
                    isScalingDown = true;
                }
            }
        }
        else if(isScalingDown)
        {
            if(currentScale >= defaultScale)
            {
                currentScale -= (scaleSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, currentScale, 1);
                pillarTop.transform.localPosition = new Vector3(0, currentScale, 0);
            }
            else
            {
                isScalingDown = false;
            }
        }
        else if(isScalingUp)
        {
            if(currentScale <= maxScale)
            {
                currentScale += (scaleSpeed * Time.deltaTime);
                transform.localScale = new Vector3(1, currentScale, 1);
                pillarTop.transform.localPosition = new Vector3(0, currentScale, 0);

                Debug.Log("Scaling Up");
            }
            else
            {
                isScalingUp = false;
            }
        }
    }

    private int DoIScale()
    {
        return Random.Range(0, oneOutOfThis);
    }
}
