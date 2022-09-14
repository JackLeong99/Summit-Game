﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Animator fade; //Creates a reference for the Animator: Fade.
    public FadeState fadeState;

    public IEnumerator FadeIn() //Called upon to Fade Out.
    {
        fade.SetTrigger("FadeIn");
        yield return FadeWait();
    }

    public void FadeOut() //Called upon to Fade In.
    {
        fade.SetTrigger("FadeOut");
    }

    public IEnumerator FadeWait()
    {
        while (!FinishedFade())
        {
            yield return null;
        }
    }

    public bool FinishedFade()
    {
        switch (fadeState)
        {
            case FadeState.Ended:
                fadeState = FadeState.Idle;
                return true;
            default:
                return false;
        }
    }
}

public enum FadeState
{
    Idle,
    Ended
}
