using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevLocker.Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject mainCamera;

    [Header("Scene Handling")]
    public List<SceneReference> startingScenes;
    public List<SceneReference> exclusionScenes;
    public List<SceneReference> gameScenes;
    public List<SceneReference> bossScenes;
    public List<SceneReference> shopScene;

    public void Awake()
    {
        GetInstances();
        StartGame();
    }

    public void GetInstances()
    {
        instance = this;
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    public void StartGame()
    {
        SceneHandler.LoadScenes(startingScenes);
    }

    public void LoadShop()
    {
        SceneHandler.SwapScenes(shopScene, exclusionScenes);
    }
}
