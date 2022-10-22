using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;
    public static MainMenu Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No MainMenu in the scene");
            }
            return instance;
        }
    }
    private GameObject _mainCamera;
    //for controller interface
    public GameObject mainFirstButton, mainsettingsButton, creditsButton;

    public GameObject mainMenu;

    public GameObject mainMenuCanvas;

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
        //selectorMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        //set mew selected object
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
        mainMenuCanvas.GetComponent<Canvas>().worldCamera = _mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void Settings()
    {

    }

    public void Quit()
    {
        AkSoundEngine.PostEvent("UI_Click", _mainCamera);
        AkSoundEngine.PostEvent("Game_Quit", _mainCamera);
        Application.Quit();
    }
    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.instance.LoadDelegate(GameManager.instance.LoadGame());
    }
    
    public void SettingsCall()
    {
        mainMenu.SetActive(false);
        SettingsMenu.instance.isSettings(true, 1);
    }
    public void CreditsCall()
    {
        mainMenu.SetActive(false);
        Credits.instance.isCredits(true);
    }
    public void MainTrue()
    {
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainsettingsButton);
    }
    public void BackCredits()
    {
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(creditsButton);
    }
}
