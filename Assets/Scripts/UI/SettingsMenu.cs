using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    #region Variables
    [Header("Selector")]
    public GameObject settingsFirst;
    //public UIEvents selectors;
    [Header("Sliders")]
    public Slider sensitivity;
    #endregion
    // Start is called before the first frame update

    void Start()
    {
        sensitivity.value = DataManager.instance.sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sensitivity(float value)
    {
        DataManager.instance.sensitivity=value;
    }
}
