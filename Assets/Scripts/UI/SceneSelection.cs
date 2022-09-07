using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    private static SceneSelection instance;
    public static SceneSelection Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No SceneSelection in the scene");
            }
            return instance;
        }
    }
    string[] avaliableLevels = { "AnimatorAttempt", "Reaper", "Archer", "Mage"}; //each one is a level name

    private bool isSecret=false;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
                if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //put all in array then each time do random select and then remove it from the array

    //gets called when going to new area after shop ie by teleporter
    public void SelectNewScene()
    {
        GameManager.instance.LoadGame();
        /*int nextLevel =Random.Range(0, avaliableLevels.Length);
        string nextLevelString=avaliableLevels[nextLevel];
        string[] tempAvaliableLevels=new string[avaliableLevels.Length-1];
        int tracker=0;
        for( int i=0; i<avaliableLevels.Length; i++)
        {
            if(avaliableLevels[i]!=nextLevelString)
            {
                tempAvaliableLevels[i-tracker]=avaliableLevels[i];
            }
            if(avaliableLevels[i]==nextLevelString)
            {
            tracker=1;
            }
        }   
        
        avaliableLevels=new string[tempAvaliableLevels.Length];
        avaliableLevels=tempAvaliableLevels;
        if(isSecret && avaliableLevels.Length==0)
        {
            nextLevelString="FinalBossScene";
        }
        SceneManager.LoadScene(nextLevelString);*/
    }

    //called by collider on portal
    public void GoToShop()
    {
        SceneManager.LoadScene("ShopArea"); //to be changed
    }
    public void DeathScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        avaliableLevels =new string[] { "AnimatorAttempt", "Reaper", "Archer", "Mage"};
        SceneManager.LoadScene("DeathScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SecretScenes()
    {
        avaliableLevels =new string[] { "SecretAnimatorAttempt", "SecretReaper", "SecretArcher", "SecretMage"};
        isSecret=true;
    }
}
