using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Instance")]
    public static DataManager instance;

    [Header("Keybindings")]
    public Dictionary<string, KeyCode> keybind = new Dictionary<string, KeyCode> //Dictionary to store the keybinds.
    {
        { "LookUp", KeyCode.W },
        { "LookDown", KeyCode.S },
        { "MoveLeft", KeyCode.A },
        { "MoveRight", KeyCode.D },
        { "Jump", KeyCode.Space },
        { "Dash", KeyCode.LeftShift },
        { "Attack", KeyCode.E },
        { "Heal", KeyCode.R },
        { "Interact", KeyCode.F },
        { "Pause", KeyCode.Escape }
    };

    [Header("Sensitivity")]
    [Range(0.1f, 10)]
    public float sensitivity;

    [Header("Volume")]
    [Range(0,100)]
    public float volume;

    public void Awake()
    {
        instance = this;

    }

    public bool firsTime = true;
}