using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject PanelGameOver;


    public TMP_Text CharacterClass;
    private string CharacterClassFormat= "Character Class: {0}";

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
        
        CharacterClass.text= string.Format(CharacterClassFormat, "Archer");
        PanelGameOver.SetActive(false);
        
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

    public void GameOverScreen() 
    {
        PanelGameOver.SetActive(true);
    }
}
