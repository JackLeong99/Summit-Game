using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public Slider BossHealthBar;
    public float BossFullHealth=100f;



    public GameObject PanelGameOver;
    public GameObject PanelWin;

    public GameObject PanelPauseMenu;
    public GameObject PowerUpMenu;

    private bool gameOver=true;

    public float BossHealth=1000;

    public DamageTextPool DamageTextPool;

    //enable certain buttons


    public TMP_Text CharacterClass;
    private string CharacterClassFormat= "Character Class: {0}";

    public TMP_Text BossName;
    private GameObject _mainCamera;

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
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PlayerHealthBar.maxValue= 100; //can be changed
        PlayerHealthBar.value=FullHealth;

        BossHealthBar.maxValue= BossHealth; //can be changed
        BossHealthBar.value=BossFullHealth;
        
        CharacterClass.text= string.Format(CharacterClassFormat, "Hero"); //to change archer set by a string
        PanelGameOver.SetActive(false);
        PanelPauseMenu.SetActive(false);
        PowerUpMenu.SetActive(false);
        PanelWin.SetActive(false);
        BossName.text="Iwazaru: Guardian of the mountain"; //set by game later same as archer
        
    }

    // Update is called once per frame
    void Update()
    {
     //   PlayerHealthBar.value= PlayerHealthBar.value-1; for testing if gamemanger and ui worked together
     //   HealthBarSet(PlayerHealthBar.value);

       
    }


    public void HealthBarSet(float currentHealth)
    {
        PlayerHealthBar.value = currentHealth;
        if(currentHealth<=0)
        {
            GameManager.Instance.onDeath(); //to be moved to whatever is handling health
        }
    }
    
    public void HealthBossBarSet(int currentHealth)
    {
        BossHealthBar.value = currentHealth;
        Debug.Log(BossHealthBar.value);
    }

    public void GameOverScreen() 
    {
        AkSoundEngine.PostEvent("Music_Defeat_Stinger", _mainCamera);
        gameOver =false;
        GameManager.Instance.DisallowPause();
        PanelGameOver.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void WinScreen() 
    {
        AkSoundEngine.PostEvent("Music_Victory_Stinger", _mainCamera);
        gameOver=false;
        GameManager.Instance.DisallowPause();
        PanelWin.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //make pause menu visable
    public void PauseMenu()
    {
        if(gameOver)
        {
            PanelPauseMenu.SetActive(true);
            AkSoundEngine.PostEvent("UI_Click", _mainCamera);
            AkSoundEngine.PostEvent("UI_Menu_On", _mainCamera);
            PowerUpMenu.SetActive(false);
        }
    }

    //take game out of pause
    public void ClickPlay()
    {
        PanelPauseMenu.SetActive(false);
        GameManager.Instance.ResumeGame();
        AkSoundEngine.PostEvent("UI_Click", _mainCamera);
        AkSoundEngine.PostEvent("UI_Menu_Off", _mainCamera);
    }

    public void PowerMenu()
    {
        PowerUpMenu.SetActive(true);
        PanelPauseMenu.SetActive(false);
        
    }

    public void Quit()
    {
        AkSoundEngine.PostEvent("UI_Click", _mainCamera);
        AkSoundEngine.PostEvent("Game_Quit", _mainCamera);
        Application.Quit();
    }
}
