using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    public static SettingsMenu instance;
    #region Variables
    [Header("Selector")]
    public GameObject settingsFirst;
    //public UIEvents selectors;
    [Header("Sliders")]
    public Slider sensitivity;
    public Slider volume;
    [Header("General")]
    public GameObject settingsMenu;
    #endregion

    private int lastScene;
    public void Awake()
    {
        instance = this;
        
    }
    // Start is called before the first frame update

    void Start()
    {
        settingsMenu.SetActive(false);
        sensitivity.value = DataManager.instance.sensitivity;
        volume.value=DataManager.instance.volume;
    }

    public void Sensitivity(float value)
    {
        DataManager.instance.sensitivity=value;
    }

    public void Volume(float value)
    {
        DataManager.instance.volume=value;
        AkSoundEngine.SetRTPCValue("Volume_Master", value);
    }

    public void isSettings(bool isSettings, int menu)
    {
        switch (isSettings)
        {
            case true:
                EventSystem.current.SetSelectedGameObject(settingsFirst);
                settingsMenu.SetActive(true);
                lastScene=menu;
                break;
             case false:
                 break;
        }
    }

    public void GoBack()
    {
        if(lastScene==0)
        {
            settingsMenu.SetActive(false);
			GameObject pauseObject = GameObject.FindWithTag("Pause");
			Pause pausing = pauseObject.GetComponent<Pause>();
			pausing.UpdatePause(true);
        }
        if(lastScene==1)
        {
            settingsMenu.SetActive(false);
            MainMenu.Instance.MainTrue();
        }
    }
    public void ExitSettings()
    {
        settingsMenu.SetActive(false);
    }
}
