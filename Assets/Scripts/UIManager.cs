using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No UIManager in the scene");
            }
            return instance;
        }
    }

    public Slider PlayerHealthBar;
    private float FullHealth=100f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthBar.maxValue= 100; //can be changed
        PlayerHealthBar.value=FullHealth;
    }

    // Update is called once per frame
    void Update()
    {
       // PlayerHealthBar.fillRect.x=50.0f;
    }


    public void HealthBarSet(float currentHealth)
    {
        PlayerHealthBar.value = currentHealth;
    }
}
