using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
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
    public List<SceneReference> testScenes;

    public void Awake()
    {
        GetInstances();
        LoadStarting();
    }

    public void GetInstances()
    {
        instance = this;
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    public void LoadStarting()
    {
        switch (true)
        {
            case bool x when testScenes.Count > 0:
                List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(gameScenes, exclusionScenes);
                scenesLoading.Concat(SceneHandler.LoadScenes(testScenes));
                break;
            default:
                SceneHandler.LoadScenes(startingScenes);
                break;
        }
    }

    public void LoadGame()
    {
        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(gameScenes, exclusionScenes);
        scenesLoading.Concat(SceneHandler.LoadScenes(bossScenes));
        exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();
    }

    public void QuitGame()
    {
        foreach (var scene in gameScenes)
        {
            exclusionScenes.Remove(scene);
        }

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(startingScenes, exclusionScenes);
    }

    public void LoadShop()
    {
        SceneHandler.SwapScenes(shopScene, exclusionScenes);
    }

    public void OnDeath()
    {
        QuitGame();
    }
}
