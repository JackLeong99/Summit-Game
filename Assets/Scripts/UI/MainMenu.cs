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
    public GameObject mainFirstButton, selectorFirst, selectorClosed;

    public GameObject mainMenu;
    public GameObject selectorMenu;
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
        selectorMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        //set mew selected object
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Settings()
    {

    }
    public void LevelSelector()
    {
        mainMenu.SetActive(false);
        selectorMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        //set mew selected object
        EventSystem.current.SetSelectedGameObject(selectorFirst);
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
        SceneSelection.Instance.SelectNewScene();
    }
    public void BackToMain()
    {
        selectorMenu.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        //set mew selected object
        EventSystem.current.SetSelectedGameObject(selectorClosed);
    }
    
    //Will likely have this removed in final release unless we want to have a scene selection
    public void RockScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Golem");
    }
    public void ReaperScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Reaper"); //to be changed
    }
    public void MageScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Mage"); //to be changed
    }
    public void ArcherScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Archer"); //to be changed
    }
    public void SecretScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Final"); //to be changed
    }
    public void AlphaScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("AlphaScene"); //to be changed
    }
}
