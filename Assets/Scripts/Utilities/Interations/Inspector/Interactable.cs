using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent callback;
    public UnityEvent display;

    public void Invoke()
    {
        callback.Invoke();
    }

    public void Display()
    {
        display.Invoke();
    }
}
