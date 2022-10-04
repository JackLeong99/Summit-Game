using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageIn : MonoBehaviour
{
    public static UIDamageIn instance;
    public GameObject blood;
    public Image bloodSplat;

    public float timeVis;
    private float timer;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        blood.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            timer-=Time.deltaTime;
        }
        if(timer<=0)
        {
            blood.SetActive(false);
        }
    }
    public void DamageVis()
    {
        blood.SetActive(true);
        timer=timeVis;
        bloodSplat.CrossFadeAlpha(0f, timeVis, false);
    }
}
