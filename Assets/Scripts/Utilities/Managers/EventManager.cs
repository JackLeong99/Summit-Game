using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    #region
    //list of events
    //!!! make sure to add .RemoveAllListeners() to any newly added events
    public FloatEvent OnHealthChange;
    public FloatEvent OnTakeDamage;


    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        OnHealthChange.RemoveAllListeners();
        OnTakeDamage.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        OnHealthChange.RemoveAllListeners();
        OnTakeDamage.RemoveAllListeners();
    }
}
