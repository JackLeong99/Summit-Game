using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DevLocker.Utils;
using StarterAssets;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Key References")]
    public GameObject player;
    public GameObject mainCamera;
    public FadeController fade;
    public bool inLoading = false;
    public bool finalReady = false;

    [Header("Input References")]
    public StarterAssetsInputs input;
    public MenuInput menuInput;

    [Header("Scene Handling")]
    public List<SceneReference> startingScenes;
    public List<SceneReference> exclusionScenes;
    public List<SceneReference> gameScenes;
    public List<SceneReference> bossScenes;

    [Header("Unique Scene Handling")]
    public SceneReference shopScene;
    public SceneReference deathScene;
    public SceneReference finalScene;
    public SceneReference introScene;
    public SceneReference endingScene;
    public List<SceneReference> testScenes;

    private  List<SceneReference> selectableScenes;

    public void Awake()
    {
        GetInstances();
        StartCoroutine(LoadStarting());
        menuInput = new MenuInput();
        menuInput.UI.Enable();
    }

    public void GetInstances()
    {
        instance = this;
        mainCamera = GameObject.FindWithTag("MainCamera");
        fade = GameObject.FindWithTag("FadeController").GetComponent<FadeController>();
        input = GameObject.FindWithTag("EventSystem").GetComponent<StarterAssetsInputs>();

        selectableScenes = new List<SceneReference>(bossScenes);
    }

    public IEnumerator LoadStarting()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        switch (true)
        {
            case bool x when testScenes.Count > 0:
                scenesLoading = SceneHandler.LoadScenes(gameScenes);
                scenesLoading = scenesLoading.Concat(SceneHandler.LoadScenes(testScenes)).ToList();
                exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();
                yield return StartCoroutine(LoadProgression(scenesLoading, testScenes[0]));
                break;
            default:
                scenesLoading = SceneHandler.LoadScenes(startingScenes);
                yield return StartCoroutine(LoadProgression(scenesLoading, startingScenes[0]));
                Debug.Log("?");
                break;
        }
    }

    public IEnumerator LoadGame()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(introScene, exclusionScenes);

        yield return StartCoroutine(LoadProgression(scenesLoading, introScene));
    }

    public IEnumerator QuitGame()
    {
        yield return StartCoroutine(StartLoad());

        foreach (var scene in gameScenes)
        {
            exclusionScenes.Remove(scene);
        }

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(startingScenes, exclusionScenes);

        yield return StartCoroutine(LoadProgression(scenesLoading, startingScenes[0]));
    }

    public IEnumerator LoadProgression(List<AsyncOperation> scenesLoading, SceneReference activeScene)
    {
        yield return StartCoroutine(SceneLoadProgress(scenesLoading));

        yield return StartCoroutine(SceneHandler.SetActive(activeScene));

        yield return fade.FadeOut();
        inLoading = false;
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

    public IEnumerator StartLoad()
    {
        inLoading = true;
        yield return fade.FadeIn();
    }

    public IEnumerator LoadShop(bool firstLoad)
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        switch (true)
        {
            case bool x when firstLoad:
                scenesLoading = scenesLoading.Concat(SceneHandler.LoadScenes(gameScenes)).ToList();
                exclusionScenes = exclusionScenes.Concat(gameScenes).ToList();
                break;
        }

        scenesLoading = scenesLoading.Concat(SceneHandler.SwapScenes(shopScene, exclusionScenes)).ToList();
        yield return StartCoroutine(LoadProgression(scenesLoading, shopScene));
    }

    public IEnumerator LoadFinal()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(finalScene, exclusionScenes);
        yield return StartCoroutine(LoadProgression(scenesLoading, finalScene));
    }

    public IEnumerator LoadOutro()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = SceneHandler.SwapScenes(endingScene, exclusionScenes);
        yield return StartCoroutine(LoadProgression(scenesLoading, endingScene));
    }

    public IEnumerator LoadBoss()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

        switch (true)
        {
            case bool y when selectableScenes.Count == 1:
                finalReady = true;
                break;
            case bool z when selectableScenes.Count < 1:
                selectableScenes = new List<SceneReference>(bossScenes);
                break;
        }

        SceneReference selected = selectableScenes[ListHandler.IndexRandomizer(selectableScenes.Count)];
        scenesLoading = scenesLoading.Concat(SceneHandler.SwapScenes(selected, exclusionScenes)).ToList();
        selectableScenes.Remove(selected);

        yield return StartCoroutine(LoadProgression(scenesLoading, selected));
    }

    public IEnumerator OnDeath()
    {
        yield return StartCoroutine(StartLoad());

        List<AsyncOperation> scenesLoading = SceneHandler.ReloadScene(gameScenes[0]);
        scenesLoading = scenesLoading.Concat(SceneHandler.SwapScenes(deathScene, exclusionScenes)).ToList();
        yield return StartCoroutine(LoadProgression(scenesLoading, deathScene));
        UIItemDisplay.instance.ClearListOfItems(); //same in player health. Delete one that is not needed
    }

    public void LoadDelegate(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}