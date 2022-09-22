using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class Pause : MonoBehaviour
{
    #region Variables
    [Header("General")]
    public PauseState pauseState = PauseState.Playing; //Checks whether or not the game is paused
    public GameObject pauseMenu, background;
    //public FadeController fade;

    [Header("Selector")]
    public GameObject pauseFirst;
    //public UIEvents selectors;
    #endregion

    #region General
    public void Awake()
    {
        //fade = GameObject.FindWithTag("FadeController").GetComponent<FadeController>();
        //selectors = GameObject.FindWithTag("Selectors").GetComponent<UIEvents>();
        pauseMenu.SetActive(false);
        background.SetActive(false);
        //selectors.Visibility(false);
    }
    #endregion

    #region Pause
    public void DoPause()
    {             
        switch (pauseState)
            {
                case PauseState.Playing:
                    PauseG(); 

                    break;
                case PauseState.Pause:
                    ResumeG();
                    break;
            }
    }

    public void ResumeG() //Trigger for resuming game and resume button
    {
        UpdatePause(false);
    }

    public void PauseG() //Trigger for pausing game
    {
        UpdatePause(true);
      
    }

    public void UpdatePause(bool pause)
    {
        AkSoundEngine.PostEvent("UI_Click", GameManager.instance.mainCamera);

        pauseMenu.SetActive(pause);
        background.SetActive(pause);

        switch (pause)
        {
            case true:
                pauseState = PauseState.Pause;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                EventSystem.current.SetSelectedGameObject(pauseFirst);
                AkSoundEngine.PostEvent("UI_Menu_On", GameManager.instance.mainCamera);
                break;
            case false:
                pauseState = PauseState.Playing;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                AkSoundEngine.PostEvent("UI_Menu_Off", GameManager.instance.mainCamera);
                break;
        }

        Cursor.visible = pause;
    }

    public void CallMenu() //Trigger for menu button
    {
        StartCoroutine(ChangeToMain());
    }

    IEnumerator ChangeToMain()
    {
        Time.timeScale = 1f;
        GameManager.instance.QuitGame();
        yield return null;
    }

    public void SettingsCall()
    {
        pauseMenu.SetActive(false);
        background.SetActive(false);
        SettingsMenu.instance.isSettings(true, 0);
    }
    #endregion
}
public enum PauseState
{
    Playing,
    Pause
}