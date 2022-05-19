using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No GameManager in the scene");
            }
            return instance;
        }
    }
    public Camera Testing;
    public GameObject Player;
        
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
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetButtonDown("Escape"))
    {
        // Application.Quit();
        PauseGame();

            //Player.GetComponent<ThirdPersonController>().enabled = false;
            //Cursor.visible = true; //the issues is in StarterAssetsInput :(
            Cursor.lockState = CursorLockMode.None;
    }
    if(Input.GetButtonDown("Teleport"))
    {
        ResumeGame (); //why doesn't the cursor work!!!!!!!!!!!!!!!!!!
    }
    }

    public void onDeath() {
        UIManager.Instance.GameOverScreen();
    }

    public void PauseGame ()
    {
       // Testing.gameObject.SetActive(false);
        Time.timeScale = 0;
         UIManager.Instance.PauseMenu();
         Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Cursor.visible = true;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       // Testing.gameObject.SetActive(true);
    }
}
