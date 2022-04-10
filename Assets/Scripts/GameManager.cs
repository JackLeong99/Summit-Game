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
        
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetButtonDown("Escape"))
    {
        Application.Quit();
    }
    }

    public void onDeath() {
        UIManager.Instance.GameOverScreen();
    }
}
