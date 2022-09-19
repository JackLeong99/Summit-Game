using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public static HealthbarManager instance;
    public BossHealthBar bossBar;

    public void Awake()
    {
        instance = this;
    }

    public void SetBoss()
    {
        StartCoroutine(bossBar.SetParameters());

        SetActive(true);
    }

    public void ClearBoss()
    {
        StartCoroutine(bossBar.ClearParameters());

        SetActive(false);
    }

    public void SetActive(bool state)
    {
        bossBar.gameObject.SetActive(state);
    }
}
