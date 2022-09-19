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
    public Slider volume;
    #endregion
    // Start is called before the first frame update

    void Start()
    {
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

    public void GoBack()
    {
        //go back to main or pause depending on which menu was previous
    }
}
