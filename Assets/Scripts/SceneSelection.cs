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
    string[] avaliableLevels = { "AnimatorAttempt", "ReaperScene", "ArcherScene", "MageScene"}; //each one is a level name
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
    if(Input.GetButtonDown("Escape"))
    {
        SelectNewScene();
    }
    }

    //put all in array then each time do random select and then remove it from the array

    //gets called when going to new area after shop ie by teleporter
    public void SelectNewScene()
    {
        int nextLevel =Random.Range(0, avaliableLevels.Length);
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
        SceneManager.LoadScene(nextLevelString);
    }

    public void GoToShop()
    {
        SceneManager.LoadScene("AnimatorAttempt");
    }
    //make a go to shop method

    //These methods are used for testing purposes ie when we get people to playtest
    public void RockScene()
    {
        SceneManager.LoadScene("AnimatorAttempt");
    }
    public void ReaperScene()
    {
        SceneManager.LoadScene("ReaperScene"); //to be changed
    }
    public void MageScene()
    {
        SceneManager.LoadScene("MageScene"); //to be changed
    }
    public void ArcherScene()
    {
        SceneManager.LoadScene("ArcherScene"); //to be changed
    }
    public void SecretScene()
    {
        SceneManager.LoadScene("SecretScene"); //to be changed
    }
}
