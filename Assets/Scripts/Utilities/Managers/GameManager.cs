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
    public List<SceneReference> exclusionScenes;
    public List<SceneReference> startingScenes;
    public List<SceneReference> gameScenes;

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        instance = this;
        mainCamera = GameObject.FindWithTag("MainCamera");
        SceneHandler.LoadScenes(startingScenes);
    }
}
