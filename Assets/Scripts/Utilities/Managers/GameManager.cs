using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DevLocker.Utils;
using StarterAssets;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject mainCamera;
    public StarterAssetsInputs input;
    public MenuInput menuInput;

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
        menuInput = new MenuInput();
        menuInput.UI.Enable();
    }

    public void GetInstances()
    {
        instance = this;
        mainCamera = GameObject.FindWithTag("MainCamera");
        input = GameObject.FindWithTag("EventSystem").GetComponent<StarterAssetsInputs>();
    }

    public void LoadStarting()
    {
        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        switch (true)
        {
            case bool x when testScenes.Count > 0:
                scenesLoading = SceneHandler.LoadScenes(gameScenes);
                scenesLoading = scenesLoading.Concat(SceneHandler.LoadScenes(testScenes)).ToList();
                exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();
                StartCoroutine(LoadProgression(scenesLoading, testScenes[0]));
                break;
            default:
                scenesLoading = SceneHandler.LoadScenes(startingScenes);
                StartCoroutine(LoadProgression(scenesLoading, startingScenes[0]));
                Debug.Log("?");
                break;
        }
    }

    public void LoadGame()
    {
        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(introScene, exclusionScenes);

        StartCoroutine(LoadProgression(scenesLoading, introScene[0]));
    }

    public void QuitGame()
    {
        foreach (var scene in gameScenes)
        {
            exclusionScenes.Remove(scene);
        }

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(startingScenes, exclusionScenes);

        StartCoroutine(LoadProgression(scenesLoading, startingScenes[0]));
    }

    public IEnumerator LoadProgression(List<AsyncOperation> scenesLoading, SceneReference activeScene)
    {
        yield return StartCoroutine(SceneLoadProgress(scenesLoading));

        yield return StartCoroutine(SceneHandler.SetActive(activeScene));
        Debug.Log("?");
    }

    public IEnumerator SceneLoadProgress(List<AsyncOperation> scenesLoading)
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                Debug.Log(scenesLoading[i].ToString());
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
        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(shopScene, exclusionScenes);
        StartCoroutine(LoadProgression(scenesLoading, shopScene[0]));
    }

    public void LoadBoss()
    {
        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(gameScenes, exclusionScenes);
        scenesLoading = scenesLoading.Concat(SceneHandler.LoadScenes(bossScenes)).ToList();
        exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();

        StartCoroutine(LoadProgression(scenesLoading, bossScenes[0]));
    }

    public void OnDeath()
    {
        List<AsyncOperation> scenesLoading = SceneHandler.ReloadScene(gameScenes[0]);
        scenesLoading = scenesLoading.Concat(SceneHandler.SwapScenes(deathScene, exclusionScenes)).ToList();
        StartCoroutine(LoadProgression(scenesLoading, deathScene[0]));
    }
}
