using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    public static HealthbarManager instance;
    public GameObject bossBar;

    public void Awake()
    {
        instance = this;
    }

    public void SetActive(bool state)
    {
        bossBar.SetActive(state);
    }
}
