using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using DevLocker.Utils;
using StarterAssets;

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

    [Header("Unique Scene Handling")]
    public List<SceneReference> shopScene;
    public List<SceneReference> deathScene;
    public List<SceneReference> introScene;
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
                exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();
                break;
            default:
                SceneHandler.LoadScenes(startingScenes);
                break;
        }
    }

    public void LoadGame()
    {
        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(gameScenes, exclusionScenes);
        scenesLoading.Concat(SceneHandler.LoadScenes(introScene));
        exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();

        StartCoroutine(LoadProgression(scenesLoading, true));
    }

    public void QuitGame()
    {
        foreach (var scene in gameScenes)
        {
            exclusionScenes.Remove(scene);
        }

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(startingScenes, exclusionScenes);

        StartCoroutine(LoadProgression(scenesLoading, false));
    }

    public IEnumerator LoadProgression(List<AsyncOperation> scenesLoading, bool isLoad)
    {
        yield return WaitForDataProgress();

        yield return StartCoroutine(SceneLoadProgress(scenesLoading));

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator SceneLoadProgress(List<AsyncOperation> scenesLoading)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
    }

    public IEnumerator WaitForDataProgress()
    {
        while (player == null)
        {
            yield return null;
        }
    }

    public void LoadShop()
    {
        SceneHandler.SwapScenes(shopScene, exclusionScenes);
    }

    public void LoadBoss()
    {
        SceneHandler.SwapScenes(bossScenes, exclusionScenes);
    }

    public void OnDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QuitGame();
    }
}
