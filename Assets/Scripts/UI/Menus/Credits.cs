using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Credits : MonoBehaviour
{
    public static Credits instance;
    #region Variables
    [Header("Selector")]
    public GameObject creditsFirst;
    [Header("General")]
    public GameObject creditsMenu;
    #endregion

    public void Awake()
    {
        instance = this;
        creditsMenu.SetActive(false);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void isCredits(bool credit)
    {
        switch (credit)
        {
            case true:
                EventSystem.current.SetSelectedGameObject(creditsFirst);
                creditsMenu.SetActive(true);
                break;
             case false:
                 break;
        }
    }
    public void GoBack()
    {
            creditsMenu.SetActive(false);
            MainMenu.Instance.BackCredits();
    }
}
